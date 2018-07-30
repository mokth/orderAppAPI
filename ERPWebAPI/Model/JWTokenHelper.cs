using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jose;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using galaCoreAPI.Model;

namespace galaCoreAPI.Model
{
    public class JWTokenHelper
    {
        protected static readonly byte[] secretKey = Base64UrlDecode("Itd0tX3+1BBP2DZwE7FfDjtaYC1sUgexPIXl+PILB8E=");

        public static bool ValidateToken(string token)
        {
            bool isValid = false;
            string[] para = token.Split(';');
            try
            {
                string decode = JWT.Decode(para[1], secretKey, JwsAlgorithm.HS256);
                var jsonobj= JObject.Parse(decode);
                if (para[0] == jsonobj["aud"].ToString())
                {
                    Double second = Convert.ToDouble(jsonobj["exp"]);
                    DateTime expiry= UnixTimeStampToDateTime(second);
                    isValid = expiry >= DateTime.Now;                    
                }
            }
            catch
            {
            }
            return isValid;
        }

        public static UserInfo DecodeToken(string token)
        {
            string[] para = token.Split(';');
            UserInfo user = new UserInfo();
            try
            {
                string decode = JWT.Decode(para[1], secretKey, JwsAlgorithm.HS256);
                var jsonobj = JObject.Parse(decode);
                if (para[0] == jsonobj["aud"].ToString())
                {
                    user.name = jsonobj["aud"].ToString();
                    user.companyCode= jsonobj["company"].ToString();
                  
                }
            }
            catch
            {
            }
            return user;
        }

        public static string GenerateJWT(UserInfo user)
        {
            
            DateTime issued = DateTime.Now;
            DateTime expire = DateTime.Now.AddHours(10);

            var payload = new Dictionary<string, object>()
            {
                {"iss", "https://www.wincom3cloud.com/"},
                {"aud", user.name},
                {"name", user.fullname},
                {"iat", ToUnixTime(issued).ToString()},
                {"exp", ToUnixTime(expire).ToString()},
                {"company", user.companyCode}
                
            };

            string token = JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);

            return token;
        }

        /// <remarks>
        /// Take from http://stackoverflow.com/a/33113820
        /// </remarks>
        static byte[] Base64UrlDecode(string arg)
        {
            string s = arg;
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding
            switch (s.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default:
                    throw new System.Exception(
             "Illegal base64url string!");
            }
            return Convert.FromBase64String(s); // Standard base64 decoder
        }

        static long ToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        //public static UserInfo GetToken(HttpRequest Request)
        //{
        //    UserInfo user = new UserInfo();
        //    string authHeader = Request.Headers["Authorization"];
        //    user = JWTokenHelper.DecodeToken(authHeader);

        //    return user;
        //}
    }
}
