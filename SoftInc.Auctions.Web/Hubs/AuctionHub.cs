using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using SoftInc.Auctions.Business.Ef;
using SoftInc.Auctions.Business.Managers;

namespace SoftInc.Auctions.Web.Hubs
{
    public class AuctionHub : Hub
    {
        IRepository<Bidding> _biddingManager;

        public AuctionHub()
        {
            _biddingManager = new DataManager<Bidding>();
        }
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public void Bid(string name, string bidderId, string itemId, string amount)
        {
            long bId;
            long itmId;
            decimal amt;
            decimal? maxAmt = null;
            var msg = string.Empty;

            if (long.TryParse(bidderId, out bId) && long.TryParse(itemId, out itmId) && decimal.TryParse(amount, out amt))
            {
                var i = Task.Run(async () => await _biddingManager.Search(x => x.ItemId == itmId));
                var itm = i.Result.OrderByDescending(x => x.Amount).FirstOrDefault();
                maxAmt = itm?.Amount.GetValueOrDefault();

                msg = amt > maxAmt ? $"Current Max bid {amt:C}" : $"Your bid of {amt:C} below current max bid {maxAmt:C}";
                maxAmt = amt > maxAmt ? amt : maxAmt;
                //var b = await _
                var b = Task.Run(async () => await _biddingManager.Save(new Bidding { BidderId = bId, Amount = amt, ItemId = itmId, DateCreated = DateTime.Now, DateModified = DateTime.Now }));
                var r = b.Result;
            }
            else
            {
                msg = "Your Bidding session may have expired. Please logout and log back in to continue";
            }

            Clients.All.addNewMessageToPage(name, msg, maxAmt.GetValueOrDefault().ToString("C"));
        }

    }
}