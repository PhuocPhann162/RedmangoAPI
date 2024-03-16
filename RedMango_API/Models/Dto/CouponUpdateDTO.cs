namespace RedMango_API.Models.Dto
{
    public class CouponUpdateDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
