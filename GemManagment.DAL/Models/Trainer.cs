using GemManagment.DAL.Models.Base;
using GemManagment.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Models
{
    public class Trainer : GemUser
    {
        //public DateOnly HiredDate { get; set; }
        public GeneralFitness Specialty { get; set; }
        public ICollection<Session> Sessions { get; set; } = new HashSet<Session>();
    }
}
