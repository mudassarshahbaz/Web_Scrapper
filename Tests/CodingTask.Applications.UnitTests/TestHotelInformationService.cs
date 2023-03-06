namespace CodingTask.Applications.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using AngleSharp;
    using AngleSharp.Dom;

    using CodingTask.Application.Concrete;
    using CodingTask.Application.Constant;
    using CodingTask.Application.Contracts;
    using CodingTask.Application.DataTypes;

    using HtmlAgilityPack;

    using Newtonsoft.Json;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using NUnit.Framework;

    [TestFixture]
    public class TestHotelInformationService
    {
        private IDocument domDocument;

        private List<AlternateHotelInformationDTO> expectedAlternativeHotelData;

        private string expectedHtmlBody;

        private string expectedJson;

        private List<string> expectedRoomCategories;

        private IAlternateHotelInformationService hotelAlternativeDetailProviderMock;

        private HotelInformationDTO hotelDetail;

        private IHotelHtmlService hotelHtmlProviderMock;

        private IHotelOutputJsonService hotelOutputJsonProviderMock;

        private IHotelRoomCategoriesInformationService hotelRoomCategoriesProviderMock;

        private HotelInformationService systemUnderTest;

        [Test]
        public void GetHotelDetail_WhenInvoked_ReturnsExpectedOutputJson()
        {
            Task<string> actual = InvokeGetHotelDetail();
            Assert.That(actual.Result, Is.EqualTo(expectedJson));
        }

        [Test]
        public void GetHotelDetail_WhenResponseIsValid_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() => InvokeGetHotelDetail());
        }

        [Test]
        public void GetHotelDetail_WhenRoomCategoriesThrowException_ReturnsExpectedExceptionWithMessage()
        {
            MakeRoomCategoriesThrowException();

            Task<string> actual = InvokeGetHotelDetail();

            if (actual.Exception?.InnerException != null)
            {
                Assert.AreEqual(ErrorMessage.MsgErrorOccurred, actual.Exception.InnerException.Message);
            }
        }

        [SetUp]
        public void Setup()
        {
            SetupExpectedRoomCategories();
            SetupExpectedAlternativeHotelData();
            SetupHotelDetail();
            SetupExpectedJson();
            SetupExpectedHtmlBody();
            SetupDomDocument();
            CreateMock();

            CreateMockReturns();
            CreateSystemUnderTest();
        }

        [Test]
        public void ValidateResponse_HotelAlternativeDetailThrowsException_ReturnsExpectedExceptionWithMessage()
        {
            MakeHotelAlternativeDetailProviderThrowException();

            Task<string> actual = InvokeGetHotelDetail();

            if (actual.Exception?.InnerException != null)
            {
                Assert.AreEqual(ErrorMessage.MsgErrorOccurred, actual.Exception.InnerException.Message);
            }
        }

        [Test]
        public void ValidateResponse_WhenOutPutJsonProviderThrowsException_ReturnsExpectedExceptionWithMessage()
        {
            MakeOutputJsonProviderThrowException();

            Task<string> actual = InvokeGetHotelDetail();

            if (actual.Exception?.InnerException != null)
            {
                Assert.AreEqual(ErrorMessage.MsgErrorOccurred, actual.Exception.InnerException.Message);
            }
        }

        [Test]
        public void ValidateResponse_WhenProviderThrowException_ReturnsExpectedExceptionWithMessage()
        {
            MakeHtmlProviderThrowException();

            Task<string> actual = InvokeGetHotelDetail();

            if (actual.Exception?.InnerException != null)
            {
                Assert.AreEqual(ErrorMessage.MsgErrorOccurred, actual.Exception.InnerException.Message);
            }
        }

        private void CreateMock()
        {
            hotelAlternativeDetailProviderMock = Substitute.For<IAlternateHotelInformationService>();
            hotelHtmlProviderMock = Substitute.For<IHotelHtmlService>();
            hotelOutputJsonProviderMock = Substitute.For<IHotelOutputJsonService>();
            hotelRoomCategoriesProviderMock = Substitute.For<IHotelRoomCategoriesInformationService>();
        }

        private void CreateMockReturns()
        {
            hotelAlternativeDetailProviderMock.GetAlternateHotelInformation(domDocument)
                .Returns(expectedAlternativeHotelData);

            hotelHtmlProviderMock.GetHotelHtmlBody().Returns(expectedHtmlBody);
            hotelOutputJsonProviderMock.GetHotelOutPutJson(Arg.Any<object>()).Returns(expectedJson);
            hotelRoomCategoriesProviderMock.GetRoomCategories(domDocument).Returns(expectedRoomCategories);
        }

        private void CreateSystemUnderTest()
        {
            systemUnderTest = new HotelInformationService(
                hotelHtmlProviderMock,
                hotelRoomCategoriesProviderMock,
                hotelAlternativeDetailProviderMock,
                hotelOutputJsonProviderMock);
        }

        private string GetHtmlPageBody()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, DefaultData.FolderName, DefaultData.FileName);
            HtmlDocument doc = new HtmlDocument();
            doc.Load(path);

            HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode("/html/body");

            return bodyNode.InnerHtml;
        }

        private Task<string> InvokeGetHotelDetail()
        {
            return systemUnderTest.GetHotelDetail();
        }

        private void MakeHotelAlternativeDetailProviderThrowException()
        {
            hotelAlternativeDetailProviderMock.GetAlternateHotelInformation(domDocument).Throws(new Exception());
        }

        private void MakeHtmlProviderThrowException()
        {
            hotelHtmlProviderMock.GetHotelHtmlBody().Throws(new Exception());
        }

        private void MakeOutputJsonProviderThrowException()
        {
            hotelOutputJsonProviderMock.GetHotelOutPutJson(domDocument).Throws(new Exception());
        }

        private void MakeRoomCategoriesThrowException()
        {
            hotelRoomCategoriesProviderMock.GetRoomCategories(domDocument).Throws(new Exception());
        }

        private async void SetupDomDocument()
        {
            IBrowsingContext context = BrowsingContext.New();
            domDocument = await context.OpenNewAsync();
            string hotelHtmlPageBody = GetHtmlPageBody();

            domDocument.Body.InnerHtml = hotelHtmlPageBody;
        }

        private void SetupExpectedAlternativeHotelData()
        {
            expectedAlternativeHotelData = new List<AlternateHotelInformationDTO>
                                               {
                                                   new AlternateHotelInformationDTO
                                                       {
                                                           Description = DefaultData.AlternativeHotelDescription,
                                                           Name = DefaultData.AlternativeHotelName,
                                                           OverAllRating = DefaultData.AlternativeHotelOverAllReviewScore,
                                                           Rating = DefaultData.AlternativeHotelRating,
                                                           RecentBooking = DefaultData.AlternativeHotelRecentBookings,
                                                           ScoreFromReview = DefaultData.HotelNumberOfReviews
                                                       }
                                               };
        }

        private void SetupExpectedHtmlBody()
        {
            expectedHtmlBody = GetHtmlPageBody();
        }

        private void SetupExpectedJson()
        {
            expectedJson = JsonConvert.SerializeObject(hotelDetail, Formatting.Indented);
        }

        private void SetupExpectedRoomCategories()
        {
            expectedRoomCategories = new List<string>
                                         {
                                             DefaultData.RoomCategories1, DefaultData.RoomCategories2,
                                             DefaultData.RoomCategories3, DefaultData.RoomCategories4,
                                             DefaultData.RoomCategories5, DefaultData.RoomCategories6,
                                             DefaultData.RoomCategories7
                                         };
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