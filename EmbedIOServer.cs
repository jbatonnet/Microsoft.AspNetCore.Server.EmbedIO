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

            webServer.UrlPrefixes.Remove("http://*/");
            foreach (string address in serverAddresses.Addresses)
                webServer.UrlPrefixes.Add(address + "/");

            // Start listener
            webServer.RunAsync();
        }

        public void Dispose()
        {
            webServer.Dispose();
        }
    }
}