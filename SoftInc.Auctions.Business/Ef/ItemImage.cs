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
    
    public partial class ItemImage
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public string ImageString { get; set; }
        public string ThumbImageString { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    
        public virtual Item Item { get; set; }
    }
}
