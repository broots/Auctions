﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using SoftInc.Auctions.Web.Mapping;

[assembly: OwinStartup(typeof(SoftInc.Auctions.Web.Startup))]

namespace SoftInc.Auctions.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
            MappingProfile.Configure();
        }
    }
}
