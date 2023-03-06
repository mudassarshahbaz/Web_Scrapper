namespace CodingTask.Applications.UnitTests
{
    using System;
    using System.IO;

    using AngleSharp;
    using AngleSharp.Dom;

    using CodingTask.Application.Concrete;
    using CodingTask.Application.Constant;

    using HtmlAgilityPack;

    using NSubstitute;

    using NUnit.Framework;

    using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

    [TestFixture]
    public class TestHotelHtmlProvider
    {
        private IConfiguration configurationMock;

        private IDocument domDocument;

        private string expectedHtmlBody;

        private HotelHtmlService systemUnderTest;

        [Test]
        public void GetHotelHtmlBody_WhenFolderPathIsEmpty_ThrowsExceptionWithExpectedDescription()
        {
            SetupFolderAndFileNameAsEmpty();
            Assert.Throws(
                Is.TypeOf<Exception>().And.Message.EqualTo(ErrorMessage.MsgLoadFile),
                () => InvokeGetHotelHtmlBody());
        }

        [Test]
        public void GetHotelHtmlBody_WhenFolderPathIsInvalid_ThrowsExceptionWithExpectedDescription()
        {
            SetupInvalidPath();
            Assert.Throws(
                Is.TypeOf<Exception>().And.Message.EqualTo(ErrorMessage.MsgLoadFile),
                () => InvokeGetHotelHtmlBody());
        }

        [Test]
        public void GetHotelHtmlBody_WhenInvoked_ReturnsExpectedHtmlBody()
        {
            string actual = InvokeGetHotelHtmlBody();
            Assert.AreEqual(expectedHtmlBody, actual);
        }

        [Test]
        public void GetHotelHtmlBody_WhenResponseIsValid_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() => InvokeGetHotelHtmlBody());
        }

        [SetUp]
        public void Setup()
        {
            SetupExpectedHtmlBody();
            SetupDomDocument();
            CreateMock();
            CreateMockReturns();
            CreateSystemUnderTest();
        }

        private void CreateMock()
        {
            configurationMock = Substitute.For<IConfiguration>();
        }

        private void CreateMockReturns()
        {
            configurationMock.GetSection("ScrapFileDetails:FileName").Value.Returns(DefaultData.FileName);
            configurationMock.GetSection("ScrapFileDetails:FolderName").Value.Returns(DefaultData.FolderName);
        }

        private void CreateSystemUnderTest()
        {
            systemUnderTest = new HotelHtmlService(configurationMock);
        }

        private string GetHtmlPageBody()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, DefaultData.FolderName, DefaultData.FileName);
            HtmlDocument doc = new HtmlDocument();
            doc.Load(path);

            HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode("/html/body");

            return bodyNode.InnerHtml;
        }

        private string InvokeGetHotelHtmlBody()
        {
            return systemUnderTest.GetHotelHtmlBody();
        }

        private async void SetupDomDocument()
        {
            IBrowsingContext context = BrowsingContext.New();
            domDocument = await context.OpenNewAsync();
            string hotelHtmlPageBody = GetHtmlPageBody();

            domDocument.Body.InnerHtml = hotelHtmlPageBody;
        }

        private void SetupExpectedHtmlBody()
        {
            expectedHtmlBody = GetHtmlPageBody();
        }

        private void SetupFolderAndFileNameAsEmpty()
        {
            configurationMock.GetSection("ScrapFileDetails:FileName").Value.Returns(string.Empty);
            configurationMock.GetSection("ScrapFileDetails:FolderName").Value.Returns(string.Empty);
        }

        private void SetupInvalidPath()
        {
            configurationMock.GetSection("ScrapFileDetails:FolderName").Value.Returns(DefaultData.ScrapDataFolderName);
        }
    }
}