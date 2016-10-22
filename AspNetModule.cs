﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Unosquare.Labs.EmbedIO;

namespace Microsoft.AspNetCore.Server.EmbedIO
{
    internal class AspNetModule : WebModuleBase
    {
        public override string Name => "ASP.NET Core module";

        public AspNetModule(IHttpApplication<object> application, IFeatureCollection features)
        {
            AddHandler(ModuleMap.AnyPath, HttpVerbs.Any, (server, context) =>
            {
                FeatureContext featureContext = new FeatureContext(context);

                object applicationContext = application.CreateContext(featureContext.Features);

                application.ProcessRequestAsync(applicationContext).Wait();
                
                featureContext.OnStart().Wait();

                //context.Dispose();
                application.DisposeContext(applicationContext, null);

                featureContext.OnCompleted().Wait();




                return true;
            });
        }
    }
}
