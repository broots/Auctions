using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftInc.Auctions.Web.Models
{
    public class AuctionModel : BaseModel
    {
        [Required, Display(Name = "Auction Name")]
        public string AuctionName { get; set; }

        public string Summary { get; set; }
        public string Description { get; set; }

        [Display(Name = "Start Date"), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Start Time")]
        public TimeSpan? StartTime { get; set; }

        public string StartTimeSelected
        {
            get { return StartTime?.ToString(); }
            set
            {
                var t = TimeSpan.Parse(value);
                StartTime = t;
            }
        }

        public List<ItemModel> Items { get; set; }

        public List<string> UnAuctionedItemsSelected { get; set; }

        [Display(Name = "Available Items")]
        public IEnumerable<SelectListItem> UnAuctionedItems { get; set; }

        [Display(Name = "End Time")]
        public TimeSpan? EndTime { get; set; }
    }
}