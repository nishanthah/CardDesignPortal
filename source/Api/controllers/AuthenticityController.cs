using System;
using System.Collections.Generic;
using Db.Mysql;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Card.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiJwtAuthDemo.Options;
using System.IdentityModel.Tokens.Jwt;




namespace Card.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = -1)]
    public class AuthenticityController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;

        public AuthenticityController(IOptions<JwtIssuerOptions> jwtOptions, ILoggerFactory loggerFactory)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);

            _logger = loggerFactory.CreateLogger<AuthenticityController>();

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Authenticity")]
        public async Task<IActionResult> authToken([FromForm]LoginEntity loginObj)
        {
            var identity = await GetClaimsIdentity(loginObj);
            if (identity == null)
            {
                _logger.LogInformation($"Invalid username ({loginObj.username}) or password ({loginObj.password})");
                return BadRequest("Invalid credentials");
            }
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, loginObj.username),
        new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
        identity.FindFirst("PortalCharacter")
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

            // Serialize and return the response
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);

        }

        // private string GenerateToken()
        // {
        //     byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        //     byte[] key = Guid.NewGuid().ToByteArray();
        //     string tokensss = Convert.ToBase64String(time.Concat(key).ToArray());
        //     return tokensss;
        // }

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

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        /// <summary>
        /// IMAGINE BIG RED WARNING SIGNS HERE!
        /// You'd want to retrieve claims through your claims provider
        /// in whatever way suits you, the below is purely for demo purposes!
        /// </summary>
        private static Task<ClaimsIdentity> GetClaimsIdentity(LoginEntity user)
        {

            try
            {
                int count = 0;
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader reader = connect.MysqlExecuteQuery("select * from user where (username ='" + user.username + "' && password ='" + user.password + "' )");

                    while (reader.Read())
                    {
                        count++;
                    }
                }
                if (count > 0)
                {


                    return Task.FromResult(new ClaimsIdentity(new GenericIdentity(user.username, "Token"),
                          new[]
                          {
                    new Claim("PortalCharacter", "cardPortalKey")
                          }));
                }
                else
                {
                //     return Task.FromResult(new ClaimsIdentity(new GenericIdentity(user.username, "Token"),
                //   new Claim[] { }));
                Task.FromResult<ClaimsIdentity>(null);
                }
            }
            catch (MySqlException ex)
            {
                Task.FromResult<ClaimsIdentity>(null);
            }
            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }

    }

}