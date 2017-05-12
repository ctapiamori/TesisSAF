using Microsoft.Owin.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;

namespace SOCAUDAPI.Providers
{
    public class CustomPolicyProvider : Attribute, ICorsPolicyProvider
    {
        private CorsPolicy _policy;

        public CustomPolicyProvider()
        {
            // Create a CORS policy.
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true,
                AllowAnyOrigin = true
            };

            // Magic line right here
            _policy.Origins.Add("*");

        }

        //public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(_policy);
        //}

        public Task<CorsPolicy> GetCorsPolicyAsync(Microsoft.Owin.IOwinRequest request)
        {
            return Task.FromResult(_policy);
        }
    }
}