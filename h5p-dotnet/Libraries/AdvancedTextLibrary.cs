using H5pDotNet.Types;

namespace H5pDotNet.Libraries;

public class AdvancedTextLibrary : ILibraryType<H5PText>
{
    internal AdvancedTextLibrary()
    {
    }

    public string Library { get; set; } = "HP5.AdvancedText 1.1";
    public Metadata Metadata { get; set; }
    public H5PText Parameters { get; set; }

    public static AdvancedTextLibrary FromText(string text, string title = "Text")
    {
        return new AdvancedTextLibrary
        {
            Parameters = new H5PText { Text = text },
            Metadata = new Metadata { Title = title, License = "U", ContentType = "Text" }
        };
    }
}