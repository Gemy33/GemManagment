using GemManagment.BLL.Services.AttachmentService;
using GemManagment.BLL.Services.Interfaces;
using GemManagment.BLL.ViewModels.Member;
using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Implementaion
{
    
    public class MemberService : ImemberService
    {
        private readonly IUniteOfWork uniteOfWork;
        private readonly IAttachmentService attachmentService;

        public MemberService(IUniteOfWork uniteOfWork, IAttachmentService attachmentService)
        {
            this.uniteOfWork = uniteOfWork;
            this.attachmentService = attachmentService;
        }

        public bool CreateMember(CreateMemberViewModel member)
        {
            var _memberRepo = uniteOfWork.GetGenericRepo<Member>();
            try
            {
                var existingEmail = _memberRepo.GetAll(m => m.Email == member.Email).Any();
                if (existingEmail)
                {
                    return false; // Email already exists
                }
                var existingPhone = _memberRepo.GetAll(m => m.Phone == member.Phone).Any();
                if (existingPhone)
                {
                    return false; // Phone already exists
                }


                var newMember = new Member
                {
                    Photo = attachmentService.Upload("member", member.PhotoFile),
                    Name = member.Name,
                    Email = member.Email,
                    Phone = member.Phone,
                    DataOfBarth = member.DateOfBirth,
                    HealthRecord = new HealthRecord
                    {
                        Height = member.HealthRecord.Height,
                        Weight = member.HealthRecord.Weight,
                        BloodType = member.HealthRecord.BloodType,
                        Note = member.HealthRecord.Note
                    },
                    Gender = member.Gender,
                    Address = new Addrees
                    {
                        BuildingNumber = member.BuildingNumber.ToString(),
                        City = member.City,
                        Street = member.Street
                    },

                };
                _memberRepo.Add(newMember);
                return uniteOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {

                return false;
            }

        }
        public IEnumerable<MemberViewModel> GetAllMembrs()
        {

            var members = uniteOfWork.GetGenericRepo<Member>().GetAll();
            var memberViewModels = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Photo = m.Photo,
                Name = m.Name,
                Phone = m.Phone,
                Email = m.Email,
                Gender = m.Gender.ToString()

            });
            return memberViewModels;

        }
        public MemberDetailsViewModel? GetMemberDetails(int memberId)
        {
            var _memberRepo = uniteOfWork.GetGenericRepo<Member>();
            var member = _memberRepo.GetById(memberId);
            if (member == null)
            {
                return null!;
            }
            var memberDetails = new MemberDetailsViewModel
            {

                Photo = member.Photo,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DataOfBarth.ToString("yyyy-MM-dd"),
                Address = $"{member.Address.BuildingNumber} - {member.Address.City} - {member.Address.Street}",
                Name = member.Name,
                Phone = member.Phone,
                Email = member.Email,

            };
            var _membershipRepo = uniteOfWork.GetGenericRepo<MemberPlan>();

            var ActiveMemberPlane = _membershipRepo.GetAll(ms => ms.MemberId == memberId && ms.Statuse == "Active").FirstOrDefault();
            if (ActiveMemberPlane != null)
            {
                memberDetails.MembershipStartDate = ActiveMemberPlane.CreatedAt.ToShortDateString();
                memberDetails.MembershipEndDate = ActiveMemberPlane.EndDate.ToShortDateString();

                var plan = uniteOfWork.GetGenericRepo<Member>().GetById(ActiveMemberPlane.PlansId);
                if (plan != null)
                    memberDetails.PlaneName = plan.Name;
            }
            return memberDetails;
        }
        public HealthyRecordViewModel? GetMemberHealthDetails(int Id)
        {
            var recordHealth = uniteOfWork.GetGenericRepo<HealthRecord>().GetById(Id);
            if (recordHealth is null)
            {
                return null!;
            }
            return new HealthyRecordViewModel
            {
                Height = recordHealth.Height,
                Weight = recordHealth.Weight,
                BloodType = recordHealth.BloodType,
                Note = recordHealth.Note
            };

        }
        public bool UpdateMember(int Id, UpdatedMemberViewModel member)
        {
            // when updat email or phone may me insert existng recored 
            try
            {
                var _memberRepo = uniteOfWork.GetGenericRepo<Member>();
                var existingMeail = _memberRepo.GetAll(m => m.Email == member.Email && m.Id != Id).Any();
                if (existingMeail)
                    return false;

                // add error this email aready exist
                var existingPhone = _memberRepo.GetAll(m => m.Phone == member.Phone && m.Id != Id).Any();
                if (existingPhone)
                    return false;

                var employeeToUpdate = _memberRepo.GetById(Id);
                if (employeeToUpdate is null)
                {
                    return false;
                }
                //if (member.newfile is not null)
                //{
                //    // exist old photo ==> remove it and add the new image
                //    if (employeeToUpdate.Photo is not null)
                //        attachmentService.Delete("member", employeeToUpdate.Photo);
                //    var newphoto = attachmentService.Upload("member", member.newfile);
                //    // no exist  ==> add the new image direct

                //    employeeToUpdate.Photo = newphoto;
                //}


                employeeToUpdate.Email = member.Email;
                employeeToUpdate.Phone = member.Phone;
                employeeToUpdate.Address.BuildingNumber = member.BuildingNumber.ToString();
                employeeToUpdate.Address.Street = member.Street;
                employeeToUpdate.Address.City = member.City;

                _memberRepo.Update(employeeToUpdate);
                return uniteOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool RemoveMember(int MemberId)
        {
            var _memberRepo = uniteOfWork.GetGenericRepo<Member>();
            var isExist = _memberRepo.GetById(MemberId);
            if (isExist is null)
            {
                return false;
            }
            var _memberSessionRepo = uniteOfWork.GetGenericRepo<MemberSession>();
            var HasActiveSession = _memberSessionRepo.GetAll(ms => ms.MemberId == MemberId && ms.Session.StartDate > DateTime.Now && ms.IsAttended == false).Any();
            if (HasActiveSession)
            {
                return false;
            }

            var _membershipRepo = uniteOfWork.GetGenericRepo<MemberPlan>();
            var memberPlans = _membershipRepo.GetAll(mp => mp.MemberId == MemberId);

            attachmentService.Delete("member", isExist.Photo);

            try
            {
                if (memberPlans.Any())
                {
                    foreach (var plan in memberPlans)
                    {
                        _membershipRepo.Delete(plan);
                    }

                }
                _memberRepo.Delete(isExist);
                return uniteOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }



        }
        public UpdatedMemberViewModel? GetMemberForUpdate(int memberId)
        {
            var member = uniteOfWork.GetGenericRepo<Member>().GetById(memberId);
            if (member is null)
            {
                return null;
            }
            return new UpdatedMemberViewModel
            {
                Photo = member.Photo,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = int.Parse(member.Address.BuildingNumber),
                Street = member.Address.Street,
                City = member.Address.City
            };
        }
    }
}
