using GemManagment.DAL.Data.Context;
using GemManagment.DAL.Models;
using GemManagment.DAL.Models.Base;
using GemManagment.DAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Repositorys.Implementaion
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GemDbcontext _gemDbcontext;

        public GenericRepo(GemDbcontext gemDbcontext)
        {
            _gemDbcontext = gemDbcontext;
        }

        public void Add(TEntity entity)
        {
            _gemDbcontext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _gemDbcontext.Set<TEntity>().Remove(entity);    
            
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
           if(condition is null)
                return _gemDbcontext.Set<TEntity>().ToList();
           else
                return _gemDbcontext.Set<TEntity>().Where(condition).ToList();
        }

        public TEntity? GetById(int id) => _gemDbcontext.Set<TEntity>().Find(id);


        public void Update(TEntity entity)
        {
            _gemDbcontext.Set<TEntity>().Update(entity);
            
        }
    }
}
