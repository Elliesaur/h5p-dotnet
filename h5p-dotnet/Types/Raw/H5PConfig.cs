using Newtonsoft.Json;

namespace H5pDotNet.Types.Raw;

public class H5PConfig
{
    [JsonProperty("title")] public string Title { get; set; }

    [JsonProperty("language")] public string Language { get; set; }

    [JsonProperty("mainLibrary")] public string MainLibrary { get; set; }

    [JsonProperty("embedTypes")] public List<string> EmbedTypes { get; set; }

    [JsonProperty("license")] public string License { get; set; }

    [JsonProperty("defaultLanguage")] public string DefaultLanguage { get; set; }

    [JsonProperty("preloadedDependencies")]
    public List<PreloadedDependency> PreloadedDependencies { get; set; }
}