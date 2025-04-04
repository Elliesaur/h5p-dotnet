using H5pDotNet.Types;
using Newtonsoft.Json;

namespace H5pDotNet.Libraries.DialogCards;

/// <summary>
///     This can be used when dealing with the "Library" type of attribute.
/// </summary>
public class DialogCardsLibrary : ILibraryType<H5PDialogCards>
{
    internal DialogCardsLibrary()
    {
    }

    [JsonProperty("library")] public string Library { get; set; } = "HP5.Dialogcards 1.9";

    [JsonProperty("metadata")] public Metadata Metadata { get; set; }

    [JsonProperty("params")] public H5PDialogCards Parameters { get; set; }

    public static DialogCardsLibrary FromCards(List<H5PDialogCard> cards, string title = "Text",
        string description = "")
    {
        return new DialogCardsLibrary
        {
            Parameters = new H5PDialogCards
            {
                Title = title,
                Description = description,
                Dialogs = cards
            },
            Metadata = new Metadata { Title = title, License = "U", ContentType = "Text" }
        };
    }
}