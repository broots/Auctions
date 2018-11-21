using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Business.Managers;
using SoftInc.Auctions.Web.Models;
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
    public class AuctionController : BaseApiController<Auction>
    {
        public AuctionController()
        {
        }

        //public async Task<IHttpActionResult> Save(Auction auction)
        //{
        //    return await Save(auction);
        //}

        public async Task<List<AuctionModel>> GetActiveAuctions()
        {
            var dt = DateTime.Today;
            var result = await Search<List<AuctionModel>>(m => m.EndDate >= dt, m => m.EndDate, null, null, m => m.Items);

            return result;
        }

        public async Task<List<AuctionModel>> SearchForAuctions(Expression<Func<Auction, bool>> query, Expression<Func<Auction, object>> orderBy = null, int? skip = null, int? take = null, params Expression<Func<Auction, object>>[] includes)
        {
            var result = await Search< List<AuctionModel>>(query, orderBy, skip, take, includes);
            return result;
        }

        public async Task<AuctionModel> GetAuctionById(long id)
        {
            var result = await GetById<AuctionModel>(m => m.Id == id, m => m.Items);
            return result;
        }

    }
}
