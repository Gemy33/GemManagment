using GemManagment.BLL.ViewModels.Member;
using GemManagment.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.Interfaces
{
    public interface ImemberService
    {
        IEnumerable<MemberViewModel> GetAllMembrs();

       bool CreateMember(CreateMemberViewModel member );

        MemberDetailsViewModel? GetMemberDetails(int memberId);

        HealthyRecordViewModel? GetMemberHealthDetails(int Id);

        bool UpdateMember(int Id , UpdatedMemberViewModel member);

        UpdatedMemberViewModel? GetMemberForUpdate(int memberId);
        bool RemoveMember(int MemberId);

    }
}
