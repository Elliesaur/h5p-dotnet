using SixLabors.ImageSharp;

namespace H5pDotNet.Utilities;

public static class FileHelper
{
    private static readonly HttpClient httpClient = new();

    public static async Task<byte[]> DownloadFile(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("URL cannot be null or empty.", nameof(url));

        try
        {
            // Send a GET request to the specified URL
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throw if not a success code.

            // Read the response content as a byte array
            return await response.Content.ReadAsByteArrayAsync();
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., logging)
            throw new InvalidOperationException("Error downloading file.", ex);
        }
    }

    public static async Task<string> GetMimeTypeFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        // Get the file extension
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return GetMimeTypeFromExtension(extension);
    }

    public static async Task<string> GetMimeTypeUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("URL cannot be null or empty.", nameof(url));

        try
        {
            // Send a HEAD request to get the headers without downloading the content
            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
            response.EnsureSuccessStatusCode();

            // Check if the Content-Type header is present
            if (response.Content.Headers.ContentType != null) return response.Content.Headers.ContentType.MediaType;
        }
        catch
        {
        }

        // If the Content-Type header is not available, fallback to extension-based detection
        try
        {
            var uri = new Uri(url);
            var extension = Path.GetExtension(uri.AbsolutePath).ToLowerInvariant();
            return GetMimeTypeFromExtension(extension);
        }
        catch (UriFormatException)
        {
            throw new ArgumentException("Invalid URL format.", nameof(url));
        }
    }

    private static string GetMimeTypeFromExtension(string extension)
    {
        return extension switch
        {
            ".txt" => "text/plain",
            ".pdf" => "application/pdf",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".html" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            ".json" => "application/json",
            ".xml" => "application/xml",
            // Video file types
            ".mp4" => "video/mp4",
            ".m4v" => "video/x-m4v",
            ".mov" => "video/quicktime",
            ".avi" => "video/x-msvideo",
            ".wmv" => "video/x-ms-wmv",
            ".flv" => "video/x-flv",
            ".mkv" => "video/x-matroska",
            ".webm" => "video/webm",
            ".3gp" => "video/3gpp",
            ".mpeg" => "video/mpeg",
            ".mpg" => "video/mpeg",
            ".ogv" => "video/ogg",
            // Audio file types
            ".mp3" => "audio/mpeg",
            ".wav" => "audio/wav",
            ".ogg" => "audio/ogg",
            ".aac" => "audio/aac",
            ".flac" => "audio/flac",
            ".m4a" => "audio/m4a",
            ".wma" => "audio/x-ms-wma",
            ".opus" => "audio/opus",
            _ => "application/octet-stream" // Default MIME type
        };
    }

    public static async Task<Tuple<int, int>> GetImageSize(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        // Ensure the file exists
        if (!File.Exists(filePath)) throw new FileNotFoundException("The specified file was not found.", filePath);

        // Load the image directly
        using (var image = await Image.LoadAsync(filePath))
        {
            return Tuple.Create(image.Width, image.Height);
        }
    }
}