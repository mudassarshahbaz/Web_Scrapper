namespace CodingTask.Application.Concrete
{
    using System;
    using System.IO;

    using CodingTask.Application.Constant;
    using CodingTask.Application.Contracts;

    using HtmlAgilityPack;

    using Microsoft.Extensions.Configuration;

    public class HotelHtmlService : IHotelHtmlService
    {
        private readonly IConfiguration configuration;

        public HotelHtmlService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetHotelHtmlBody()
        {
            try
            {
                string fileName = configuration.GetSection("ScrapFileDetails:FileName").Value;
                string folderName = configuration.GetSection("ScrapFileDetails:FolderName").Value;

                string path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory ?? throw new Exception(ErrorMessage.MsgDirectory),
                    folderName,
                    fileName);

                HtmlDocument doc = new HtmlDocument();
                doc.Load(path);

                HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode("/html/body");
                return bodyNode.InnerHtml;
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(ErrorMessage.MsgLoadFile, exception);
            }
        }
    }
}