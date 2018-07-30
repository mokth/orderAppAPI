using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using galaCoreAPI.Model;
using ERPWebAPI.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using galaCoreAPI.Authentication;
using Microsoft.Extensions.Options;
using galaCoreAPI.Authentication.Model;
using Newtonsoft.Json;
using galaCoreAPI.Entities;
using Microsoft.Extensions.Configuration;
using galaEatAPI.Services;

namespace ERPWebAPI.Controllers
{
    // [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly DataDbContect _context;
        private readonly ICommonService _configuration;

        public AuthController(DataDbContect context,IHostingEnvironment hostingEnvironment, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, ICommonService configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _context = context;
            _configuration = configuration;
        }

      
        [HttpPost, Route("jwt")]
        [AllowAnonymous]
        public async Task<JsonResult> GetJWT([FromBody]UserInfo user)
        {
            JsonResult categoryJson = null;
            if (AuthHelper.CheckValidUser(user))
            {
                var jwt = await Tokens.GenerateJwt(user, _jwtFactory, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
                categoryJson = new JsonResult(jwt);
            } else categoryJson = new JsonResult("Invalid user/password");

            return categoryJson;

        }

        [HttpPost, Route("jwt1")]
        public async Task<JsonResult> GetJWT1([FromBody]UserInfo user)
        {
            JsonResult categoryJson = null;
            if (AuthHelper.CheckValidUserNormal(user))
            {
                var jwt = await Tokens.GenerateJwt(user, _jwtFactory, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
                categoryJson = new JsonResult(jwt);
                categoryJson = Json(new
                {
                    ok = "yes",
                    error = "Success",
                    chgpass= user.access,
                    data = jwt
                });

            }
            else {
                categoryJson = Json(new
                {
                    ok = "no",
                    error = "Invalid user/password",
                    chgpass ="",
                    data = ""
                });
            } 

            return categoryJson;

        }

        [HttpPost, Route("access")]
        public JsonResult GetAccessRight([FromBody]UserInfo user)
        {
            JsonResult categoryJson = null;
            //make used of user.fullname to be the ERP screen id
            bool isvalid = AuthHelper.IsValidAccessRight(user.name, user.fullname);
            categoryJson = Json(new
            {
                ok = (isvalid) ? "YES" : "NO"                
            });

            return categoryJson;

        }

     
        [HttpGet("reset/{userid}")]
        public JsonResult ResetPassword(string userid)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            JsonResult categoryJson = null;
            string msg = "";
            if (_context.ResetPassowrd(userid, _configuration, webRootPath,ref msg))
            {
                categoryJson = Json(new
                {
                    ok = "OK",
                    Error = msg
                });
            }
            else
            {
                categoryJson = Json(new
                {
                    ok = "",
                    Error = msg
                });
            }

            return categoryJson;
        }

        [HttpPost, Route("change")]
        public async Task<JsonResult> ChangePassword([FromBody]UserInfo user)
        {
            user.password = user.access;
            string newpass = user.fullname;
            if (!AuthHelper.CheckValidUserNormal(user))
            {
               return Json(new
                {
                    ok =  "no",
                    error = "Invalid userid/password..."
                });
            }

            string msg = "";
            string fullname = user.fullname;
            user.fullname = AuthHelper.HashString(newpass);
            bool success =  _context.ChangePassowrd(user, ref msg);
            string jwt = "";
            if (success)
            {
                user.fullname = fullname;
                jwt = await Tokens.GenerateJwt(user, _jwtFactory, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
            JsonResult restultJson = Json(new
            {
                ok = (success) ? "yes" : "no",
                data = jwt,
                error = msg
            });

            return restultJson;
        }

       

      
    }
}