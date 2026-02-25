using GemManagment.DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Models
{
    public class Session:BaseEntity
    {
        public string Description { get; set; } = null!;
        public int Capacity { get; set; } // 1 - 25
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<MemberSession> MembersSession { get; set; } = new HashSet<MemberSession>();
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public Trainer Trainer { get; set; } = null!;
        public int TrainerId { get; set; }


    }
}
