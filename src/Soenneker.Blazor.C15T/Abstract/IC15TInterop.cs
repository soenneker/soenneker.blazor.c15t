using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.C15t.Models;

namespace Soenneker.Blazor.C15t.Abstract;

/// <summary>
/// Blazor interop for c15t consent runtime operations.
/// </summary>
public interface IC15tInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes the c15t runtime.
    /// </summary>
    /// <param name="options">Options to configure for the c15t.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> Initialize(C15tOptions? options = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current consent state.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> GetState(CancellationToken cancellationToken = default);

    /// <summary>
    /// Accepts all displayed consent categories.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> AcceptAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rejects all non-necessary consent categories.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> RejectNonNecessary(CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the selected consent choices.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> SaveCustom(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets and saves one consent category.
    /// </summary>
    /// <param name="category">Category to select or update.</param>
    /// <param name="value">Whether consent is granted for the category.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> SetConsent(string category, bool value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets one selected consent category without saving.
    /// </summary>
    /// <param name="category">Category to select or update.</param>
    /// <param name="value">Whether consent is granted for the category.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> SetSelectedConsent(string category, bool value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Opens the c15t privacy dialog state.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> OpenDialog(CancellationToken cancellationToken = default);

    /// <summary>
    /// Shows the c15t banner state.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> ShowBanner(CancellationToken cancellationToken = default);

    /// <summary>
    /// Closes c15t UI state.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> CloseUi(CancellationToken cancellationToken = default);

    /// <summary>
    /// Resets saved consents.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> ResetConsents(CancellationToken cancellationToken = default);
}
