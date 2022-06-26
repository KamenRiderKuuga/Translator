using System.Text.Json.Serialization;

namespace Translator.Shared
{
    public partial class BaiduTranslateOutput
    {
        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("trans_result")]
        public TransResult[] TransResult { get; set; }

        [JsonPropertyName("error_code")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("error_msg")]
        public string ErrorMsg { get; set; }
    }

    public partial class TransResult
    {
        [JsonPropertyName("src")]
        public string Src { get; set; }

        [JsonPropertyName("dst")]
        public string Dst { get; set; }
    }
}
