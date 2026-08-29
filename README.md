[![](https://img.shields.io/nuget/v/soenneker.blazor.c15t.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.c15t/)

# Soenneker.Blazor.C15t

A higher-level Blazor utility built on top of `IC15tInterop`.

## Install

```bash
dotnet add package Soenneker.Blazor.C15t
```

## Quick start

```csharp
using Soenneker.Blazor.C15t.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddC15t();
```

Adds c15t interop and consent services as scoped services.

## What you get

- `IC15t` — A higher-level Blazor utility built on top of `IC15tInterop`.
- `IC15tConsentService` — A small stateful service over the c15t interop.
- `IC15tInterop` — Blazor interop for c15t consent runtime operations.
- `C15tConstants` — Constants used by the c15t Blazor wrapper.
- `C15tRegistrar` — Registration extensions for the c15t Blazor wrapper.
- `C15tConsentState` — A lightweight view of the current c15t consent state.
- `C15tDisplayedConsent` — A c15t consent category displayed to the visitor.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IC15t.Initialize(cancellationToken)` | Ensures the underlying JavaScript module has been loaded and is ready for use. | A task that completes when the C15t is ready for use. |
| `IC15tConsentService.CurrentState` | The most recently observed consent state. | The most recently observed consent state. |
| `IC15tConsentService.Initialize(options, cancellationToken)` | Initializes c15t and captures the current state. | A task whose result is the requested c15t Consent State. |
| `IC15tConsentService.HasConsent(category)` | Returns true when the specified category has consent. | true if consent has been granted for the category; otherwise, false. |
| `IC15tConsentService.Refresh(cancellationToken)` | Refreshes the current state from c15t. | A task whose result is the requested c15t Consent State. |
| `IC15tConsentService.AcceptAll(cancellationToken)` | Accepts all displayed consent categories. | A task whose result is the requested c15t Consent State. |
| `IC15tConsentService.RejectNonNecessary(cancellationToken)` | Rejects all non-necessary consent categories. | A task whose result is the requested c15t Consent State. |
| `IC15tConsentService.SaveCustom(cancellationToken)` | Saves selected custom consent choices. | A task whose result is the requested c15t Consent State. |
| `IC15tConsentService.SetConsent(category, value, cancellationToken)` | Sets and saves one consent category. | A task whose result is the requested c15t Consent State. |
| `IC15tConsentService.SetSelectedConsent(category, value, cancellationToken)` | Sets one selected consent category without saving. | A task whose result is the requested c15t Consent State. |
| `IC15tConsentService.OpenDialog(cancellationToken)` | Opens the privacy dialog. | A task whose result is the requested c15t Consent State. |
| `IC15tConsentService.ShowBanner(cancellationToken)` | Shows the consent banner. | A task whose result is the requested c15t Consent State. |
| `IC15tConsentService.CloseUi(cancellationToken)` | Closes c15t UI. | A task whose result is the requested c15t Consent State. |
| `IC15tConsentService.ResetConsents(cancellationToken)` | Resets saved consents. | A task whose result is the requested c15t Consent State. |
| `IC15tInterop.Initialize(options, cancellationToken)` | Initializes the c15t runtime. | A task whose result is the requested c15t Consent State. |
| `IC15tInterop.GetState(cancellationToken)` | Gets the current consent state. | A task whose result is the requested c15t Consent State. |
| `IC15tInterop.AcceptAll(cancellationToken)` | Accepts all displayed consent categories. | A task whose result is the requested c15t Consent State. |
| `IC15tInterop.RejectNonNecessary(cancellationToken)` | Rejects all non-necessary consent categories. | A task whose result is the requested c15t Consent State. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Dispose instances you own when their scope ends so held resources can be released.
