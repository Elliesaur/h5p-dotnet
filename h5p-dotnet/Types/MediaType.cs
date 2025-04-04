using H5pDotNet.Types.Raw;
using H5pDotNet.Utilities;
using Newtonsoft.Json;

namespace H5pDotNet.Types;

public abstract class MediaType : IH5PContent
{
    [JsonProperty("path")] public string Path { get; set; }
    [JsonProperty("mime")] public string Mime { get; set; }

    [JsonProperty("copyright")] public CopyrightInformation Copyright { get; set; }

    public static async Task<MediaType?> FromLocalFile(H5PPackage package, string file)
    {
        var mimeType = await FileHelper.GetMimeTypeFile(file);
        var filePath = await package.AddFile(file);
        return await GetMediaType(filePath, mimeType);
    }

    public static async Task<MediaType?> FromRemoteFile(H5PPackage package, string url)
    {
        var mimeType = await FileHelper.GetMimeTypeUrl(url);
        var data = await FileHelper.DownloadFile(url);
        var filePath = await package.AddFile(data, mimeType.Split('/').Last());
        return await GetMediaType(filePath, mimeType);
    }

    private static async Task<MediaType?> GetMediaType(string filePath, string mimeType)
    {
        var relativeFilePath = System.IO.Path.GetFileName(filePath);
        if (mimeType.Contains("image"))
        {
            // Image
            var sizeData = await FileHelper.GetImageSize(filePath);
            return new ImageMediaType
            {
                Width = sizeData.Item1,
                Height = sizeData.Item2,
                Mime = mimeType,
                Path = relativeFilePath,
                Copyright = new CopyrightInformation
                {
                    License = "U"
                }
            };
        }

        if (mimeType.Contains("video"))
            // Video
            return new VideoMediaType
            {
                Mime = mimeType,
                Path = relativeFilePath,
                Copyright = new CopyrightInformation
                {
                    License = "U"
                }
            };

        if (mimeType.Contains("audio"))
            return new AudioMediaType
            {
                Mime = mimeType,
                Path = relativeFilePath,
                Copyright = new CopyrightInformation
                {
                    License = "U"
                }
            };

        return null;
    }
}