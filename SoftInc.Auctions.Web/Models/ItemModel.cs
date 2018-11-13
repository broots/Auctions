using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftInc.Auctions.Web.Models
{
    public class ItemModel
    {
        public long Id { get; set; }

        [Display(Name = "Item Name")]
        [Required]
        public string ItemName { get; set; }

        [Display(Name = "Summary")]
        public string ItemSummary { get; set; }

        [Display(Name = "Description")]
        public string ItemDescription { get; set; }
        public long? AuctionId { get; set; }
        public short? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        [Display(Name = "Reserve Price")]
        public decimal? ReservePrice { get; set; }

        [Display(Name = "Is Sold")]
        public bool? IsSold { get; set; }

        public string CategoryIdSelected
        {
            get { return CategoryId?.ToString(); }
            set { CategoryId = !string.IsNullOrEmpty(value) ? (short?)short.Parse(value) : null; }
        }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public string SubCategoryIdSelected
        {
            get { return SubCategoryId?.ToString(); }
            set { SubCategoryId = !string.IsNullOrEmpty(value) ? (short?)short.Parse(value) : null; }
        }

        public IEnumerable<SelectListItem> SubCategories { get; set; }

        [FileExtensions(Extensions = ".jpg,.jpeg,.png", ErrorMessage = "Incorrect file format")]
        public List<HttpPostedFileBase> UploadFiles { get; set; }

        public List<string> Images { get; set; }
    }
}