﻿using AutoMapper;
using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Web.Helpers;
using SoftInc.Auctions.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftInc.Auctions.Web.Mapping
{
    public class MappingProfile
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ItemSubCategory, SubCategoryModel>();
                cfg.CreateMap<SubCategoryModel, ItemSubCategory>();
                cfg.CreateMap<ItemModel, Item>();
                cfg.CreateMap<Item, ItemModel>();
                cfg.CreateMap<ExtendedItemModel, Item>();
                cfg.CreateMap<Item, ExtendedItemModel>().IgnoreAllNonExisting();
                cfg.CreateMap<Auction, AuctionModel>();
                cfg.CreateMap<AuctionModel, Auction>();
                cfg.CreateMap<RegisterViewModel, Bidder>();
                cfg.CreateMap<Bidder, RegisterViewModel>();
                cfg.CreateMap<Bidding, BiddingModel>();
                cfg.CreateMap<BiddingModel, Bidding>();
            });
        }
    }
}