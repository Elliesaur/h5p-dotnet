using System.IO.Compression;
using H5pDotNet.Libraries;
using H5pDotNet.Libraries.DialogCards;
using H5pDotNet.Types.Raw;
using H5pDotNet.Utilities;
using Newtonsoft.Json;

namespace H5pDotNet;

public class H5PPackage
{
    private static readonly Dictionary<string, Type> _contentMapping = new()
    {
        { "H5P.Dialogcards", typeof(H5PDialogCards) }
    };

    private readonly string _contentDirectory;
    private readonly List<string> _contentFiles = new();
    private readonly List<string> _libraryDependencies = new();

    private readonly string _tempDirectory;

    private H5PPackage(string fileName)
    {
        FileName = fileName;
        _tempDirectory = Path.GetTempPath() + Guid.NewGuid();
        Directory.CreateDirectory(_tempDirectory);
        _contentDirectory = $"{_tempDirectory}/content";
    }

    public string FileName { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
    public string MainLibrary { get; set; }

    public IH5PContent Content { get; set; }

    /// <summary>
    ///     Creates a new package from the library name supplied.
    /// </summary>
    /// <param name="libNameNoVersion">Library name without version (no dash).</param>
    /// <param name="fileName">File name to store it, does not write immediately.</param>
    /// <param name="title">Title for the package.</param>
    /// <param name="language">Language key or "und".</param>
    /// <returns>Package.</returns>
    public static async Task<H5PPackage> FromNewPackage(string libNameNoVersion, string fileName, string title,
        string language = "und")
    {
        var h5p = new H5PPackage(fileName);
        h5p.Title = title;
        h5p.Language = language;
        await h5p.AddLibrary(libNameNoVersion, true);
        return h5p;
    }

    /// <summary>
    ///     Loads an existing package from disk.
    /// </summary>
    /// <param name="filePath">Full path to .h5p.</param>
    /// <returns>Package.</returns>
    /// <exception cref="Exception">Unsupported content type (library).</exception>
    public static async Task<H5PPackage> FromExistingPackage(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        using var archive = ZipFile.OpenRead(fileInfo.FullName);

        var h5p = new H5PPackage(Path.GetFileNameWithoutExtension(fileInfo.FullName));
        archive.ExtractToDirectory(h5p._tempDirectory);

        // Force load the config
        await h5p.SyncConfig(false);

        if (_contentMapping != null && !_contentMapping.ContainsKey(h5p.MainLibrary))
            throw new Exception($"H5P package content '{h5p.MainLibrary}' not found");

        var packageContentType = _contentMapping[h5p.MainLibrary];
        var contentExisting = await File.ReadAllTextAsync(h5p._contentDirectory + "/content.json");
        h5p.Content = (IH5PContent)JsonConvert.DeserializeObject(contentExisting, packageContentType!)!;
        h5p._libraryDependencies.Add(h5p.MainLibrary);
        foreach (var file in Directory.GetFiles(h5p._contentDirectory))
        {
            if (file.Contains("content.json")) continue;
            h5p._contentFiles.Add(file);
        }

        return h5p;
    }

    /// <summary>
    ///     Sets the "main" content for the json file. Only one content can exist.
    /// </summary>
    /// <param name="content">The content type that is any IH5PContent that is runnable as a library.</param>
    public async Task SetMainContent(IH5PContent content)
    {
        Content = content;
    }

    /// <summary>
    ///     Add a file given the local file path, this file will be copied to the "content" directory.
    /// </summary>
    /// <param name="filePath">Local file path.</param>
    /// <returns>The copied full path to the content directory file.</returns>
    public async Task<string> AddFile(string filePath)
    {
        _contentFiles.Add(filePath);
        var destFileName = Path.Combine(_contentDirectory, Path.GetFileName(filePath));
        File.Copy(filePath, destFileName, true);
        return destFileName;
    }

    /// <summary>
    ///     Adds a file given a byte array. Writes file to temporary location.
    /// </summary>
    /// <param name="content">File contents.</param>
    /// <param name="extension">File extension (from mime type).</param>
    /// <returns>The copied full path to the content directory file.</returns>
    public async Task<string> AddFile(byte[] content, string extension)
    {
        var tempName = Path.GetTempFileName().Replace(".", "");
        var tempFullPath = $"{tempName}.{extension}";
        await File.WriteAllBytesAsync(tempFullPath, content);
        return await AddFile(tempFullPath);
    }

    /// <summary>
    ///     Not sure what this is currently beyond a mess.
    ///     Adds a library by downloading it directly from the hub.
    /// </summary>
    /// <param name="library">Library name "H5P.Dialogcards" for example.</param>
    /// <param name="isMainLibrary">Whether it is the main library (if it is, it assumes cleared temp directory).</param>
    public async Task AddLibrary(string library, bool isMainLibrary)
    {
        var libName = library.Contains(" ") ? library.Split(' ')[0] : library;
        var libData = await LibraryAPI.GetLibrary(libName);
        var zip = Path.Combine(_tempDirectory, libName + ".h5p");
        await File.WriteAllBytesAsync(zip, libData);
        _libraryDependencies.Add(library);

        using var archive = ZipFile.OpenRead(zip);
        if (isMainLibrary)
        {
            MainLibrary = libName;
            archive.ExtractToDirectory(_tempDirectory);

            foreach (var file in Directory.GetFiles(_contentDirectory, "*", SearchOption.AllDirectories))
                // Remove the existing data that is irrelevant for our use case.
                File.Delete(file);

            // Remove existing directories.
            foreach (var dir in Directory.GetDirectories(_contentDirectory)) Directory.Delete(dir, true);
        }
        else
        {
            // TODO: Version appending.
            var outDir = Path.Combine(_tempDirectory, library);
            Directory.CreateDirectory(outDir);
            archive.ExtractToDirectory(outDir);
        }
    }

    /// <summary>
    ///     Writes all changes, requires a content to be set in the <see cref="SetMainContent" /> method.
    /// </summary>
    /// <param name="saveFilePath">File path to save to, if blank defaults to the <see cref="FileName" />.</param>
    /// <returns>The full path to the saved file.</returns>
    public async Task<string> Save(string saveFilePath = "")
    {
        // Delete library downloads.
        foreach (var file in Directory.GetFiles(_tempDirectory, "*.h5p")) File.Delete(file);

        // Make the json
        var content = JsonConvert.SerializeObject(Content);
        await File.WriteAllTextAsync($"{_contentDirectory}/content.json", content);

        await SyncConfig(true);

        var destinationArchiveFileName = string.IsNullOrEmpty(saveFilePath)
            ? $"{Environment.CurrentDirectory}/{FileName}.h5p"
            : saveFilePath;

        if (File.Exists(destinationArchiveFileName)) File.Delete(destinationArchiveFileName);

        ZipFile.CreateFromDirectory(_tempDirectory, destinationArchiveFileName);
        return destinationArchiveFileName;
    }

    private async Task SyncConfig(bool writePropertyData)
    {
        // Fix h5p.json
        var configFile = $"{_tempDirectory}/h5p.json";
        var text = await File.ReadAllTextAsync(configFile);
        var config = JsonConvert.DeserializeObject<H5PConfig>(text);
        if (writePropertyData)
        {
            config.Title = Title;
            config.Language = Language;
            config.MainLibrary = MainLibrary;
            var configSerialised = JsonConvert.SerializeObject(config);

            File.Delete(configFile);
            await File.WriteAllTextAsync(configFile, configSerialised);
        }
        else
        {
            Title = config.Title;
            Language = config.Language;
            MainLibrary = config.MainLibrary;
        }
    }
}