using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NewsLetterBoy.Model
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        public Task<int> InsertAsync(T entity); 
        public Task UpdateAsync(T entity);
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> whereClause);
    }
}