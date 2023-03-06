namespace CodingTask.Application.Contracts
{
    using System.Collections.Generic;

    using AngleSharp.Dom;

    public interface IHotelRoomCategoriesInformationService
    {
        List<string> GetRoomCategories(IDocument element);
    }
}