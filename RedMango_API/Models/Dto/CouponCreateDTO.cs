namespace RedMango_API.Models.Dto
{
    public class CouponCreateDTO
    {
        public string Code { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
        public int Expiration { get; set; }
    }
}
