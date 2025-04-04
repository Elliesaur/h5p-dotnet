using Newtonsoft.Json;

namespace H5pDotNet.Types.Raw;

public class CopyrightInformation
{
    [JsonProperty("license")] public string License { get; set; }

    [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
    public string Source { get; set; }

    [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
    public string Author { get; set; }

    [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
    public string Title { get; set; }

    [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
    public string Version { get; set; }
}