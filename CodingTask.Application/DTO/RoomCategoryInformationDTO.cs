using Newtonsoft.Json;

namespace CodingTask.Application.DataTypes
{
    public class RoomCategoryInformationDTO
    {
        [JsonProperty(PropertyName = "hotelName")]
        public string Name { get; set; }
    }
}