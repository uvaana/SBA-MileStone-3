using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StuAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using StuAPI.Models;

namespace UploadImage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        StuDBContext db;
        private IConfiguration config;
        public LoginController(StuDBContext _db, IConfiguration _config)
        {
            db = _db;
            config = _config;
        }
        [HttpPost]
        [Route("login-user")]
        public IActionResult Login(TblLogin tblLogin)
        {
            if (db.TblLogins.Any(x => x.Password == tblLogin.Password && x.UserName == tblLogin.UserName))
            {
                var token = GenerateToken(tblLogin);
                return Ok(new { Token = token, Message = "successful login" });
            }
            else
            {
                return Ok(new { Token = "", Message = "userName or password is incorrect" });
            }

        }

        [HttpPost]
        [Route("register-user")]
        public IActionResult Register(TblLogin tblLogin)
        {
            if (db.TblLogins.Any(x => x.UserName == tblLogin.UserName))
            {
                return Ok(new { Token = "", Message = "email id already exists!!.Please use different email" });
            }
            else
            {
                db.TblLogins.Add(tblLogin);
                db.SaveChanges();
                var Token = GenerateToken(tblLogin);
                return Ok(new { Token = Token, Message = "user added successfully" });
            }
        }

        private string GenerateToken(TblLogin login)
        {
            var key = config["jwt:key"];
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, login.UserName)
                }),
                Expires = DateTime.Now.AddMinutes(120),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDiscriptor));
        }
    }
}