using Newtonsoft.Json;

namespace H5pDotNet.Libraries;

public class H5PText : IH5PContent
{
    [JsonProperty("text")] public string Text { get; set; }
}