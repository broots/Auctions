using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SoftInc.Auctions.Web.Controllers
{
    public class HomeController : BaseController
    {
        private string email;
        public HomeController()
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated && Session["bidderId"] == null)
                Task.Run(async () => await GetBidder());

        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Home Page";
            PageHeader = "Home Page";
            PageDescription = "Optional...";

            var data = await _auctionController.GetActiveAuctions();

            return View(data);
        }

        public async Task<ActionResult> AuctionItems(long id)
        {
            var auction = await _auctionController.GetAuctionById(id);
            var items = auction.Items;
            items.ForEach(x =>
                {
                    x.AuctionStartDate = auction.StartDate;
                });

            return View(items);
        }

        [Authorize]
        public async Task<ActionResult> Bidding(long itemId)
        {
            var data = await _itemController.GetItemAndBids(itemId);
            var auction = await _auctionController.GetAuctionById(data.AuctionId.GetValueOrDefault());

            data.AuctionEndDate = auction.EndDate;
            data.AuctionStartTime = auction.StartTime;
            data.AuctionEndTime = auction.EndTime;
            return View(data);
        }

        [Authorize]
        public async Task<ActionResult> MyAuctions()
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated && Session["bidderId"] == null)
                await GetBidder();

            var bidderId = (long)Session["bidderId"];
            var bidder = await bidderMng.Get(m => m.Id == bidderId, m => m.Biddings, m => m.Biddings.Select(x => x.Item));

            bidder.Biddings = bidder.Biddings.GroupBy(x => x.ItemId).Select(x => x.OrderByDescending(a => a.Amount).FirstOrDefault())?.ToList();
            var data = Mapper.Map<ExtendedBidderModel>(bidder);

            return View(data);
        }

        private async Task<bool> GetBidder()
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated && Session["bidderId"] == null)
            {
                var um = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                email = User.Identity.Name;
                var result = await GetBidder(User.Identity.Name, um);
                //var br = b.Result;
                return true;
            }

            return false;
        }

    }
}
