using GemManagment.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Models.Base
{
    public class GemUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public DateOnly DataOfBarth { get; set; }

        public Gender Gender { get; set; }

        public Addrees Address { get; set; } = null!;
    }
}
