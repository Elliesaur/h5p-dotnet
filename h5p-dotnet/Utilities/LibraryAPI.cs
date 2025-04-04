namespace H5pDotNet.Utilities;

public static class LibraryAPI
{
    private static readonly Dictionary<string, byte[]> _cachedLibraries = new();

    /// <summary>
    ///     Get a library from the hub and return the data from it.
    /// </summary>
    /// <param name="libNameNoVersion"></param>
    /// <returns></returns>
    public static async Task<byte[]> GetLibrary(string libNameNoVersion)
    {
        if (_cachedLibraries.TryGetValue(libNameNoVersion, out var library)) return library;

        using var httpClient = new HttpClient();
        var url = "https://api.h5p.org/v1/content-types/" + libNameNoVersion;
        var data = await httpClient.GetByteArrayAsync(url);
        _cachedLibraries.Add(libNameNoVersion, data);
        return data;
    }
}