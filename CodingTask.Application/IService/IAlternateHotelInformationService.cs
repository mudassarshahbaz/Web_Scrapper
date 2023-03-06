namespace CodingTask.Application.Contracts
{
    using System.Collections.Generic;

    using AngleSharp.Dom;

    using CodingTask.Application.DataTypes;

    public interface IAlternateHotelInformationService
    {
        List<AlternateHotelInformationDTO> GetAlternateHotelInformation(IDocument element);
    }
}