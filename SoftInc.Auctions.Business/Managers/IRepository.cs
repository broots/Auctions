using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SoftInc.Auctions.Business.Managers
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<T> Get(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes);
        Task<T> Save(T entity);
        Task<List<T>> SaveAll(List<T> entities);
        Task<List<T>> Search(Expression<Func<T, bool>> query, Expression<Func<T, object>> orderBy = null, int? skip = null, int? take = null, params Expression<Func<T, object>>[] includes);
        Task<bool> Delete(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes);
        Task<bool> DeleteAll(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes);
    }
}
