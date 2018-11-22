namespace SoftInc.Auctions.Web.Models
{
    public class ItemImageModel : BaseModel
    {
        public long ItemId { get; set; }
        public string ImageString { get; set; }
        public string ThumbImageString { get; set; }
    }
}