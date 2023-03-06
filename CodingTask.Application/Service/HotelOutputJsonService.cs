namespace CodingTask.Application.Concrete
{
    using CodingTask.Application.Contracts;

    using Newtonsoft.Json;

    public class HotelOutputJsonService : IHotelOutputJsonService
    {
        public string GetHotelOutPutJson(object hotelDetail)
        {
            return GetSerializedJson(hotelDetail);
        }

        private string GetSerializedJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}