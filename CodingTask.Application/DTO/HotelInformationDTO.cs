namespace CodingTask.Application.DataTypes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class HotelInformationDTO
    {
        [JsonProperty(PropertyName = "hotelName")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "classification")]
        public string Classification { get; set; }

        [JsonProperty(PropertyName = "reviewPoints")]
        public string ReviewPoints { get; set; }

        [JsonProperty(PropertyName = "numReviews")]
        public string NumberOfReviews { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "roomCategories")]
        public List<string> RoomCategories { get; set; }

        [JsonProperty(PropertyName = "alternativeHotels")]
        public List<AlternateHotelInformationDTO> HotelAlternate { get; set; }
    }
}