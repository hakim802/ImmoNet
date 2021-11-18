using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccess.Data;
using Microsoft.EntityFrameworkCore;
using static DataContext.Repository.IRepository.IGenericRepository;

namespace DataContext.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ImmoDbContext _context;

        private readonly DbSet<T> _db;

        public GenericRepository(ImmoDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }



        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<T> Get(System.Linq.Expressions.Expression<System.Func<T, bool>> expression,
                List<string> includes = null, List<string> includes2 = null,
                    List<string> includes3 = null, List<string> includes4 = null)
        {
            IQueryable<T> query = _db;

            if (includes is not null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (includes2 is not null)
            {
                foreach (var includeProperty in includes2)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (includes3 is not null)
            {
                foreach (var includeProperty in includes3)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (includes4 is not null)
            {
                foreach (var includeProperty in includes4)
                {
                    query = query.Include(includeProperty);
                }
            }



            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(System.Linq.Expressions.Expression<System.Func<T, bool>> expression = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                        List<string> includes = null, List<string> includes2 = null,
                            List<string> includes3 = null, List<string> includes4 = null)
        {
            IQueryable<T> query = _db;

            if (expression is not null)
            {
                query = query.Where(expression);
            }
            if (includes is not null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (includes2 is not null)
            {
                foreach (var includeProperty in includes2)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (includes3 is not null)
            {
                foreach (var includeProperty in includes3)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (includes4 is not null)
            {
                foreach (var includeProperty in includes4)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Create(T entity)
        {
            await _db.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}