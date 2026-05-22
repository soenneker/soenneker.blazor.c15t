using Microsoft.JSInterop;
using Soenneker.Atomics.ValueBools;
using Soenneker.Blazor.C15t.Abstract;
using Soenneker.Blazor.C15t.Constants;
using Soenneker.Blazor.C15t.Models;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.C15t;

/// <inheritdoc cref="IC15tInterop"/>
public sealed class C15tInterop : IC15tInterop
{
    private const string _modulePath = C15tConstants.InteropScript;

    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    private bool _initialized;
    private ValueAtomicBool _disposed;

    public C15tInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask<C15tConsentState?> Initialize(C15tOptions? options = null, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            C15tConsentState? state = await module.InvokeAsync<C15tConsentState?>("initialize", linked, options ?? new C15tOptions());
            _initialized = true;
            return state;
        }
    }

    public async ValueTask<C15tConsentState?> GetState(CancellationToken cancellationToken = default)
    {
        return await Invoke<C15tConsentState?>("getState", cancellationToken);
    }

    public async ValueTask<C15tConsentState?> AcceptAll(CancellationToken cancellationToken = default)
    {
        return await Invoke<C15tConsentState?>("acceptAll", cancellationToken);
    }

    public async ValueTask<C15tConsentState?> RejectNonNecessary(CancellationToken cancellationToken = default)
    {
        return await Invoke<C15tConsentState?>("rejectNonNecessary", cancellationToken);
    }

    public async ValueTask<C15tConsentState?> SaveCustom(CancellationToken cancellationToken = default)
    {
        return await Invoke<C15tConsentState?>("saveCustom", cancellationToken);
    }

    public async ValueTask<C15tConsentState?> SetConsent(string category, bool value, CancellationToken cancellationToken = default)
    {
        return await Invoke<C15tConsentState?>("setConsent", cancellationToken, category, value);
    }

    public async ValueTask<C15tConsentState?> SetSelectedConsent(string category, bool value, CancellationToken cancellationToken = default)
    {
        return await Invoke<C15tConsentState?>("setSelectedConsent", cancellationToken, category, value);
    }

    public async ValueTask<C15tConsentState?> OpenDialog(CancellationToken cancellationToken = default)
    {
        return await Invoke<C15tConsentState?>("openDialog", cancellationToken);
    }

    public async ValueTask<C15tConsentState?> ShowBanner(CancellationToken cancellationToken = default)
    {
        return await Invoke<C15tConsentState?>("showBanner", cancellationToken);
    }

    public async ValueTask<C15tConsentState?> CloseUi(CancellationToken cancellationToken = default)
    {
        return await Invoke<C15tConsentState?>("closeUi", cancellationToken);
    }

    public async ValueTask<C15tConsentState?> ResetConsents(CancellationToken cancellationToken = default)
    {
        return await Invoke<C15tConsentState?>("resetConsents", cancellationToken);
    }

    private async ValueTask<T> Invoke<T>(string identifier, CancellationToken cancellationToken, params object?[] args)
    {
        ThrowIfDisposed();

        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<T>(identifier, linked, args);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed.TrySetTrue())
            return;

        _cancellationScope.Cancel();

        if (_initialized)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, CancellationToken.None);
                await module.InvokeVoidAsync("dispose", CancellationToken.None);
            }
            catch
            {
                // Best-effort cleanup when the JS runtime may already be unavailable.
            }
        }

        await _moduleImportUtil.DisposeContentModule(_modulePath);
        await _cancellationScope.DisposeAsync();
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed.Value, this);
    }
}
