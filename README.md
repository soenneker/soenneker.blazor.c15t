[![](https://img.shields.io/nuget/v/soenneker.blazor.c15t.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.c15t/)

# Soenneker.Blazor.C15t

A Blazor wrapper around the c15t consent runtime, with scoped state, consent actions, and a cascading provider.

## Installation

```bash
dotnet add package Soenneker.Blazor.C15t
```

## Registration

```csharp
using Soenneker.Blazor.C15t.Registrars;

builder.Services.AddC15t();
```

## Initialize the consent runtime

Wrap the application content with `C15tProvider`. It initializes after the first render, when JavaScript interop is available, and cascades `IC15tConsentService` to descendants.

```razor
@using Soenneker.Blazor.C15t
@using Soenneker.Blazor.C15t.Models

<C15tProvider Options="_options">
    <Router AppAssembly="@typeof(App).Assembly">
        ...
    </Router>
</C15tProvider>

@code {
    private readonly C15tOptions _options = new()
    {
        Mode = "offline",
        ConsentCategories =
        [
            "necessary",
            "functionality",
            "experience",
            "measurement",
            "marketing"
        ]
    };
}
```

The provider does not delay rendering its children. Code that loads analytics, marketing, or other optional resources must still check consent before running.

## Read and change consent

```razor
@using Soenneker.Blazor.C15t.Abstract
@inject IC15tConsentService Consent

<button @onclick="async () => await Consent.AcceptAll()">Accept all</button>
<button @onclick="async () => await Consent.RejectNonNecessary()">Necessary only</button>

@if (Consent.HasConsent("measurement"))
{
    <p>Analytics may be enabled.</p>
}
```

`CurrentState` is the last state returned by an operation through this scoped service. `Refresh()` reads the runtime again. For a custom preferences UI, call `SetSelectedConsent(category, value)` for each pending choice and then `SaveCustom()`; `SetConsent()` saves one category immediately.

`AcceptAll()`, `RejectNonNecessary()`, `ResetConsents()`, `OpenDialog()`, `ShowBanner()`, and `CloseUi()` all return the resulting state and update `CurrentState`.

## Runtime options

- With no backend URL, the wrapper selects offline mode.
- `BackendUrl` and `Mode` configure a hosted or self-hosted c15t consent manager.
- `ConsentCategories` is forwarded as c15t's initial GDPR categories.
- `ExtensionData` forwards additional c15t options without requiring wrapper changes.
- `ModuleUrl` defaults to the pinned c15t ESM package on jsDelivr. Relative same-origin URLs are supported for self-hosting.

The module URL must resolve to HTTPS, except for loopback HTTP development URLs. It is executable code: only configure a module location you control or explicitly trust. If a Content Security Policy is enabled, allow the selected module origin and any configured backend origin.
