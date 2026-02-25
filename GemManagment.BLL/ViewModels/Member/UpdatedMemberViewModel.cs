using GemManagment.DAL.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.ViewModels.Member
{
    public class UpdatedMemberViewModel
    {

        public string Name { get; set; } = null!;

        public string? Photo { get; set; }
        //public IFormFile newfile { get; set; }

        [Required(ErrorMessage = "Email is Rquired")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "must BE Email Type")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is Rquired")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"^01(1|2|5|0)\d{8}$", ErrorMessage = "The Phone Must Be Egyption Number")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Building Number Is Rquired")]
        [Range(1, 500, ErrorMessage = "Must be between 1 to 500")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street Is Rquired")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "Street Is Rquired")]
        public string City { get; set; } = null!;

        


    }
}
