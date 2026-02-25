using GemManagment.DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Models
{
    public class MemberSession:BaseEntity
    {

        public bool IsAttended { get; set; }
        public Session Session { get; set; } = null!;
        public int SessionId { get; set; }

        public Member Member { get; set; } = null!;
        public int MemberId { get; set; }

    }
}
