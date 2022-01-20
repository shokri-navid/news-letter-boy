using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsLetterBoy.Model;

namespace NewsLetterBoy.Repository
{
    public class GenericRepository<T>: IGenericRepository<T> where T: BaseModel
    {
        protected readonly ILogger _logger;
        protected readonly NewsLetterDbContext _context;
        public GenericRepository(NewsLetterDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> InsertAsync(T entity)
        {
            try
            {
                
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var entry = _context.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity.Id == entity.Id);

                if (entry == null)
                {
                    _context.Set<T>().Update(entity);
                }
                else
                {
                    if (!ReferenceEquals(entity, entry.Entity))
                        entry.CurrentValues.SetValues(entity);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereClause)
        {
            try
            {
                var result  =  await _context.Set<T>().Where(whereClause).Where(x=>!x.IsDeleted).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

       

      
    }
}