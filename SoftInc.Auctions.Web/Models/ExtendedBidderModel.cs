﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftInc.Auctions.Web.Models
{
    public class ExtendedBidderModel : BidderModel
    {
        public List<ExtendedBiddingModel> Biddings { get; set; }
    }
}