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

            return View(items);
        }

        [Authorize]
        public async Task<ActionResult> Bidding(long itemId)
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated && Session["bidderId"] == null)
            {
                var ac = new AccountController();
                var b = await ac.GetBidder(User.Identity.Name);
            }

            var data = await _itemController.GetItemAndBids(itemId);
            return View(data);
        }
    }
}
