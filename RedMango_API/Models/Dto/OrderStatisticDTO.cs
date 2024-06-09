namespace RedMango_API.Models.Dto
{
    public class OrdersStatisticDTO
    {
        public int? DaysInMonth { get; set; }
        public string? Label { get; set; }
        public List<double>? OrdersData { get; set; }
    }
}
