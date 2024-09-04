using System.Net.Http.Headers;
using System.Text.Json;

using Ivao.It.IvaoApiSdk.Accessors;
using Ivao.It.IvaoApiSdk.Auth;
using Ivao.It.IvaoApiSdk.Config;
using Ivao.It.IvaoApiSdk.Json;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ivao.It.IvaoApiSdk;

public static class DependencyInjection
{
    public static IServiceCollection AddIvaoApi(this IServiceCollection services, IConfiguration config)
    {
        //Config IOptions
        services.Configure<IvaoApiConfig>(config.GetRequiredSection(IvaoApiConfig.SectionName));
        
        //Basic HTTP Clients Config
        Action<HttpClient> configAction = cfg =>
        {
            cfg.BaseAddress = new Uri(@"https://api.ivao.aero/");
            cfg.DefaultRequestHeaders.Accept.Clear();
            cfg.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        };
        
        //Config Auth
        services.AddHttpClient<IAuthenticator, DefaultAuthenticator>(configAction);
        services.TryAddSingleton<IAuthenticator, DefaultAuthenticator>();

        //Typed HttpClients
        services.AddHttpClient<ITrackerApi, TrackerApi>(configAction);
        services.AddHttpClient<IAtcBookingsApi, AtcBookingsApi>(configAction);
        services.AddHttpClient<ICoreApi, CoreApi>(configAction);

        //Api accessor services
        services.TryAddTransient<ITrackerApi, TrackerApi>();
        services.TryAddTransient<IAtcBookingsApi, AtcBookingsApi>();
        services.TryAddTransient<ICoreApi, CoreApi>();

        return services;
    }
}
