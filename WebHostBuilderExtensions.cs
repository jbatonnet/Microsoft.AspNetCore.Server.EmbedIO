using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Server.EmbedIO;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Hosting
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseEmbedIO(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                ServiceCollectionServiceExtensions.AddSingleton<IServer, EmbedIOServer>(services);
            });
        }
    }
}