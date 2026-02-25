using GemManagment.DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Models
{
    public class Member : GemUser
    {
        // joinded date == created at from base entity
        public string? Photo { get; set; }

        public HealthRecord HealthRecord { get; set; } = null!;
        public int HealthRecordId { get; set; }

        public ICollection<MemberPlan> MemberPlans { get; set; } = new HashSet<MemberPlan>();
        public ICollection<MemberSession> MemberSessions { get; set; } = new HashSet<MemberSession>();

    }
}
