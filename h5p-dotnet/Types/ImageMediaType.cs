using Newtonsoft.Json;

namespace H5pDotNet.Types;

public class ImageMediaType : MediaType
{
    [JsonProperty("width")] public int Width { get; set; }

    [JsonProperty("height")] public int Height { get; set; }
}