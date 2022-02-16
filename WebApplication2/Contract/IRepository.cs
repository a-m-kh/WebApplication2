using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Dto;
using System.Collections.Generic;
namespace WebApplication2.Contract
{
    public interface IRepository<TEntity> where TEntity  : class, new()
    {
        public Task<TEntity> AddAsync(TEntity te);
        public Task<List<TEntity>> Get(TEntity te);
        public Task<TEntity> Update(TEntity te );
        public Task<TEntity> Delete(TEntity te);
        public Task<bool> Search(TEntity te);
    }
}
