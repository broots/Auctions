using SoftInc.Auctions.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SoftInc.Auctions.Web.Controllers
{
    public class BaseApiController<T> : ApiController where T : class
    {
        protected IRepository<T> _dataManager;

        public BaseApiController()
        {
            _dataManager = new DataManager<T>();
        }

        public async Task<IHttpActionResult> Save(T entity)
        {
            try
            {
                entity = await _dataManager.Save(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IHttpActionResult> Search(Expression<Func<T, bool>> query, Expression<Func<T, object>> orderBy = null, int? skip = null, int? take = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var result = await _dataManager.Search(query, orderBy, skip, take, includes);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IHttpActionResult> GetById(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var result = await _dataManager.Get(query, includes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
