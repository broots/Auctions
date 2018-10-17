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
    public class AuctionController : ApiController
    {
        IRepository<Auction> _auctionManager;

        public AuctionController()
        {
            _auctionManager = new DataManager<Auction>();
        }

        public async Task<IHttpActionResult> Save(Auction auction)
        {
            try
            {
                auction = await _auctionManager.Save(auction);
                return Ok(auction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IHttpActionResult> GetUpcomingAuctions()
        {
            try
            {
                var dt = DateTime.Today;
                var result = await _auctionManager.Search(m => m.StartDate >= dt);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IHttpActionResult> GetAuctionById(long id)
        {
            try
            {
                var result = await _auctionManager.Get(m => m.Id == id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
