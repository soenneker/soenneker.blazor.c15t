using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Soenneker.Blazor.C15t.Models;

/// <summary>
/// A c15t consent category displayed to the visitor.
/// </summary>
public sealed class C15tDisplayedConsent
{
    /// <summary>
    /// The category name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The category description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Whether the category is disabled.
    /// </summary>
    [JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }

    /// <summary>
    /// Whether the category is displayed.
    /// </summary>
    [JsonPropertyName("display")]
    public bool? Display { get; set; }

    /// <summary>
    /// Whether the category is selected by default.
    /// </summary>
    [JsonPropertyName("defaultValue")]
    public bool? DefaultValue { get; set; }

    /// <summary>
    /// Additional c15t-provided values.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}

