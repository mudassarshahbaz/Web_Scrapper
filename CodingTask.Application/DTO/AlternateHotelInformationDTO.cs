namespace CodingTask.Application.DataTypes
{
    using Newtonsoft.Json;

    public class AlternateHotelInformationDTO
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "hotelName")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "ratingAverage")]
        public string OverAllRating { get; set; }

        [JsonProperty(PropertyName = "reviewPoints")]
        public string Rating { get; set; }

        [JsonProperty(PropertyName = "recentBookings")]
        public string RecentBooking { get; set; }

        [JsonProperty(PropertyName = "scoreFromReviews")]
        public string ScoreFromReview { get; set; }
    }
}