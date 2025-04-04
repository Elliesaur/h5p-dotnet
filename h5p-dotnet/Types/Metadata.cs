using Newtonsoft.Json;

namespace H5pDotNet.Types;

public class Metadata
{
    [JsonProperty("contentType", NullValueHandling = NullValueHandling.Ignore)]
    public string ContentType { get; set; }

    [JsonProperty("license", NullValueHandling = NullValueHandling.Ignore)]
    public string License { get; set; }

    [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
    public string Title { get; set; }

    [JsonProperty("authors", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Authors { get; set; } = new();

    [JsonProperty("changes", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Changes { get; set; } = new();
}