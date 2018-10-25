using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IHttpActionResult> GetUpcomingAuctions()
        {
            var dt = DateTime.Today;
            var result = await Search(m => m.StartDate >= dt);

            return result;
        }

        public async Task<IHttpActionResult> GetAuctionById(long id)
        {
            var result = await GetById(m => m.Id == id);
            return result;
        }
    }
}
