using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MTTWebAPI.Domain.Services.Abstract;
using MTTWebAPI.WebUI.Helpers;

namespace MTTWebAPI.WebUI.Security
{
	public class TokenInspector : DelegatingHandler
    {
        private readonly IWebSecurityService _webSecurityService;

        public TokenInspector(IWebSecurityService pWebSecurityService)
        {
            _webSecurityService = pWebSecurityService;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            const string tokenName = "X-Token";

            if (request.Headers.Contains(tokenName))
            {
                string encryptedToken = request.Headers.GetValues(tokenName).First();
                try
                {
                    Token token = Token.Decrypt(encryptedToken);
                    bool isValidUserId = _webSecurityService.UserExists(token.UserId);// IdentityStore.IsValidUserId(token.UserId);
                    bool requestIPMatchesTokenIP = token.IP.Equals(request.GetClientIP());

                    if (!isValidUserId || !requestIPMatchesTokenIP)
                    {
                        HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid indentity or client machine.");
                        return Task.FromResult(reply);
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid token.");
                    return Task.FromResult(reply);
                }
            }
            else
            {
                HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Request is missing authorization token.");
                return Task.FromResult(reply);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}