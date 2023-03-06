namespace CodingTask.Application.Contracts
{
    using System.Threading.Tasks;

    public interface IHotelInformationService
    {
        Task<string> GetHotelDetail();
    }
}