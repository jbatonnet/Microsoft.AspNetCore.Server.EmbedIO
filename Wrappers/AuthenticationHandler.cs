using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.Extensions.Primitives;

namespace Microsoft.AspNetCore.Server.EmbedIO
{
    public class AuthenticationHandler : IAuthenticationHandler
    {
        public Task AuthenticateAsync(AuthenticateContext context)
        {
            throw new NotImplementedException();
        }

        public Task ChallengeAsync(ChallengeContext context)
        {
            throw new NotImplementedException();
        }

        public void GetDescriptions(DescribeSchemesContext context)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(SignInContext context)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync(SignOutContext context)
        {
            throw new NotImplementedException();
        }
    }
}
