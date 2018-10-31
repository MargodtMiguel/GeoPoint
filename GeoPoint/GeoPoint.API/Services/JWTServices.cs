using GeoPoint.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GeoPoint.API.Services
{
    public class JWTServices<TEntity> where TEntity : IdentityUser
    {
        ///private readonly ILogger logger;
        private readonly UserManager<TEntity> userManager;
        private readonly IPasswordHasher<TEntity> hasher;
        private readonly IConfiguration configuration;

        public JWTServices(IConfiguration configuration, UserManager<TEntity> userManager, IPasswordHasher<TEntity> hasher)
        {
            this.userManager = userManager;
            this.hasher = hasher;
            this.configuration = configuration;
        }

        public async Task<object> GenerateJwtToken(IdentityModel identityModel)
        {
            try
            {
                TEntity user = await userManager.FindByNameAsync(identityModel.UserName);

                if (user != null)
                {
                    if (hasher.VerifyHashedPassword(user, user.PasswordHash, identityModel.Password) == PasswordVerificationResult.Success)
                    {
                        Console.Write("password verified: {0}", user.PasswordHash);
                    }

                }
                else
                {
                    return new { error = "Unknown user or password" };
                }
                await userManager.AddClaimAsync(user, new Claim("myExtraKey", "myExtraValue"));
                var userClaims = await userManager.GetClaimsAsync(user);
                var claims = new List<Claim>
             {
                 new Claim(JwtRegisteredClaimNames.Sub, identityModel.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

             }.Union(userClaims);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                 issuer: configuration["Tokens:Issuer"],
                 audience: configuration["Tokens:Audience"],
                 claims: claims,
                 expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Tokens: Expires"])),
                 signingCredentials: creds
                 );
                return new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };
            }
            catch (Exception exc)
            {
                //logger.LogError($"Exception thrown when creating JWT: {exc}");
            }
            return new { error = "Failed to generate JWT token" };
        }
    }
}