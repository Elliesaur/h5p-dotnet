using H5pDotNet.Types;
using Newtonsoft.Json;

namespace H5pDotNet.Libraries.DialogCards;

public class H5PDialogCard : H5PText, IH5PContent
{
    /// <summary>
    ///     Supports HTML Tags: p, br, strong, em, code.
    /// </summary>
    [JsonProperty("answer")]
    public string Answer { get; set; }

    /// <summary>
    ///     Tips, use the tip nested class if any are present.
    /// </summary>
    [JsonProperty("tips")]
    public object[] Tips { get; set; } = Array.Empty<object>();

    /// <summary>
    ///     Audio files, optional.
    /// </summary>
    [JsonProperty("audio", NullValueHandling = NullValueHandling.Ignore)]
    public List<AudioMediaType>? Audio { get; set; }

    /// <summary>
    ///     Optional image for the card. (The card may use just an image, just a text or both)
    /// </summary>
    [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
    public ImageMediaType? Image { get; set; }

    /// <summary>
    ///     Alternative text for the image.
    ///     Optional.
    /// </summary>
    [JsonProperty("imageAltText")]
    public string? ImageAltText { get; set; } = string.Empty;

    /// <summary>
    ///     Tip object to use with the "tips" property of the card.
    /// </summary>
    public class Tip
    {
        /// <summary>
        ///     Tip for the first part of the dialogue.
        ///     Optional.
        /// </summary>
        [JsonProperty("front", NullValueHandling = NullValueHandling.Ignore)]
        public string? Front { get; set; }

        /// <summary>
        ///     Tip for the second part of the dialogue.
        ///     Optional.
        /// </summary>
        [JsonProperty("back", NullValueHandling = NullValueHandling.Ignore)]
        public string? Back { get; set; }
    }
}