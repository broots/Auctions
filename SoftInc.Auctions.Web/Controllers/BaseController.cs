﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SoftInc.Auctions.Web.Controllers
{
    public class BaseController : Controller
    {
        protected AuctionController _auctionController;
        protected ItemController _itemController;
        protected string PageHeader { get; set; }
        protected string PageDescription { get; set; }
        protected string UserName { get; set; }

        private IRepository<Bidder> bidderMng;


        public BaseController()
        {
            _auctionController = new AuctionController();
            _itemController = new ItemController();

            bidderMng = new DataManager<Bidder>();

            UserName = User?.Identity?.GetUserName(); // this.HttpContext?.User.Identity.Name;

            ViewBag.User = !string.IsNullOrEmpty(UserName) ? UserName : "Anonymous";
            ViewBag.PageHeader = PageHeader;
            ViewBag.PageDescription = PageDescription;
        }

        protected async Task<Bidder> GetBidder(string email, ApplicationUserManager userManager = null)
        {
            var um = userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); 
            var u = await um.FindByEmailAsync(email);
            var userId = u.Id;
            var result = await bidderMng.Search(m => m.UserId == userId);
            var bidder = result.FirstOrDefault();
            SetBidder(bidder);

            return bidder;
        }

        protected void SetBidder(Bidder b)
        {
            Session["bidderId"] = b?.Id;
            Session["Name"] = $"{b.FirstName}";
        }

    }
}