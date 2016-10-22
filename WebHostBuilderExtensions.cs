using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Server.EmbedIO
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseEmbedIO(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                //ServiceCollectionServiceExtensions.AddTransient<IConfigureOptions<WebListenerOptions>, WebListenerOptionsSetup>(services);
                ServiceCollectionServiceExtensions.AddSingleton<IServer, EmbedIOServer>(services);
            });
        }
    }
}