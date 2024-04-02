namespace RedMango_API.Models.Dto
{
    public class AverageRatingDTO
    {
        public int noOneStar { get; set; } = 0;
        public int noTwoStars { get; set; } = 0;
        public int noThreeStars { get; set; } = 0;
        public int noFourStars { get; set; } = 0;
        public int noFiveStars { get; set; } = 0;
        public float averageRating { get; set; } = 0;
        public int totalRating { get; set; } = 0;
    }
}
