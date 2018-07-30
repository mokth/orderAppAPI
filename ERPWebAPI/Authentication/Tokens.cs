using galaCoreAPI.Authentication.Model;
using Newtonsoft.Json;
using galaCoreAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galaCoreAPI.Authentication
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(UserInfo user, IJwtFactory jwtFactory,  JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = user.name,
                auth_token = await jwtFactory.GenerateEncodedToken(user),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
