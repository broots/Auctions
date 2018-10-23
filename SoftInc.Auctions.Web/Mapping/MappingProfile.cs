﻿using AutoMapper;
using SoftInc.Auctions.Business.Ef;
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
            });
        }
    }
}