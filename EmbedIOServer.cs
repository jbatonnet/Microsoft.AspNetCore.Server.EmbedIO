using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;

using Unosquare.Labs.EmbedIO;
using Unosquare.Labs.EmbedIO.Log;

namespace Microsoft.AspNetCore.Server.EmbedIO
{
    internal class EmbedIOServer : IServer, IDisposable
    {
        public IFeatureCollection Features { get; } = new Http.Features.FeatureCollection();

        private WebServer webServer;
        private AspNetModule aspNetModule;

        private ServerAddressesFeature serverAddresses = new ServerAddressesFeature();

        public EmbedIOServer(ILoggerFactory loggerFactory)
        {
            Features.Set<IServerAddressesFeature>(serverAddresses);
        }

        public void Start<TContext>(IHttpApplication<TContext> application)
        {
            // Setup handler module
            aspNetModule = new AspNetModule(new HttpApplicationWrapper<TContext>(application), Features);

            // Setup web server
            webServer = new WebServer();
            webServer.RegisterModule(aspNetModule);

#if DEBUG
            webServer.UrlPrefixes.Add("http://127.0.0.1:5000/");
#endif

            webServer.UrlPrefixes.Remove("http://*/");
            foreach (string address in serverAddresses.Addresses)
                webServer.UrlPrefixes.Add(address + "/");

            // Start listener
            webServer.RunAsync();

#if DEBUG
            Task.Run(async () =>
            {
                await Task.Delay(1000);

                using (HttpClient httpClient = new HttpClient())
                    await httpClient.GetAsync("http://127.0.0.1:5000/");
            });
#endif
        }

        public void Dispose()
        {
            webServer.Dispose();
        }
    }
}