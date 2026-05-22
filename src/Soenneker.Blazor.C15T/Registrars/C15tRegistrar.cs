using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.C15T.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Blazor.C15T.Registrars;

/// <summary>
/// Registration for the interop and utility services.
/// </summary>
public static class C15tRegistrar
{
    /// <summary>
    /// Adds <see cref="IC15TInterop"/> and <see cref="IC15t"/> as scoped services.
    /// </summary>
    public static IServiceCollection AddC15tAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
                .TryAddScoped<IC15TInterop, C15TInterop>();

        services.TryAddScoped<IC15t, C15t>();

        return services;
    }
}
