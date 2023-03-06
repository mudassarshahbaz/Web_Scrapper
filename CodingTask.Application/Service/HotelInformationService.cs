namespace CodingTask.Application.Concrete
{
    using System;
    using System.Threading.Tasks;

    using AngleSharp;
    using AngleSharp.Dom;

    using CodingTask.Application.Constant;
    using CodingTask.Application.Contracts;
    using CodingTask.Application.DataTypes;
    using CodingTask.Utils;

    public class HotelInformationService : IHotelInformationService
    {
        private readonly string classificationIdentifier = "-star";

        private readonly IAlternateHotelInformationService hotelAlternativeDetailProvider;

        private readonly IHotelHtmlService hotelHtmlProvider;

        private readonly IHotelOutputJsonService hotelOutputJsonProvider;

        private readonly IHotelRoomCategoriesInformationService hotelRoomCategoriesProvider;

        public HotelInformationService(
            IHotelHtmlService hotelHtmlProvider,
            IHotelRoomCategoriesInformationService hotelRoomCategoriesProvider,
            IAlternateHotelInformationService hotelAlternativeDetailProvider,
            IHotelOutputJsonService hotelOutputJsonProvider)
        {
            this.hotelAlternativeDetailProvider = hotelAlternativeDetailProvider;
            this.hotelRoomCategoriesProvider = hotelRoomCategoriesProvider;
            this.hotelHtmlProvider = hotelHtmlProvider;
            this.hotelOutputJsonProvider = hotelOutputJsonProvider;
        }

        public async Task<string> GetHotelDetail()
        {
            try
            {
                IBrowsingContext context = BrowsingContext.New();
                IDocument document = await context.OpenNewAsync();
                string hotelHtmlPageBody = hotelHtmlProvider.GetHotelHtmlBody();

                document.Body.InnerHtml = hotelHtmlPageBody;
                HotelInformationDTO hotelData = GetHotelDetailDto(document);
                return hotelOutputJsonProvider.GetHotelOutPutJson(hotelData);
            }
            catch (Exception exception)
            {
                throw new Exception(ErrorMessage.MsgErrorOccurred, exception);
            }
        }

        private string GetClassificationFromDescription(string description)
        {
            int index = description.IndexOf(classificationIdentifier, StringComparison.Ordinal);

            return index > 0 ? description.Substring(index - 1, 1) : string.Empty;
        }

        private HotelInformationDTO GetHotelDetailDto(IDocument document)
        {
            string description = HtmlElementFinder.FindElementBySelector(document, HotelCssSelector.HotelDescription);
            return new HotelInformationDTO
            {
                           ReviewPoints = HtmlElementFinder.FindElementBySelector(
                               document,
                               HotelCssSelector.HotelReviewPoints),
                           Address = HtmlElementFinder.FindElementBySelector(document, HotelCssSelector.HotelAddress),
                           HotelAlternate = hotelAlternativeDetailProvider.GetAlternateHotelInformation(document),
                           Name = HtmlElementFinder.FindElementBySelector(document, HotelCssSelector.HotelName),
                           NumberOfReviews = HtmlElementFinder.FindElementBySelector(
                               document,
                               HotelCssSelector.HotelNumberOfReviews),
                           RoomCategories = hotelRoomCategoriesProvider.GetRoomCategories(document),
                           Classification = GetClassificationFromDescription(description), Description = description
                       };
        }
    }
}