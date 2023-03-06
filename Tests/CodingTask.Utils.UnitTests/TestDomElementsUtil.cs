namespace CodingTask.Utils.UnitTests
{
    using System;
    using System.IO;

    using AngleSharp;
    using AngleSharp.Dom;

    using HtmlAgilityPack;

    using NUnit.Framework;

    [TestFixture]
    public class TestDomElementsUtil
    {
        private IDocument domDocument;

        [TestCase(DefaultData.AlternateHotelRecentBookingSelector, 0, DefaultData.AlternativeHotelRecentBookings)]
        [TestCase(DefaultData.AlternateHotelRecentBookingSelector, 1, DefaultData.AlternativeHotelRecentBookings2)]
        [TestCase(DefaultData.AlternateHotelRecentBookingSelector, 2, DefaultData.AlternativeHotelRecentBookings3)]
        public void FindChildElementBySelector_WhenInvokedReturnsExpectedOutputValue(
            string selector,
            int index,
            string expected)
        {
            string actual = InvokeFindChildElementBySelector(selector, index);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(DefaultData.HotelNameSelector, DefaultData.HotelName)]
        public void FindElementBySelector_WhenInvokedReturnsExpectedOutputValue(string selector, string expected)
        {
            string actual = InvokeFindElementBySelector(selector);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(DefaultData.AlternateHotelCountSelector, DefaultData.AlternateHotelsCount)]
        public void FindElementsCountBySelector_WhenInvokedReturnsExpectedOutputValue(string selector, int expected)
        {
            int actual = InvokeFindElementsCountBySelector(selector);
            Assert.AreEqual(expected, actual);
        }

        [SetUp]
        public void Setup()
        {
            SetupDomDocument();
        }

        private string GetHtmlPageBody()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, DefaultData.FolderName, DefaultData.FileName);
            HtmlDocument doc = new HtmlDocument();
            doc.Load(path);

            HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode("/html/body");

            return bodyNode.InnerHtml;
        }

        private string InvokeFindChildElementBySelector(string selector, int index)
        {
            return HtmlElementFinder.FindChildElementBySelector(domDocument, selector, index);
        }

        private string InvokeFindElementBySelector(string selector)
        {
            return HtmlElementFinder.FindElementBySelector(domDocument, selector);
        }

        private int InvokeFindElementsCountBySelector(string selector)
        {
            return HtmlElementFinder.FindElementsCountBySelector(domDocument, selector);
        }

        private async void SetupDomDocument()
        {
            IBrowsingContext context = BrowsingContext.New();
            domDocument = await context.OpenNewAsync();
            string hotelHtmlPageBody = GetHtmlPageBody();

            domDocument.Body.InnerHtml = hotelHtmlPageBody;
        }
    }
}