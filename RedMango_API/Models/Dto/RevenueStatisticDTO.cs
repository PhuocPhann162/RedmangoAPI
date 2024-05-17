namespace RedMango_API.Models.Dto
{
    public class RevenueStatisticDTO
    {
        public int? DaysInMonth { get; set; }
        public string? Label { get; set; }
        public List<double>? RevenueData { get; set; }
    }
}
