using AutoMapper;
using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Business.Managers;
using SoftInc.Auctions.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
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
                item.SubCategories = new List<SelectListItem>();
                return View(item);
            }

            item.DateCreated = DateTime.Now;
            return await Save<Item>(item);
        }

        public async Task<ActionResult> Edit(long id)
        {
            var data = await _itemManager.Get(m => m.Id == id, m => m.ItemImages);
            return View(data);
        }

        public async Task<ActionResult> Save(ItemModel model)
        {
            return await Save<Item>(model);
        }

        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = await _itemManager.Get(m => m.Id == id, m => m.ItemImages);

            if (item == null)
            {
                return HttpNotFound();
            }

            var result = Mapper.Map<ItemModel>(item);
            result.Images = item.ItemImages.Select(x => x.ThumbImageString).ToList();

            return View(result);
        }

        public async Task<ActionResult> UploadItemImages(ItemModel model)
        {
            if (model.UploadFiles.Count == 0)
            {
                ViewBag.UploadStatus = "Please select files to upload";
                return RedirectToAction("Details", new { id = model.Id });
            }

            var lsImages = new List<ItemImage>();
            //item.ItemImages = item.ItemImages ?? new List<ItemImages>();

            foreach (var file in model.UploadFiles)
            {
                Image img;
                Image imgThumb; //img.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);

                var imgStr = string.Empty;
                var thumbImgStr = string.Empty;

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    imgStr = Convert.ToBase64String(array);
                    img = Image.FromStream(ms);
                }

                using (var ms = new MemoryStream())
                {
                    imgThumb = img.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                    imgThumb.Save(ms, ImageFormat.Png);
                    byte[] array = ms.GetBuffer();
                    thumbImgStr = Convert.ToBase64String(array);
                }

                lsImages.Add(new ItemImage { ImageString = imgStr, ThumbImageString = thumbImgStr, DateCreated = DateTime.Now, DateModified = DateTime.Now, ItemId = model.Id });

            }

            if (lsImages.Count > 0)
            {
                var i = await _itemImagesManager.SaveAll(lsImages);
            }

            ViewBag.UploadStatus = model.UploadFiles.Count.ToString() + " files uploaded successfully.";
            // after successfully uploading redirect the user
            return RedirectToAction("Details", new { id = model.Id });
        }

        public async Task<ActionResult> Delete(long id)
        {
            var bln = await _itemManager.Delete(m => m.Id == id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetSubCategories(string id)
        {
            return FilterSubCategories(id);
        }
    }
}