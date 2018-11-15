using AutoMapper;
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

        protected async Task<K> Search<K>(Expression<Func<T, bool>> query, Expression<Func<T, object>> orderBy = null, int? skip = null, int? take = null, params Expression<Func<T, object>>[] includes) where K : class
        {
            try
            {
                var data = await _dataManager.Search(query, orderBy, skip, take, includes);
                var result = Mapper.Map<K>(data);

                return result; // Ok(result);
            }
            catch (Exception ex)
            {
                return null; // BadRequest(ex.Message);
            }
        }

        protected async Task<K> GetById<K>(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes) where K : class
        {
            try
            {
                var data = await _dataManager.Get(query, includes);
                var result = Mapper.Map<K>(data);
                return result;
            }
            catch (Exception ex)
            {
                return null;// BadRequest(ex.Message);
            }
        }
    }
}
