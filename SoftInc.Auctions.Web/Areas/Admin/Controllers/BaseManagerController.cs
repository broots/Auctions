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
    public class BaseManagerController : Controller
    {
        protected IRepository<ItemCategory> _catManager;
        protected IRepository<ItemSubCategory> _subCatManager;

        protected List<ItemCategory> Categories;
        protected List<ItemSubCategory> SubCategories;

        public BaseManagerController()
        {
            _catManager = new DataManager<ItemCategory>();
            _subCatManager = new DataManager<ItemSubCategory>();

            var t = Task.Run(async () => await LoadCategories());
            Categories = t.Result;
        }

        protected async Task<List<ItemCategory>> LoadCategories()
        {
            var cats = await _catManager.GetAll();
            SubCategories = await _subCatManager.GetAll();

            return cats;
        }

        protected IEnumerable<SelectListItem> GetCategoryList(short? categoryId = null)
        {
            var ls = Categories.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.Id.ToString(), Selected = x.Id == categoryId });
            return ls;
        }

        protected JsonResult FilterSubCategories(string id)
        {
            var sc = SubCategories.Where(x => x.CategoryId == int.Parse(id)).ToList();
            var subCats = sc.Select(x => new SelectListItem { Text = x.SubCategoryName, Value = x.Id.ToString() });
            return Json(new SelectList(subCats, "Value", "Text", "--Please Select--"));
        }

        protected async Task<ActionResult> Save<T>(object model) where T : class
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataManager = new DataManager<T>();

                    ((BaseModel)model).DateCreated = ((BaseModel)model).DateCreated ?? DateTime.Now;
                    ((BaseModel)model).DateModified = DateTime.Now;

                    var data = Mapper.Map<T>(model);
                    var result = await dataManager.Save(data);

                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return Redirect(Request.UrlReferrer.ToString()); //View(viewName, model);
        }
    }
}