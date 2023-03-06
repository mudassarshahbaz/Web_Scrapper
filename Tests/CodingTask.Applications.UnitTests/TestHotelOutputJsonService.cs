namespace CodingTask.Applications.UnitTests
{
    using System.Collections.Generic;

    using CodingTask.Application.Concrete;
    using CodingTask.Application.DataTypes;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class TestHotelOutputJsonService
    {
        private string expectedJson;

        private HotelInformationDTO hotelDetail;

        private HotelOutputJsonService systemUnderTest;

        [Test]
        public void GetHotelOutPutJson_WhenInvoked_ReturnsExpectedOutputJson()
        {
            string actual = InvokeGetHotelOutPutJson();
            Assert.That(actual, Is.EqualTo(expectedJson));
        }

        [Test]
        public void GetHotelOutPutJson_WhenInvokedWithHotelInfoAsNull_ReturnsOutputJsonAsNull()
        {
            hotelDetail = null;
            string actual = InvokeGetHotelOutPutJson();
            Assert.That(actual, Is.EqualTo(DefaultData.NullJson));
        }

        [SetUp]
        public void Setup()
        {
            SetupHotelDetail();
            SetupExpectedJson();
            CreateSystemUnderTest();
        }

        private void CreateSystemUnderTest()
        {
            systemUnderTest = new HotelOutputJsonService();
        }

        private string InvokeGetHotelOutPutJson()
        {
            return systemUnderTest.GetHotelOutPutJson(hotelDetail);
        }

        private void SetupExpectedJson()
        {
            expectedJson = JsonConvert.SerializeObject(hotelDetail, Formatting.Indented);
        }

        private void SetupHotelDetail()
        {
            hotelDetail = new HotelInformationDTO
                              {
                                  Address = DefaultData.HotelAddress, Classification = DefaultData.Classification,
                                  Description = DefaultData.Description,
                                  HotelAlternate = new List<AlternateHotelInformationDTO>
                                                       {
                                                           new AlternateHotelInformationDTO
                                                               {
                                                                   Description = DefaultData.AlternativeHotelDescription,
                                                                   Name = DefaultData.AlternativeHotelName,
                                                                   OverAllRating = DefaultData
                                                                       .AlternativeHotelOverAllReviewScore,
                                                                   Rating = DefaultData.AlternativeHotelRating,
                                                                   RecentBooking =
                                                                       DefaultData.AlternativeHotelRecentBookings,
                                                                   ScoreFromReview = DefaultData
                                                                       .AlternativeHotelOverAllReviewScore
                                                               }
                                                       },
                                  Name = DefaultData.HotelName, NumberOfReviews = DefaultData.HotelNumberOfReviews,
                                  ReviewPoints = DefaultData.HotelReviewPoints,
                                  RoomCategories = new List<string>
                                                       {
                                                           DefaultData.RoomCategories1, DefaultData.RoomCategories2,
                                                           DefaultData.RoomCategories3
                                                       }
                              };
        }
    }
}