using GemManagment.DAL.Data.Context;
using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Repositorys.Implementaion
{
    public class MemberShipRepo : GenericRepo<MemberPlan>, IMemberShipRepo
    {
        private readonly GemDbcontext dbcontext;

        public MemberShipRepo(GemDbcontext dbcontext):base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public MemberPlan? GetMember(int memberid, int planid)
        {
           var meberplan = dbcontext.MemberPlan.Find(memberid, planid);
            if (meberplan == null) return null;
            return meberplan;

        }

        public List<MemberPlan> GetMembers()
        {
            var mebers = dbcontext.MemberPlan.Include(p => p.Member).Include(p => p.Plan).ToList();
            return mebers;
        }
    }
}
