namespace CodingTask.Applications.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using AngleSharp;
    using AngleSharp.Dom;

    using CodingTask.Application.Concrete;
    using CodingTask.Application.DataTypes;

    using HtmlAgilityPack;

    using NUnit.Framework;

    [TestFixture]
    public class TestAlternateHotelInformationService
    {
        private IDocument domDocument;

        private List<AlternateHotelInformationDTO> expectedAlternateHotelData;

        private AlternateHotelInformationService systemUnderTest;

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void GetAlternateHotelInformation_WhenInvoked_ReturnsExpectedAlternateHotelInformation(int counter)
        {
            List<AlternateHotelInformationDTO> actual = InvokeGetAlternateHotelInformation();
            Assert.AreEqual(expectedAlternateHotelData[counter].Description, actual[counter].Description);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void GetAlternateHotelInformation_WhenInvoked_ReturnsExpectedAlternateHotelOVerAllRating(int counter)
        {
            List<AlternateHotelInformationDTO> actual = InvokeGetAlternateHotelInformation();
            Assert.AreEqual(expectedAlternateHotelData[counter].OverAllRating, actual[counter].OverAllRating);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void GetAlternateHotelInformation_WhenInvoked_ReturnsExpectedAlternateHotelRecentBooking(int counter)
        {
            List<AlternateHotelInformationDTO> actual = InvokeGetAlternateHotelInformation();
            Assert.AreEqual(expectedAlternateHotelData[counter].RecentBooking, actual[counter].RecentBooking);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void GetAlternateHotelInformation_WhenInvoked_ReturnsExpectedName(int counter)
        {
            List<AlternateHotelInformationDTO> actual = InvokeGetAlternateHotelInformation();
            Assert.AreEqual(expectedAlternateHotelData[counter].Name, actual[counter].Name);
        }

        [SetUp]
        public void Setup()
        {
            SetupExpectedAlternateHotelData();
            SetupDomDocument();
            CreateSystemUnderTest();
        }

        private void CreateSystemUnderTest()
        {
            systemUnderTest = new AlternateHotelInformationService();
        }

        private string GetHtmlPageBody()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, DefaultData.FolderName, DefaultData.FileName);
            HtmlDocument doc = new HtmlDocument();
            doc.Load(path);

            HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode("/html/body");

            return bodyNode.InnerHtml;
        }

        private List<AlternateHotelInformationDTO> InvokeGetAlternateHotelInformation()
        {
            return systemUnderTest.GetAlternateHotelInformation(domDocument);
        }

        private async void SetupDomDocument()
        {
            IBrowsingContext context = BrowsingContext.New();
            domDocument = await context.OpenNewAsync();
            string htmlPageBody = GetHtmlPageBody();

            domDocument.Body.InnerHtml = htmlPageBody;
        }

        private void SetupExpectedAlternateHotelData()
        {
            expectedAlternateHotelData = new List<AlternateHotelInformationDTO>
                                               {
                                                   new AlternateHotelInformationDTO
                                                       {
                                                           Description = DefaultData.AlternateHotelDescription,
                                                           Name = DefaultData.AlternateHotelName,
                                                           OverAllRating = DefaultData.AlternateHotelRatingAverage,
                                                           Rating = DefaultData.AlternateHotelRating,
                                                           RecentBooking = DefaultData.AlternateHotelRecentBookings,
                                                           ScoreFromReview = DefaultData.AlternateHotelRecentBookings
                                                       },
                                                   new AlternateHotelInformationDTO
                                                       {
                                                           Description = DefaultData.AlternateHotelDescription2,
                                                           Name = DefaultData.AlternateHotelName2,
                                                           OverAllRating = DefaultData.AlternateHotelRatingAverage,
                                                           Rating = DefaultData.AlternateHotelRating2,
                                                           RecentBooking = DefaultData.AlternateHotelRecentBookings2
                                                       },
                                                   new AlternateHotelInformationDTO
                                                       {
                                                           Description = DefaultData.AlternateHotelDescription3,
                                                           Name = DefaultData.AlternateHotelName3,
                                                           OverAllRating = DefaultData.AlternateHotelRatingAverage,
                                                           RecentBooking = DefaultData.AlternateHotelRecentBookings3
                                                       }
                                               };
        }
    }
}