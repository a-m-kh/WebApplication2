using WebApplication2.Contract;
using WebApplication2.Dto;
using WebApplication2.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mapster;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace WebApplication2.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class , new()
    {
        private readonly  DBContext _Context;
        public Repository(DBContext context)
        {
            _Context = context;
        }
       
        public virtual async Task<TEntity> AddAsync(TEntity entity )
        {
            if(entity== null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} must not be null");
            }
            try
            {
                var en = await _Context.Set<TEntity>().AddAsync(entity);
                await _Context.SaveChangesAsync();
                return entity;
            }catch(DbUpdateException ex)
            {
                throw new DbUpdateException($"{nameof(AddAsync)} could not be save: {ex.Message}");
            }          
        }

        public virtual async Task<TEntity> Delete(TEntity entity )
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Delete)} must not be null");
            }
            try
            {
                _Context.Set<TEntity>().Remove(entity);
                await _Context.SaveChangesAsync();
                return entity;

            }catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"{nameof(Delete)} could not delete : {ex.Message}");
            }
        }

        public virtual async Task<List<TEntity>> Get(TEntity entity)
        {
            if(entity ==null)
            {
                throw new ArgumentNullException($"{nameof(Get)} must not be null");
            }
            try
            {
                var entities = _Context.Set<TEntity>().ToList();
                //await _Context.SaveChangesAsync();
                return entity.Adapt<List<TEntity>>();
            }catch(DbUpdateException ex)
            {
                throw new DbUpdateException($"{nameof(Get)} could not be get : {ex.Message}");
            }
           
        }
        public virtual async Task<int> Count(TEntity entity )
        {
            if(entity == null)
            {
                throw new ArgumentNullException($"{nameof(Count)} must not be null");
            }
            try
            {
                int number = await _Context.Set<TEntity>().CountAsync();
                return number;
            }catch(DbUpdateException ex)
            {
                throw new DbUpdateException($"{nameof(Count)} could not Count :{ex.Message}");
            }
        }
        public virtual async Task<bool> Search(TEntity entity )
        {
            if(entity == null)
            {
                throw new ArgumentNullException($"{nameof(Search)} must not be null");
            }
            try
            {
                var en = await _Context.Set<TEntity>().FindAsync(entity);
                return true;
            }catch(DbUpdateException ex)
            {
                throw new DbUpdateException($"{nameof(Search)} could not Search :{ex.Message}");
            }
            
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException($"{nameof(Update)} must not be null");
            }
            try
            {
                _Context.Set<TEntity>().Update(entity);
                await _Context.SaveChangesAsync();
                return entity;
            }catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"{nameof(Update)} could not be Update : {ex.Message}");
            }
             
        }
    }
}
