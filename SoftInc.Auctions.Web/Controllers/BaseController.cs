using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftInc.Auctions.Web.Controllers
{
    public class BaseController : Controller
    {
        protected AuctionController _auctionController;
        protected string PageHeader { get; set; }
        protected string PageDescription { get; set; }
        protected string UserName { get; set; }

        public BaseController()
        {
            _auctionController = new AuctionController();

            UserName = this.HttpContext?.User.Identity.Name;

            ViewBag.User = !string.IsNullOrEmpty(UserName) ? UserName : "Anonymous";
            ViewBag.PageHeader = PageHeader;
            ViewBag.PageDescription = PageDescription;
        }
    }
}