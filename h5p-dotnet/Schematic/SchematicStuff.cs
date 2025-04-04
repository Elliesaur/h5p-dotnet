using Newtonsoft.Json;

namespace H5pDotNet.Schematic;

public class SchematicStuff
{
}

public interface IDefaultAttributeSchematic
{
    [JsonProperty("default", NullValueHandling = NullValueHandling.Ignore)]
    public string? DefaultValue { get; set; }
}

/// <summary>
///     <see cref="IImportantAttributeSchematic" />
/// </summary>
public class ImportantAttributeObjectSchematic
{
    [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
    public string Description { get; set; }

    [JsonProperty("example", NullValueHandling = NullValueHandling.Ignore)]
    public string Example { get; set; }
}

public interface IImportantAttributeSchematic
{
    /// <summary>
    ///     This attribute is used to give more detailed instructions and contains two parts, i.e description and example.
    ///     Type: An object containing attribute "description" and "example".
    ///     Supported by: text (when using the html or textarea widget)
    /// </summary>
    [JsonProperty("important", NullValueHandling = NullValueHandling.Ignore)]
    public ImportantAttributeObjectSchematic? Important { get; set; }
}

public class SchematicRegexpAttributeObject
{
    [JsonProperty("pattern", NullValueHandling = NullValueHandling.Ignore)]
    public string Pattern { get; set; }

    [JsonProperty("modifiers", NullValueHandling = NullValueHandling.Ignore)]
    public string Modifiers { get; set; }
}

public class SchematicTextType : IH5PContentSchematic, IDefaultAttributeSchematic, IImportantAttributeSchematic,
    IWidgetAttributeSchematic
{
    /// <summary>
    ///     The maximum number of characters in text. Not supported when "html" widget is used.
    ///     Default value: 255
    /// </summary>
    [JsonProperty("maxLength", NullValueHandling = NullValueHandling.Ignore)]
    public int? MaxLength { get; set; }


    [JsonProperty("regexp", NullValueHandling = NullValueHandling.Ignore)]
    public SchematicRegexpAttributeObject? Regexp { get; set; }

    /// <summary>
    ///     Possible values are "p" and "div". If not set, "div" will be assumed.
    ///     This only affects the html widget behavior when pressing enter.
    ///     Default value: "div"
    ///     Supported by: text (only when using the html widget)
    /// </summary>
    [JsonProperty("enterMode", NullValueHandling = NullValueHandling.Ignore)]
    public string? EnterMode { get; set; }

    /// <summary>
    ///     Description:  Lists available HTML tags in the text field. This defines allowed HTML tags in this library.
    ///     If tags are not defined at all, the text will be output as plain text, with all HTML specific chars escaped
    ///     (&, lt and gt). If tags are defined, HTML will be filtered for allowed tags, and then returned to the library as
    ///     HTML.
    ///     Type: Array of tags (as strings). Available tags are:
    ///     strong
    ///     em
    ///     sub
    ///     sup
    ///     u
    ///     strike
    ///     ul
    ///     ol
    ///     blockquote
    ///     a
    ///     table
    ///     hr
    ///     Supported by: text
    /// </summary>
    [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Tags { get; set; }

    /// <summary>
    ///     Object containing font properties. Defining these while using the html widget allows the user to choose font
    ///     size, family, color and background color for the text. Be warned that enabling for this in your content type
    ///     will make it more difficult to change design aspects in the future, i.e. changing the background color might
    ///     decrease readability and break WCAG requirements.
    /// </summary>
    [JsonProperty("font", NullValueHandling = NullValueHandling.Ignore)]
    public FontSchematic FontSchematicDetails { get; set; }

    public string? DefaultValue { get; set; }
    public string Type { get; set; } = "text";
    public string Name { get; set; }
    public string Label { get; set; }
    public string? Description { get; set; }
    public bool? Optional { get; set; }
    public string? Importance { get; set; }
    public bool? Common { get; set; }
    public ImportantAttributeObjectSchematic? Important { get; set; }

    public string? Widget { get; set; }

    public class FontSchematic
    {
        public object Size { get; set; }
        public object Family { get; set; }
        public object Color { get; set; }
        public object Background { get; set; }
    }
}

public class SchematicNumberType : IH5PContentSchematic, IDefaultAttributeSchematic, IWidgetAttributeSchematic,
    IMinAttributeSchematic, IMaxAttributeSchematic
{
    /// <summary>
    ///     Steps to allow in number. E.g if this is set to 5, 6 won't be allowed, but 5, 10, 15  and so on will.
    /// </summary>
    [JsonProperty("step", NullValueHandling = NullValueHandling.Ignore)]
    public int? Step { get; set; }

    /// <summary>
    ///     The number of decimal digits allowed. Use 0 for integer values.
    /// </summary>
    [JsonProperty("decimals", NullValueHandling = NullValueHandling.Ignore)]
    public int? Decimals { get; set; } = 0;

    public string? DefaultValue { get; set; }
    public string Type { get; set; } = "number";
    public string Name { get; set; }
    public string Label { get; set; }
    public string? Description { get; set; }
    public bool? Optional { get; set; }
    public string? Importance { get; set; }
    public bool? Common { get; set; }
    public long? Max { get; set; }
    public long? Min { get; set; }
    public string? Widget { get; set; }
}

public class BooleanType : IH5PContentSchematic, IWidgetAttributeSchematic, IDefaultAttributeSchematic
{
    public string? DefaultValue { get; set; }
    public string Type { get; set; } = "boolean";
    public string Name { get; set; }
    public string Label { get; set; }
    public string? Description { get; set; }
    public bool? Optional { get; set; }
    public string? Importance { get; set; }
    public bool? Common { get; set; }
    public string? Widget { get; set; }
}

public interface IMinAttributeSchematic
{
    /// <summary>
    ///     This attribute is supported by two types: number and list. For the number type this means minimum allowed value.
    ///     For list it means the minimum number of elements in the list.
    /// </summary>
    [JsonProperty("min", NullValueHandling = NullValueHandling.Ignore)]
    public long? Min { get; set; }
}

public interface IMaxAttributeSchematic
{
    /// <summary>
    ///     This attribute is supported by two types: number and list. For the number type this means maximum allowed value.
    ///     For list it means the maximum number of elements in the list.
    /// </summary>
    [JsonProperty("max", NullValueHandling = NullValueHandling.Ignore)]
    public long? Max { get; set; }
}

public interface IWidgetAttributeSchematic
{
    /// <summary>
    ///     Name of the widget to use in the editor. If not set, the default editor widget will be used.
    ///     Supported by: text, number, boolean, group, select, library, image, video, audio, file
    /// </summary>
    [JsonProperty("widget", NullValueHandling = NullValueHandling.Ignore)]
    public string? Widget { get; set; }
}

public interface IH5PContentSchematic
{
    /// <summary>
    ///     the field type
    /// </summary>
    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string Type { get; set; }

    /// <summary>
    ///     Internal name of the field. Must be a valid JavaScript identifier string.
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    ///     The field's label in the editor.
    /// </summary>
    [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
    public string Label { get; set; }

    /// <summary>
    ///     Description displayed with the field in the editor.
    /// </summary>
    [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
    public string? Description { get; set; }

    /// <summary>
    ///     Set to true for optional fields.
    /// </summary>
    [JsonProperty("optional", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Optional { get; set; }

    /// <summary>
    ///     An indication of the field's importance. Is typically used by the editor giving a more prominent style on the
    ///     more important fields, but could e.g. also be used to generate weighted input to a search engine.
    ///     String, one of the following: "low", "medium" or "high".
    /// </summary>
    [JsonProperty("importance", NullValueHandling = NullValueHandling.Ignore)]
    public string? Importance { get; set; }

    /// <summary>
    ///     If set to true, all instances of this library will use the same value for this field. The editor will display
    ///     the input for this field in a "Common fields" container at the end of the editor form.
    /// </summary>
    [JsonProperty("common", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Common { get; set; }
}