using galaCoreAPI.Authentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using galaCoreAPI.Model;
using Microsoft.AspNetCore.Http;

namespace galaCoreAPI.Authentication
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<string> GenerateEncodedToken(UserInfo user )
        {
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.fullname),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 new Claim(JwtRegisteredClaimNames.Exp, ToUnixEpochDate(_jwtOptions.Expiration).ToString(), ClaimValueTypes.Integer64),

                 new Claim("rol", Constants.Strings.JwtClaims.ApiAccess),
                 new Claim("id", user.name ),
                 new Claim("fullname", user.fullname ),
                 new Claim("company", user.companyCode),
          
               
                 //identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol),
                 //identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id)
             };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            // new JwtSecurityTokenHandler().ReadJwtToken("").Claims.Where(x=>x.)
            return encodedJwt;
        }
        public UserInfo DecodedRequestAuth(HttpRequest Request)
        {
            UserInfo user = new UserInfo();
            string authHeader = Request.Headers["Authorization"];
            user = DecodedToken(authHeader);

            return user;
        }

        public UserInfo DecodedToken(string token)
        {
            token = token.Replace("Bearer ", "");
            UserInfo user = new UserInfo();
            var decode = new JwtSecurityTokenHandler().ReadJwtToken(token);
            if (decode != null)
            {
              
                user.companyCode = decode.Claims.Where(x => x.Type == "company").FirstOrDefault().Value;
               }

            return user;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess)
            });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
