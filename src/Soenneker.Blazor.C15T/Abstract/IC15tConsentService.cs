using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.C15t.Models;

namespace Soenneker.Blazor.C15t.Abstract;

/// <summary>
/// A small stateful service over the c15t interop.
/// </summary>
public interface IC15tConsentService
{
    /// <summary>
    /// The most recently observed consent state.
    /// </summary>
    C15tConsentState? CurrentState { get; }

    /// <summary>
    /// Initializes c15t and captures the current state.
    /// </summary>
    /// <param name="options">Options to configure for the c15t consent service.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> Initialize(C15tOptions? options = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns true when the specified category has consent.
    /// </summary>
    /// <param name="category">Category to select or update.</param>
    /// <returns>true if consent has been granted for the category; otherwise, false.</returns>
    bool HasConsent(string category);

    /// <summary>
    /// Refreshes the current state from c15t.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> Refresh(CancellationToken cancellationToken = default);

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
    /// Saves selected custom consent choices.
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
    /// Opens the privacy dialog.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> OpenDialog(CancellationToken cancellationToken = default);

    /// <summary>
    /// Shows the consent banner.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested c15t Consent State.</returns>
    ValueTask<C15tConsentState?> ShowBanner(CancellationToken cancellationToken = default);

    /// <summary>
    /// Closes c15t UI.
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
