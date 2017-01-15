using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiJwtAuthDemo.Options;
using System.IdentityModel.Tokens.Jwt;
using Card.Models;

namespace Card.Controllers
{
    [Route("api/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;
        private static IDbRepository<User> UserDbRepository { get; set; }

        public AuthenticationController(IOptions<JwtIssuerOptions> jwtOptions, ILoggerFactory loggerFactory, IDbRepository<User> iDbRepository = null)
        {
            UserDbRepository = iDbRepository;
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);

            _logger = loggerFactory.CreateLogger<AuthenticationController>();

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AuthToken([FromForm]User loginObj)
        {
            var identity = await GetClaimsIdentity(loginObj);
            if (identity == null)
            {
                _logger.LogInformation($"Invalid username ({loginObj.UserName}) or password ({loginObj.Password})");
                return BadRequest("Invalid credentials");
            }
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, loginObj.Password),
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
        private static Task<ClaimsIdentity> GetClaimsIdentity(User user)
        {
            try
            {
                IEnumerable<User> userDetailsList = UserDbRepository.GetAll();
                User existingUser = userDetailsList
                .Where(x => x.UserName == user.UserName)
                .Where(x => x.Password == user.Password)
                .Where(x => x.IsActive == true)
                .FirstOrDefault();

                if (existingUser != null)
                {
                    return Task.FromResult(new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"),
                  new[]
                  {
                    new Claim("PortalCharacter", "cardPortalKey")
                  }));
                }
                else
                {
                    return Task.FromResult<ClaimsIdentity>(null);
                }
            }
            catch (MySqlException mySqlException)
            {
                return Task.FromResult<ClaimsIdentity>(null);
            }
        }
    }
}