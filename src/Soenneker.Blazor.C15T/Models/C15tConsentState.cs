using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Soenneker.Blazor.C15t.Models;

/// <summary>
/// A lightweight view of the current c15t consent state.
/// </summary>
public sealed class C15tConsentState
{
    /// <summary>
    /// Current saved consents by category.
    /// </summary>
    [JsonPropertyName("consents")]
    public Dictionary<string, bool>? Consents { get; set; }

    /// <summary>
    /// Current selected consents by category.
    /// </summary>
    [JsonPropertyName("selectedConsents")]
    public Dictionary<string, bool>? SelectedConsents { get; set; }

    /// <summary>
    /// Displayed consent categories.
    /// </summary>
    [JsonPropertyName("displayedConsents")]
    public List<C15tDisplayedConsent>? DisplayedConsents { get; set; }

    /// <summary>
    /// Whether the consent banner should be shown.
    /// </summary>
    [JsonPropertyName("showPopup")]
    public bool? ShowPopup { get; set; }

    /// <summary>
    /// Whether the privacy dialog is open.
    /// </summary>
    [JsonPropertyName("isPrivacyDialogOpen")]
    public bool? IsPrivacyDialogOpen { get; set; }

    /// <summary>
    /// Whether consent information is loading.
    /// </summary>
    [JsonPropertyName("isLoadingConsentInfo")]
    public bool? IsLoadingConsentInfo { get; set; }

    /// <summary>
    /// Additional c15t-provided values.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}

