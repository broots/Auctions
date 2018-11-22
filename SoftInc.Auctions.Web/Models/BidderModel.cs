using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftInc.Auctions.Web.Models
{
    public class BidderModel : BaseModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public DateTime DoB { get; set; }
        public string ContactNumber { get; set; }
        public string ContactNumber2 { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
        public string VerificationIDString { get; set; }
        public string UserId { get; set; }
    }
}