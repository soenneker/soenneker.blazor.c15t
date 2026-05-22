using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.C15T.Abstract;

namespace Soenneker.Blazor.C15T;

/// <inheritdoc cref="IC15t"/>
public sealed class C15t : IC15t
{
    private readonly IC15TInterop _interop;

    public C15t(IC15TInterop interop)
    {
        _interop = interop ?? throw new ArgumentNullException(nameof(interop));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return _interop.Initialize(cancellationToken);
    }
}
