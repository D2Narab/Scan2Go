using System.Text.Json.Serialization;
using Utility.Bases;

namespace Scan2Go.UI.BaseClasses
{
    public class ApiResponse
    {
        [JsonPropertyName("resultObject")]
        public ResultObject ResultObject { get; set; } = new();
    }

    public class ResultObject
    {
        [JsonPropertyName("listCaptionBases")]
        public List<ListCaptionBase> ListCaptionBases { get; set; } = new();

        [JsonPropertyName("listItemBases")]
        public List<dynamic> ListItemBases { get; set; } = new();

        [JsonPropertyName("recordInfo")]
        public string? RecordInfo { get; set; }

        [JsonPropertyName("totalRecordCount")]
        public int TotalRecordCount { get; set; }
    }
}