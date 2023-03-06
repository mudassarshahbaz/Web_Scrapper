namespace CodingTask.Application.Constant
{
    public class HotelCssSelector
    {
        public const string AlternateHotelCount = "#althotelsRow>td";

        public const string AlternativeHotelDescription = "#althotelsTable>tbody>tr>td>.hp_compset_description";

        public const string AlternativeHotelName = "#althotelsTable>tbody>tr>td>p>a";

        public const string AlternativeHotelOVerAllRating = "span.best";

        public const string AlternativeHotelRating = ".hp_review_score>.notranslate>span.average";

        public const string AlternativeHotelRecentBookings =
            "#althotelsTable>tbody>tr>td>.altHotels_most_recent_booking";

        public const string AlternativeScoreFromReview = "#althotelsTable>tbody>tr>td>.alt_hotels_info_row>span>strong";

        public const string HotelAddress = "#hp_address_subtitle";

        public const string HotelDescription = "#summary>p";

        public const string HotelName = "#hp_hotel_name";

        public const string HotelNumberOfReviews = ".out_of>.best";

        public const string HotelRating = "#althotelsTable>tbody>tr>td>.althotels-name>i";

        public const string HotelReviewPoints = ".average.js--hp-scorecard-scoreval";

        public const string HotelRoomCategoriesCount = ".ftd";

        public const string HotelRoomCategory = ".ftd";
    }
}