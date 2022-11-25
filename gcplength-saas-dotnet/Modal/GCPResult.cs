using System.Text.Json.Serialization;

namespace gcplength_saas_dotnet.Modal
{
    [Serializable]
    public class GCPResultData
    {
        [JsonPropertyName("GCP")]
        public string GCP { get; set; } = "";

        [JsonPropertyName("Length")]
        public int Length { get; set; } = 0;

        public GCPResultData(string gCP, int length)
        {
            GCP = gCP;
            Length = length;
        }
    }

    [Serializable]
    public class GCPResult
    {

        public bool isOK { get; set; } = false;
        public string status { get; set; } = "";

        public string message { get; set; } = "";

        public string[]? errors { get; set; } = null;

        public GCPResultData? data { get; set; } = null;

        public GCPResult(bool isOK, string status, string message, string[]? errors, GCPResultData? data)
        {
            this.isOK = isOK;
            this.status = status;
            this.message = message;
            this.errors = errors;
            this.data = data;
        }
    }
}
