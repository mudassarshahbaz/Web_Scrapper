namespace CodingTask.Web.Controllers
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using CodingTask.Application.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class WebExtractionController : Controller
    {
        private readonly IHotelInformationService detailProvider;
        private readonly ILogger<WebExtractionController> logger;

        public WebExtractionController(IHotelInformationService detailProvider, ILogger<WebExtractionController> logger)
        {
            this.detailProvider = detailProvider;
            this.logger = logger;
        }

        public async Task<IActionResult> DownloadExtractionFile()
        {
            try
            {
                string hotelDetail = await detailProvider.GetHotelDetail();
                if (string.IsNullOrEmpty(hotelDetail))
                {
                    return BadRequest("Hotel detail not available");
                }

                string fileName = $"HotelDetail_{DateTime.Now:yyyyMMddHHmmss}.txt";
                byte[] bytes = Encoding.UTF8.GetBytes(hotelDetail);

                string tempFilePath = Path.GetTempFileName();
                await System.IO.File.WriteAllBytesAsync(tempFilePath, bytes);

                var output = new FileStreamResult(new FileStream(tempFilePath, FileMode.Open), "application/octet-stream")
                {
                    FileDownloadName = fileName
                };

                return output;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while downloading extracted data");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}