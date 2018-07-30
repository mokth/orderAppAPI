using Microsoft.AspNetCore.Http;
using galaCoreAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace galaCoreAPI.Authentication
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(UserInfo user);
        UserInfo DecodedRequestAuth(HttpRequest Request);
        UserInfo DecodedToken(string token);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
