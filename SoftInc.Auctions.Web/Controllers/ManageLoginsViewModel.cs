using System.Collections.Generic;
using Microsoft.Owin.Security;

namespace SoftInc.Auctions.Web.Controllers
{
    internal class ManageLoginsViewModel
    {
        public object CurrentLogins { get; set; }
        public List<AuthenticationDescription> OtherLogins { get; set; }
    }
}