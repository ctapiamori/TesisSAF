using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SOCAUDAPI.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            if (context.IsTokenEndpoint && context.Request.Method == "OPTIONS")
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "false" });
                context.OwinContext.Response.Headers.Add("credentials", new[] { "false" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "Authorization ,GET, POST, PUT, DELETE, HEAD, OPTIONS, header" });

                // IE - EDGE
                if (System.Text.RegularExpressions.Regex.IsMatch(context.OwinContext.Request.Headers.Get("User-Agent"), @"(?:\b(MS)?IE\s+|\bTrident\/7\.0;.*\s+rv:)(\d+)"))
                    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization" });
                else
                    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Authorization ,GET, POST, PUT, DELETE, HEAD, OPTIONS, header" });
                   

                context.RequestCompleted();
                return Task.FromResult(0);
            }

            return base.MatchEndpoint(context);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
             

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }


    }
}