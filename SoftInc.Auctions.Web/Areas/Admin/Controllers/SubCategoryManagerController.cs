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
    public class SubCategoryManagerController : BaseManagerController
    {
        public SubCategoryManagerController()
        {
            _subCatManager = new DataManager<ItemSubCategory>();
            _catManager = new DataManager<ItemCategory>();
        }
        // GET: Admin/SubCategoryManager
        public async Task<ActionResult> Index()
        {
            var result = await _subCatManager.GetAll(m => m.ItemCategory, m => m.Items);
            var data = Mapper.Map<List<SubCategoryModel>>(result);
            return View(data);
        }

        public async Task<ActionResult> Create(SubCategoryModel subCategory)
        {
            if (string.IsNullOrEmpty(subCategory?.SubCategoryName))
            {
                subCategory.Categories = GetCategoryList();
                return View(subCategory);
            }

            subCategory.DateCreated = DateTime.Now;
            return await Save(subCategory, "Create");
        }

        public async Task<ActionResult> Edit(long id)
        {
            var data = await _subCatManager.Get(m => m.Id == id, m => m.Items);
            var result = new SubCategoryModel { CategoryId = data.CategoryId, Id = data.Id, SubCategoryName = data.SubCategoryName, SubCategoryDescription = data.SubCategoryDescription }; // Mapper.Map<SubCategoryModel>(data);
            result.Categories = GetCategoryList(data.CategoryId);
            return View(result);
        }

        public async Task<ActionResult> Delete(long id)
        {
            var bln = await _subCatManager.Delete(m => m.Id == id);
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> SaveEdit(SubCategoryModel subCategory)
        {
            return await Save(subCategory, "Edit");
        }

        private async Task<ActionResult> Save(SubCategoryModel subCategory, string viewName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var subC = Mapper.Map<ItemSubCategory>(subCategory);
                    var result = await _subCatManager.Save(subC);

                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(viewName, subCategory);
        }
    }
}