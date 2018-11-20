using Microsoft.AspNet.Identity.Owin;
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
        public HomeController()
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated && Session["bidderId"] == null)
            {
                var um = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var b = Task.Run(async () => await GetBidder(User.Identity.Name, um));
                var br = b.Result;
            }

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
            items.ForEach(x => x.AuctionStartDate = auction.StartDate);

            return View(items);
        }

        [Authorize]
        public async Task<ActionResult> Bidding(long itemId)
        {
            var data = await _itemController.GetItemAndBids(itemId);
            return View(data);
        }
    }
}
