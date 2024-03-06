using System.Net.Http.Headers;

using Ivao.It.ApiSdk.Accessors;
using Ivao.It.ApiSdk.Auth;
using Ivao.It.ApiSdk.Config;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ivao.It.ApiSdk;

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

        //Api accessor services
        services.TryAddTransient<ITrackerApi, TrackerApi>();
        services.TryAddTransient<IAtcBookingsApi, AtcBookingsApi>();

        return services;
    }
}
