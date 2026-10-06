using Microsoft.JSInterop;
using Soenneker.Atomics.ValueBools;
using Soenneker.Blazor.C15t.Abstract;
using Soenneker.Blazor.C15t.Constants;
using Soenneker.Blazor.C15t.Models;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.C15t;

/// <inheritdoc cref="IC15tInterop"/>
public sealed class C15tInterop : IC15tInterop
{
    private const string _modulePath = "./" + C15tConstants.InteropScript;

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
            JsonElement payload = await module.InvokeAsync<JsonElement>("initialize", linked,
                JsonSerializer.SerializeToElement(options ?? new C15tOptions(), InteropJsonContext.Default.C15tOptions));
            C15tConsentState? state = payload.Deserialize(InteropJsonContext.Default.C15tConsentState);
            _initialized = true;
            return state;
        }
    }

    public ValueTask<C15tConsentState?> GetState(CancellationToken cancellationToken = default)
    {
        return Invoke("getState", cancellationToken);
    }

    public ValueTask<C15tConsentState?> AcceptAll(CancellationToken cancellationToken = default)
    {
        return Invoke("acceptAll", cancellationToken);
    }

    public ValueTask<C15tConsentState?> RejectNonNecessary(CancellationToken cancellationToken = default)
    {
        return Invoke("rejectNonNecessary", cancellationToken);
    }

    public ValueTask<C15tConsentState?> SaveCustom(CancellationToken cancellationToken = default)
    {
        return Invoke("saveCustom", cancellationToken);
    }

    public ValueTask<C15tConsentState?> SetConsent(string category, bool value, CancellationToken cancellationToken = default)
    {
        return Invoke("setConsent", cancellationToken, category, value);
    }

    public ValueTask<C15tConsentState?> SetSelectedConsent(string category, bool value, CancellationToken cancellationToken = default)
    {
        return Invoke("setSelectedConsent", cancellationToken, category, value);
    }

    public ValueTask<C15tConsentState?> OpenDialog(CancellationToken cancellationToken = default)
    {
        return Invoke("openDialog", cancellationToken);
    }

    public ValueTask<C15tConsentState?> ShowBanner(CancellationToken cancellationToken = default)
    {
        return Invoke("showBanner", cancellationToken);
    }

    public ValueTask<C15tConsentState?> CloseUi(CancellationToken cancellationToken = default)
    {
        return Invoke("closeUi", cancellationToken);
    }

    public ValueTask<C15tConsentState?> ResetConsents(CancellationToken cancellationToken = default)
    {
        return Invoke("resetConsents", cancellationToken);
    }

    private async ValueTask<C15tConsentState?> Invoke(
        string identifier, CancellationToken cancellationToken, params object?[] args)
    {
        ThrowIfDisposed();

        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            JsonElement payload = await module.InvokeAsync<JsonElement>(identifier, linked, args);
            return payload.Deserialize(InteropJsonContext.Default.C15tConsentState);
        }
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
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
