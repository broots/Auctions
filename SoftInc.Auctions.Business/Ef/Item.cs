//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoftInc.Auctions.Business.Ef
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.Biddings = new HashSet<Bidding>();
            this.ItemImages = new HashSet<ItemImage>();
        }
    
        public long Id { get; set; }
        public string ItemName { get; set; }
        public string ItemSummary { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<long> AuctionId { get; set; }
        public Nullable<short> CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<decimal> ReservePrice { get; set; }
        public Nullable<bool> IsSold { get; set; }
    
        public virtual Auction Auction { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bidding> Biddings { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public virtual ItemSubCategory ItemSubCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemImage> ItemImages { get; set; }
    }
}
