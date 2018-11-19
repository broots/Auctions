using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftInc.Auctions.Web.Models
{
    public class ExtendedItemModel : ItemModel
    {
        public List<BiddingModel> Biddings { get; set; }
    }
}