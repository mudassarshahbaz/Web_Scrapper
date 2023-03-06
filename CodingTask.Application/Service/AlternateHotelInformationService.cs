namespace CodingTask.Application.Concrete
{
    using System.Collections.Generic;

    using AngleSharp.Dom;

    using CodingTask.Application.Constant;
    using CodingTask.Application.Contracts;
    using CodingTask.Application.DataTypes;
    using CodingTask.Utils;

    public class AlternateHotelInformationService : IAlternateHotelInformationService
    {
        public List<AlternateHotelInformationDTO> GetAlternateHotelInformation(IDocument document)
        {
            List<AlternateHotelInformationDTO> alternateHotels = new List<AlternateHotelInformationDTO>();
            int alternateHotelElements = HtmlElementFinder.FindElementsCountBySelector(
                document,
                HotelCssSelector.AlternateHotelCount);

            for (int counter = 0; counter < alternateHotelElements; counter++)
            {
                AlternateHotelInformationDTO hotelAlternate = new AlternateHotelInformationDTO
                                                    {
                                                        Name = HtmlElementFinder.FindChildElementBySelector(
                                                            document,
                                                            HotelCssSelector.AlternativeHotelName,
                                                            counter),
                                                        Rating = HtmlElementFinder.FindChildElementBySelector(
                                                            document,
                                                            HotelCssSelector.AlternativeHotelRating,
                                                            counter),
                                                        Description = HtmlElementFinder.FindChildElementBySelector(
                                                            document,
                                                            HotelCssSelector.AlternativeHotelDescription,
                                                            counter),
                                                        RecentBooking = HtmlElementFinder.FindChildElementBySelector(
                                                            document,
                                                            HotelCssSelector.AlternativeHotelRecentBookings,
                                                            counter),
                                                        ScoreFromReview = HtmlElementFinder.FindChildElementBySelector(
                                                            document,
                                                            HotelCssSelector.AlternativeScoreFromReview,
                                                            counter),
                                                        OverAllRating = HtmlElementFinder.FindChildElementBySelector(
                                                            document,
                                                            HotelCssSelector.AlternativeHotelOVerAllRating,
                                                            counter)
                                                    };
                alternateHotels.Add(hotelAlternate);
            }

            return alternateHotels;
        }
    }
}