using Newtonsoft.Json;

namespace H5pDotNet.Types.Raw;

public class PreloadedDependency
{
    [JsonProperty("machineName")] public string MachineName { get; set; }

    [JsonProperty("majorVersion")] public string MajorVersion { get; set; }

    [JsonProperty("minorVersion")] public string MinorVersion { get; set; }
}