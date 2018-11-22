using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoftInc.Auctions.Web.Models
{
    public class BiddingModel
    {
        public long ItemId { get; set; }
        public long? BidderId { get; set; }

        [Display(Name = "Bid Amount"), DisplayFormat(DataFormatString = "GH₵{0:#,##0.00}")]
        public decimal? Amount { get; set; }
    }
}