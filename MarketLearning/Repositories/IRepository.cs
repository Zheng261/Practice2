using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MarketLearning.Models.Entities;

namespace MarketLearning.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        
        Task<T> GetByIDAsync(int? id);

        Task AddAsync(T obj);

        Task RemoveAsync(int? id);

        Task<List<T>> GetByPropertyAsync(Expression<Func<T, bool>> expression);

        Task UpdateStateAsync(T obj);
    }
}
