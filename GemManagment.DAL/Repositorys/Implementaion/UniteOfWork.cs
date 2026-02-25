using GemManagment.DAL.Data.Context;
using GemManagment.DAL.Models.Base;
using GemManagment.DAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Repositorys.Implementaion
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly GemDbcontext dbcontext;
        private readonly ISessionRepo sessionRepo;
        private readonly IMemberShipRepo memberShipRepo;
        private readonly Dictionary<Type, object> repositoris = new();


        public UniteOfWork(GemDbcontext dbcontext, ISessionRepo sessionRepo, IMemberShipRepo memberShipRepo)
        {
            this.dbcontext = dbcontext;
            this.sessionRepo = sessionRepo;
            this.memberShipRepo = memberShipRepo;
        }

        public ISessionRepo SessionRepo { get => sessionRepo; }

        public IMemberShipRepo MemberShipRepo { get  => memberShipRepo; }

        public IGenericRepo<T> GetGenericRepo<T>() where T : BaseEntity, new()
        {
            var key = typeof(T);
            if (!repositoris.ContainsKey(key))
            {
                var repoInstance = new GenericRepo<T>(dbcontext);
                repositoris[key] = repoInstance;
            }
            return (IGenericRepo<T>)repositoris[key];
        }

        public int SaveChanges()
        {
           return dbcontext.SaveChanges();
        }
    }
}
