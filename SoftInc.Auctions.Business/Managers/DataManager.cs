using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Business.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SoftInc.Auctions.Business.Managers
{
    public class DataManager<T> : IRepository<T> where T : class
    {
        public async Task<bool> Delete(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes)
        {
            var toRtn = false;
            try
            {
                using (var dbContext = new AuctionsContext())
                {
                    var obj = await dbContext.Set<T>().IncludeMultiple(includes).FirstOrDefaultAsync(query);
                    dbContext.Set<T>().Remove(obj);
                    await dbContext.SaveChangesAsync();
                }

                toRtn = true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return toRtn;
        }

        public async Task<bool> DeleteAll(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes)
        {
            var toRtn = false;
            try
            {
                using (var dbContext = new AuctionsContext())
                {
                    var objs = dbContext.Set<T>().IncludeMultiple(includes).Where(query);

                    foreach (var obj in objs)
                    {
                        dbContext.Set<T>().Remove(obj);
                    }

                    await dbContext.SaveChangesAsync();
                }

                toRtn = true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return toRtn;
        }

        public async Task<T> Get(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes)
        {
            using (var dbContext = new AuctionsContext())
            {
                var result = await dbContext.Set<T>().IncludeMultiple(includes).FirstOrDefaultAsync(query);
                return result;
            }
        }

        public async Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            using (var dbContext = new AuctionsContext())
            {
                var result = await dbContext.Set<T>().IncludeMultiple(includes).ToListAsync();
                return result;
            }
        }

        public async Task<T> Save(T entity)
        {
            try
            {
                using (var dbContext = new AuctionsContext())
                {
                    dbContext.Set<T>().AddOrUpdate(entity);
                    await dbContext.SaveChangesAsync();
                }

                return entity;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public async Task<List<T>> SaveAll(List<T> entities)
        {
            try
            {
                using (var dbContext = new AuctionsContext())
                {
                    foreach (var entity in entities)
                    {
                        dbContext.Set<T>().AddOrUpdate(entity);
                    }

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (DbEntityValidationException e)
            {
                var ls = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    var errs = eve.ValidationErrors.Select(x => $"Property: {x.PropertyName}, Error:{x.ErrorMessage}");
                    var msg = $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors: {string.Join(Environment.NewLine, errs)}";
                    ls.Add(msg);
                }

                //logger.Error(string.Join(Environment.NewLine, ls));
                //throw;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return entities;
        }

        public async Task<List<T>> Search(Expression<Func<T, bool>> query, Expression<Func<T, object>> orderBy = null, int? skip = null, int? take = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                using (var dbContext = new AuctionsContext())
                {
                    var s = skip.GetValueOrDefault();
                    var t = take ?? 100000;

                    var q = dbContext.Set<T>().Where(query).IncludeMultiple(includes);

                    if (orderBy != null)
                        q.OrderBy(orderBy).Skip(s);


                    var result = await q.Take(t).ToListAsync();

                    return result;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
    }
}
