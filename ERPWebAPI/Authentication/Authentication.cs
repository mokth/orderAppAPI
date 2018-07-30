//using ERPWebAPI.Model;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Primitives;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Text.Encodings.Web;
//using System.Threading.Tasks;

//namespace ERPWebAPI.Authentication
//{
//    public interface IApiKeyValidator
//    {
//        Task<bool> ValidateAsync(string apiKey);
//    }

//    public class MyApiKeyValidatorImpl : IApiKeyValidator
//    {
//        public async Task<bool> ValidateAsync(string apiKey)
//        {
//            return await Task.Run<bool>(()=>
//                     {
//                         return JWTokenHelper.ValidateToken(apiKey);
                         
//                     }); 
//        }
//    }

//    public class ApiKeyAuthenticationOptions : AuthenticationOptions
//    {
//        public const string DefaultHeaderName = "Authorization";
//        public string HeaderName { get; set; } = DefaultHeaderName;

//        public ApiKeyAuthenticationOptions() : base()
//        {
//            AuthenticationScheme = "apikey";
//        }
//    }

//    public class ApiKeyAuthenticationMiddleware : AuthenticationMiddleware<ApiKeyAuthenticationOptions>
//    {
//        private IApiKeyValidator _validator;
//        public ApiKeyAuthenticationMiddleware(
//           IApiKeyValidator validator,  // custom dependency
//           RequestDelegate next,
//           IOptions<ApiKeyAuthenticationOptions> options,
//           ILoggerFactory loggerFactory,
//           UrlEncoder encoder)
//           : base(next, options, loggerFactory, encoder)
//        {
//            _validator = validator;
//        }

//        protected override AuthenticationHandler<ApiKeyAuthenticationOptions> CreateHandler()
//        {
//            return new ApiKeyAuthenticationHandler(_validator);
//        }
//    }

//    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
//    {
//        private IApiKeyValidator _validator;
//        public ApiKeyAuthenticationHandler(IApiKeyValidator validator)
//        {
//            _validator = validator;
//        }

//        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
//        {
//            StringValues headerValue;
//            AuthenticateResult result;
//            if (!Context.Request.Headers.TryGetValue(Options.HeaderName, out headerValue))
//            {
//                result = AuthenticateResult.Fail("Missing or malformed 'Authorization' header.");
//                return result;
//            }

//            var apiKey = headerValue.First();
//            bool success = await _validator.ValidateAsync(apiKey);
//            if (!success)
//            {
//                return AuthenticateResult.Fail("Invalid API key.");
//            }

//            // success! Now we just need to create the auth ticket
//            var identity = new ClaimsIdentity("apikey"); // the name of our auth scheme
//                                                         // you could add any custom claims here
//            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), null, "apikey");
//            return AuthenticateResult.Success(ticket);
//        }
//    }
//}
