using H5pDotNet.Types;
using Newtonsoft.Json;

namespace H5pDotNet.Libraries;

public interface ILibraryType<T> : IH5PContent where T : IH5PContent
{
    /// <summary>
    ///     Must be specific: "H5P.Image 1.1" for example.
    /// </summary>
    [JsonProperty("library", NullValueHandling = NullValueHandling.Ignore)]
    public string Library { get; set; }

    [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
    public Metadata Metadata { get; set; }

    [JsonProperty("params", NullValueHandling = NullValueHandling.Ignore)]
    public T Parameters { get; set; }
}