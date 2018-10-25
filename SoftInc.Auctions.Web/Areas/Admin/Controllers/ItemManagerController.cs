using AutoMapper;
using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Business.Managers;
using SoftInc.Auctions.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SoftInc.Auctions.Web.Areas.Admin.Controllers
{
    public class ItemManagerController : BaseManagerController
    {
        IRepository<Item> _itemManager;
        IRepository<ItemImage> _itemImagesManager;

        public ItemManagerController()
        {
            _itemManager = new DataManager<Item>();
            _itemImagesManager = new DataManager<ItemImage>();
        }

        // GET: Admin/ItemManager
        public async Task<ActionResult> Index()
        {
            var data = await _itemManager.Search(m => !m.IsSold.HasValue || !m.IsSold.Value, m => m.ItemName, null, null, m => m.ItemImages);
            var result = Mapper.Map<List<ItemModel>>(data);
            return View(result);
        }

        public async Task<ActionResult> Create(ItemModel item)
        {
            if (string.IsNullOrEmpty(item.ItemName))
            {
                item.Categories = GetCategoryList();
                return View(item);
            }

            item.DateCreated = DateTime.Now;
            return await Save(item, "Create");
        }

        public async Task<ActionResult> Edit(long id)
        {
            var data = await _itemManager.Get(m => m.Id == id, m => m.ItemImages);
            return View(data);
        }

        public async Task<ActionResult> Delete(long id)
        {
            var bln = await _itemManager.Delete(m => m.Id == id);
            return RedirectToAction("Index");
        }

        public JsonResult GetSubCategories(string id)
        {
            var subCats = SubCategories.Where(x => x.CategoryId == int.Parse(id)).Select(x => new SelectListItem { Text = x.SubCategoryName, Value = x.Id.ToString() });
            return Json(new SelectList(subCats));
        }

        private async Task<ActionResult> Save(ItemModel itemModel, string viewName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var item = Mapper.Map<Item>(itemModel);
                    var result = await _itemManager.Save(item);

                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(viewName, itemModel);
        }
    }
}