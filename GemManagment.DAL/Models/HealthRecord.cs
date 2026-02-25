using GemManagment.DAL.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Models
{

    public class HealthRecord : BaseEntity
    {
        public decimal Height { get; set; } 
        public decimal Weight { get; set; } 
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }


    }
}
