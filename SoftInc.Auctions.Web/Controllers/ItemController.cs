using AutoMapper;
using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Business.Managers;
using SoftInc.Auctions.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SoftInc.Auctions.Web.Controllers
{
    public class ItemController : BaseApiController<Item>
    {
        public async Task<ExtendedItemModel> GetItemAndBids(long itemId)
        {
            var data = await GetById<Item>(m => m.Id == itemId, m => m.Biddings);
            var result = Mapper.Map<ExtendedItemModel>(data);

            return result;
        }
    }
}
