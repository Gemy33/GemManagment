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
    internal class PlanRepo : IPlanRepo
    {
        private readonly GemDbcontext _dbcontext;

        public PlanRepo(GemDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IEnumerable<Plans> GetAll()
        {
            return _dbcontext.Plans.AsNoTracking().ToList();
        }

        public Plans? GetById(int Id)
        {
            var Plan = _dbcontext.Plans.Find(Id);
            if (Plan is null)
                return null;
            return Plan;
        }
    }
}
