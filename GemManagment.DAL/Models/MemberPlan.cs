using GemManagment.DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Models
{
    public class MemberPlan : BaseEntity
    {
        public string Statuse
        {
            get
            {
                if (EndDate <= DateTime.Now)
                {
                    return "Expired";
                }
                
                else
                {
                    return "Active";
                }
            }
        }
        public DateTime EndDate { get; set; }
        public Member Member { get; set; } = null!;
        public int MemberId { get; set; }
        public Plans Plan { get; set; } = null!;
        public int PlansId { get; set; }
    }
}
