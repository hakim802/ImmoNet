using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataContext.Repository.IRepository
{
    public interface IGenericRepository
    {
        public interface IGenericRepository<T> where T : class
        {

            Task Create(T entity);
            Task Delete(int id);
            void DeleteRange(IEnumerable<T> entities);
            Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null, List<string> includes2 = null, List<string> includes3 = null, List<string> includes4 = null);
            Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null, List<string> includes2 = null, List<string> includes3 = null, List<string> includes4 = null);
            void Update(T entity);
        }
    }
}