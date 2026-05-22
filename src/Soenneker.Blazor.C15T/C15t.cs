using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.C15t.Abstract;

namespace Soenneker.Blazor.C15t;

/// <inheritdoc cref="IC15t"/>
public sealed class C15t : IC15t
{
    private readonly IC15tInterop _interop;

    public C15t(IC15tInterop interop)
    {
        _interop = interop ?? throw new ArgumentNullException(nameof(interop));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        await _interop.Initialize(cancellationToken: cancellationToken);
    }
}
