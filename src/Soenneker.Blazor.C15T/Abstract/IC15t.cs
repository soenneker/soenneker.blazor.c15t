using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.C15T.Abstract;

/// <summary>
/// A higher-level Blazor utility built on top of <see cref="IC15TInterop"/>.
/// </summary>
public interface IC15t
{
    /// <summary>
    /// Ensures the underlying JavaScript module has been loaded and is ready for use.
    /// </summary>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
