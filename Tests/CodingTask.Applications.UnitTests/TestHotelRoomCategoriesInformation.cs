namespace CodingTask.Applications.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using AngleSharp;
    using AngleSharp.Dom;

    using CodingTask.Application.Concrete;

    using HtmlAgilityPack;

    using NUnit.Framework;

    public class TestHotelRoomCategoriesInformation
    {
        private IDocument domDocument;

        private List<string> expectedRoomCategories;

        private HotelRoomCategoriesInformationService systemUnderTest;

        [Test]
        public void GetRoomCategories_WhenInvoked_ReturnsExpectedCategories()
        {
            List<string> actual = InvokeGetRoomCategories();
            CollectionAssert.AreEqual(expectedRoomCategories, actual);
        }

        [SetUp]
        public void Setup()
        {
            SetupExpectedRoomCategories();
            SetupDomDocument();
            CreateSystemUnderTest();
        }

        private void CreateSystemUnderTest()
        {
            systemUnderTest = new HotelRoomCategoriesInformationService();
        }

        private string GetHtmlPageBody()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, DefaultData.FolderName, DefaultData.FileName);
            HtmlDocument doc = new HtmlDocument();
            doc.Load(path);

            HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode("/html/body");

            return bodyNode.InnerHtml;
        }

        private List<string> InvokeGetRoomCategories()
        {
            return systemUnderTest.GetRoomCategories(domDocument);
        }

        private async void SetupDomDocument()
        {
            IBrowsingContext context = BrowsingContext.New();
            domDocument = await context.OpenNewAsync();
            string hotelHtmlPageBody = GetHtmlPageBody();

            domDocument.Body.InnerHtml = hotelHtmlPageBody;
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
    }
}