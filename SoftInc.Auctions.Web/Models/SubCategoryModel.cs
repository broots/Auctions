using SoftInc.Auctions.Business.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftInc.Auctions.Web.Models
{
    public class SubCategoryModel : ItemSubCategory
    {
        public string CategoryName
        {
            get
            {
                return ItemCategory?.CategoryName;
            }
        }

        public string CategoryIdSelected
        {
            get { return CategoryId?.ToString(); }
            set { CategoryId = !string.IsNullOrEmpty(value) ? (short?)short.Parse(value) : null; }
        }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}