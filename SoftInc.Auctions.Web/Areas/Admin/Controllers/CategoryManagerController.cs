using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Business.Managers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SoftInc.Auctions.Web.Areas.Admin.Controllers
{
    public class CategoryManagerController : Controller
    {
        IRepository<ItemCategory> _catManager;

        public CategoryManagerController()
        {
            _catManager = new DataManager<ItemCategory>();
        }
        // GET: Admin/CategoryManager
        public async Task<ActionResult> Index()
        {
            var data = await _catManager.GetAll();
            return View(data);
        }

        public async Task<ActionResult> Create(ItemCategory category)
        {
            if (string.IsNullOrEmpty(category?.CategoryName)) return View(category);
            try
            {
                if (ModelState.IsValid)
                {
                    category.DateCreated = DateTime.Now;
                    var result = await _catManager.Save(category);

                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(category);
        }

        public async Task<ActionResult> Edit(long id)
        {
            var data = await _catManager.Get(m => m.Id == id);
            return View(data);
        }

        public async Task<ActionResult> Delete(long id)
        {
            var bln = await _catManager.Delete(m => m.Id == id);
            return RedirectToAction("Index");
        }
    }
}