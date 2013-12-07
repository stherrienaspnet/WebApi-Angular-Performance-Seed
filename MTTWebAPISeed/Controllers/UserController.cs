using MTTWebAPI.Domain.Repositories.Abstract;
using MTTWebAPI.Domain.Services.Abstract;
using MTTWebAPISeed.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MTTWebAPI.WebUI.Helpers;
using MTTWebAPI.WebUI.Security;

namespace MTTWebAPI.WebUI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IWebSecurityService webSecurityService;

        public UserController(IWebSecurityService webSecurityService)
        {
            if (webSecurityService == null)
            {
                throw new ArgumentNullException("webSecurityService");
            }

            this.webSecurityService = webSecurityService;
        }

        public Status Authenticate(User user)
        {
            if (user == null)
                throw new HttpResponseException(new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Please provide the credentials.") });

            if (webSecurityService.Login(user.UserId, user.Password))
            {
                Token token = new Token(user.UserId, Request.GetClientIP());
                return new Status { Successeded = true, Token = token.Encrypt(), Message = "Successfully signed in." };
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Invalid user name or password.") });
            }
        }
    }

}
