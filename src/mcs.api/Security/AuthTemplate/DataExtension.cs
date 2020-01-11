using System.Text.Json.Serialization;

namespace mcs.api.Security.AuthTemplate
{
    public class DataExtension
    {
        [JsonIgnore]
        public int TokenKey_Id { get; set; }
        [JsonIgnore]
        public int UserAccount_Id { get; set; }
        [JsonIgnore]
        public bool Active { get; set; }
        [JsonIgnore]
        public int Database_Id {get;set;}
    }
}