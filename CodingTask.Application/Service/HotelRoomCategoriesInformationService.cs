namespace CodingTask.Application.Concrete
{
    using System.Collections.Generic;

    using AngleSharp.Dom;

    using CodingTask.Application.Constant;
    using CodingTask.Application.Contracts;
    using CodingTask.Utils;

    public class HotelRoomCategoriesInformationService : IHotelRoomCategoriesInformationService
    {
        public List<string> GetRoomCategories(IDocument element)
        {
            List<string> roomCategories = new List<string>();
            
            int categoriesCount = HtmlElementFinder.FindElementsCountBySelector(
                element,
                HotelCssSelector.HotelRoomCategoriesCount);

            for (int categoriesCounter = 0; categoriesCounter < categoriesCount; categoriesCounter++)
            {
                string roomCategory = HtmlElementFinder.FindChildElementBySelector(
                    element,
                    HotelCssSelector.HotelRoomCategory,
                    categoriesCounter);

                roomCategories.Add(roomCategory);
            }

            return roomCategories;
        }
    }
}