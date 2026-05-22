using Soenneker.Blazor.C15t.Abstract;
using Soenneker.Blazor.C15t.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.C15t;

/// <inheritdoc cref="IC15tConsentService"/>
public sealed class C15tConsentService : IC15tConsentService
{
    private readonly IC15tInterop _interop;

    public C15tConsentState? CurrentState { get; private set; }

    public C15tConsentService(IC15tInterop interop)
    {
        _interop = interop;
    }

    public async ValueTask<C15tConsentState?> Initialize(C15tOptions? options = null, CancellationToken cancellationToken = default)
    {
        return CurrentState = await _interop.Initialize(options, cancellationToken);
    }

    public bool HasConsent(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return false;

        return CurrentState?.Consents?.TryGetValue(category, out bool value) == true && value;
    }

    public async ValueTask<C15tConsentState?> Refresh(CancellationToken cancellationToken = default)
    {
        return CurrentState = await _interop.GetState(cancellationToken);
    }

    public async ValueTask<C15tConsentState?> AcceptAll(CancellationToken cancellationToken = default)
    {
        return CurrentState = await _interop.AcceptAll(cancellationToken);
    }

    public async ValueTask<C15tConsentState?> RejectNonNecessary(CancellationToken cancellationToken = default)
    {
        return CurrentState = await _interop.RejectNonNecessary(cancellationToken);
    }

    public async ValueTask<C15tConsentState?> SaveCustom(CancellationToken cancellationToken = default)
    {
        return CurrentState = await _interop.SaveCustom(cancellationToken);
    }

    public async ValueTask<C15tConsentState?> SetConsent(string category, bool value, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category cannot be null or whitespace.", nameof(category));

        return CurrentState = await _interop.SetConsent(category, value, cancellationToken);
    }

    public async ValueTask<C15tConsentState?> SetSelectedConsent(string category, bool value, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category cannot be null or whitespace.", nameof(category));

        return CurrentState = await _interop.SetSelectedConsent(category, value, cancellationToken);
    }

    public async ValueTask<C15tConsentState?> OpenDialog(CancellationToken cancellationToken = default)
    {
        return CurrentState = await _interop.OpenDialog(cancellationToken);
    }

    public async ValueTask<C15tConsentState?> ShowBanner(CancellationToken cancellationToken = default)
    {
        return CurrentState = await _interop.ShowBanner(cancellationToken);
    }

    public async ValueTask<C15tConsentState?> CloseUi(CancellationToken cancellationToken = default)
    {
        return CurrentState = await _interop.CloseUi(cancellationToken);
    }

    public async ValueTask<C15tConsentState?> ResetConsents(CancellationToken cancellationToken = default)
    {
        return CurrentState = await _interop.ResetConsents(cancellationToken);
    }
}

