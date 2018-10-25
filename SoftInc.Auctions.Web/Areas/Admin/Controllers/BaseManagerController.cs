using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Business.Managers;
using System;
using System.Collections.Generic;
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
    }
}