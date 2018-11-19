using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftInc.Auctions.Web.Models
{
    public class BiddingModel
    {
        public long ItemId { get; set; }
        public long? BidderId { get; set; }
        public decimal? Amount { get; set; }
    }
}