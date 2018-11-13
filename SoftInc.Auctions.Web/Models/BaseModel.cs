using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftInc.Auctions.Web.Models
{
    public class BaseModel
    {
        public long Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}