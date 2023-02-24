using Newtonsoft.Json;


namespace Moor.Model.Utilities
{
    public class DataResult
    {
        public bool IsSuccess { get; set; } = false;
        public long PkId { get; set; }

        [JsonProperty("ErrorMessageList", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ErrorMessages { get; set; } = new List<string> { };
        public string ErrorMessage { get; set; }
        //public BaseModel Data { get; set; }
    }
}
