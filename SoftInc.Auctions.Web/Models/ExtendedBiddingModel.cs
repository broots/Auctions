using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftInc.Auctions.Web.Models
{
    public class ExtendedBiddingModel : BiddingModel
    {
        public ItemModel Item { get; set; }
    }
}