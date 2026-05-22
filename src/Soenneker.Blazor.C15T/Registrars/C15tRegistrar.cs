using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.C15t.Abstract;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Blazor.C15t.Registrars;

/// <summary>
/// Registration extensions for the c15t Blazor wrapper.
/// </summary>
public static class C15tRegistrar
{
    /// <summary>
    /// Adds c15t interop and consent services as scoped services.
    /// </summary>
    public static IServiceCollection AddC15t(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped();
        services.TryAddScoped<IC15tInterop, C15tInterop>();
        services.TryAddScoped<IC15t, C15t>();
        services.TryAddScoped<IC15tConsentService, C15tConsentService>();

        return services;
    }

    /// <summary>
    /// Adds c15t interop and consent services as scoped services.
    /// </summary>
    public static IServiceCollection AddC15tAsScoped(this IServiceCollection services)
    {
        return services.AddC15t();
    }
}
