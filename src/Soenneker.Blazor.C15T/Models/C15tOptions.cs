using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Soenneker.Blazor.C15t.Constants;

namespace Soenneker.Blazor.C15t.Models;

/// <summary>
/// Minimal c15t runtime options.
/// </summary>
public sealed class C15tOptions
{
    /// <summary>
    /// The c15t ESM module URL to import.
    /// </summary>
    [JsonPropertyName("moduleUrl")]
    public string? ModuleUrl { get; set; } = C15tConstants.DefaultModuleUrl;

    /// <summary>
    /// The c15t runtime mode.
    /// </summary>
    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    /// <summary>
    /// The hosted or self-hosted c15t backend URL.
    /// </summary>
    [JsonPropertyName("backendURL")]
    public string? BackendUrl { get; set; }

    /// <summary>
    /// The consent categories to expose.
    /// </summary>
    [JsonPropertyName("consentCategories")]
    public List<string>? ConsentCategories { get; set; }

    /// <summary>
    /// Additional c15t runtime options.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}

