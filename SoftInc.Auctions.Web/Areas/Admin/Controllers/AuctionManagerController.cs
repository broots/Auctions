using AutoMapper;
using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Business.Managers;
using SoftInc.Auctions.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SoftInc.Auctions.Web.Areas.Admin.Controllers
{
    public class AuctionManagerController : BaseManagerController
    {
        IRepository<Auction> _auctionManager;

        public AuctionManagerController()
        {
            _auctionManager = new DataManager<Auction>();
        }

        // GET: Admin/AuctionManager
        public async Task<ActionResult> Index()
        {
            var data = await _auctionManager.GetAll();
            var result = Mapper.Map<List<AuctionModel>>(data);
            return View(result);
        }

        public async Task<ActionResult> Create(AuctionModel auction)
        {
            if (!ModelState.IsValid)
            {
                return View(auction);
            }

            return await Save<Auction>(auction); // Save(auction, "Create");
        }

        public async Task<ActionResult> Edit(long id)
        {
            var data = await _auctionManager.Get(m => m.Id == id);
            var result = Mapper.Map<AuctionModel>(data);
            return View(result);
        }

        public async Task<ActionResult> Save(AuctionModel model)
        {
            return await Save<Auction>(model);
        }

        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var auction = await _auctionManager.Get(m => m.Id == id, m => m.Items);

            if (auction == null)
            {
                return HttpNotFound();
            }

            var result = Mapper.Map<AuctionModel>(auction);
            //result.Images = item.ItemImages.Select(x => x.ThumbImageString).ToList();

            return View(result);
        }
    }
}