using GemManagment.DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Repositorys.Interfaces
{
    public interface IUniteOfWork
    {
        IGenericRepo<T> GetGenericRepo<T>() where T : BaseEntity , new();
        public ISessionRepo SessionRepo { get;}
        public IMemberShipRepo MemberShipRepo { get;}
        int SaveChanges();
    }
}
