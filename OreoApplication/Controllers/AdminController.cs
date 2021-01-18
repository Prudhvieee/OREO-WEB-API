using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OreoAppBussinessLayer.IBussinessLayer;
using OreoAppCommonLayer.Model;

namespace OreoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public IAdminBL adminBL;
        IConfiguration configuration;
        public AdminController(IAdminBL adminBL, IConfiguration configuration)
        {
            this.adminBL = adminBL;
            this.configuration = configuration;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult RegisterAdmin(AdminRegister admin)
        {
            try
            {
                var result = this.adminBL.RegisterAdmin(admin);
                if (result)
                {
                    return this.Ok(new { Success = true, Message = "Admin record added successfully",Data=result });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { Success = false, Message = "Admin record is not added", Data = result });
                }
            }
            catch (Exception e)
            {
                var sqlException = e.InnerException as SqlException;

                if (sqlException.Number == 2601 || sqlException.Number == 2627)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { Success = false, ErrorMessage = "Cannot insert duplicate Email values",Data= sqlException });
                }
                else
                {
                    return this.BadRequest(new { Success = false, Message = e.Message,Data=e.Data });
                }
            }
        }
        [HttpPost("Login")]
        public ActionResult AdminLogin(AdminLogin Admin)
        {
            try
            {
                var result = this.adminBL.AdminLogin(Admin);
                if (result != null)
                {
                    string token = GenrateJWTToken(result.Email, result.AdminId, result.Role);
                    return this.Ok(new{Success = true,Message = "Admin logged in successfully",Data = result,Token = token});
                }
                else
                {
                    return this.NotFound(new { Success = false, Message = "Admin logged in unsuccessfully", Data = result });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message,Data=e.Data});
            }
        }
        private string GenrateJWTToken(string email, long id, string Role)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            string userId = Convert.ToString(id);
            var claims = new List<Claim>
                        {
                            new Claim("email", email),
                            new Claim(ClaimTypes.Role, Role),
                            new Claim("id",userId),
                        };
            var tokenOptionOne = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }
    }
}
