//HospitalManagement.API/Controllers/AuthController.cs
using Azure.Core;
using HospitalManagement.API.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginRequest = HospitalManagement.API.Models.Auth.LoginRequest;



namespace HospitalManagement.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IConfiguration _config;

        //public AuthController(IOptions<JwtSettings> jwtSettings)
        //{
        //    _jwtSettings = jwtSettings.Value;
        //}
        public AuthController(IOptions<JwtSettings> jwtSettings, IConfiguration config)
        {
            _config = config;
            _jwtSettings = jwtSettings.Value;
        }

        //[HttpPost("login1")]
        //public IActionResult Login1([FromBody] LoginRequest request)
        //{
        //    // NOTE: Replace this with real DB lookup or user service
        //    if (request.Username != "admin" || request.Password != "password")
        //        return Unauthorized();

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] {
        //        new Claim(ClaimTypes.Name, request.Username)
        //    }),
        //        Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
        //        Issuer = _jwtSettings.Issuer,
        //        Audience = _jwtSettings.Audience,
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return Ok(new { token = tokenHandler.WriteToken(token), username = request.Username });
        //}

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "admin123")
            {
                var token = GenerateJwtToken(request.Username, request);
                return Ok(new { token, username = request.Username });
            }
            return Unauthorized();
        }


        private string GenerateJwtToken(string username, LoginRequest request)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );



            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new[] {
            //    new Claim(ClaimTypes.Name, request.Username)
            //}),
            //    Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            //    Issuer = _jwtSettings.Issuer,
            //    Audience = _jwtSettings.Audience,
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);
            //return Ok(new { token = tokenHandler.WriteToken(token) });


            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.Name, username)
            //};

            //var token = new JwtSecurityToken(
            //    issuer: _config["JwtSettings:Issuer"],
            //    audience: _config["JwtSettings:Audience"],
            //    claims: claims,
            //    expires: DateTime.UtcNow.AddHours(1),
            //    signingCredentials: credentials
            //);

            //return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
