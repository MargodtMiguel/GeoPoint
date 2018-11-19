using AspNetCore.Identity.Mongo.Model;
using GeoPoint.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GeoPoint.API.Services
{
        public class JWTServices<TEntity> where TEntity : MongoUser
        {

            private readonly UserManager<TEntity> userManager;
            private readonly IPasswordHasher<TEntity> hasher;
            private readonly IConfiguration configuration;
            private readonly ILogger logger;
            public JWTServices(IConfiguration configuration, UserManager<TEntity> userManager, IPasswordHasher<TEntity> hasher, ILogger logger)
            {
                this.userManager = userManager;
                this.hasher = hasher;
                this.configuration = configuration;
                this.logger = logger;
            }

            //returnen van een anoniem object
            public async Task<object> GenerateJwtToken(IdentityModel identityModel)
            {
                //0. Geen signin manager -> deze signin manager maakt autorisatie cookie aan 
                //signin manager gebruiker zou kunnen als backup voor een falend token.

                //1. Gebruiker opzoeken in de database met async UserManager en hash vergelijking
                try
                {
                    TEntity user = await userManager.FindByNameAsync(identityModel.Username);

                    if (user != null)
                    {
                        //identity control op de hash (=kan zonder signin)
                        if (hasher.VerifyHashedPassword(user, user.PasswordHash, identityModel.Password) == PasswordVerificationResult.Success)
                        {
                            //code hier indien paswoord controle.
                            Console.Write("password verified: {0}", user.PasswordHash);
                        }
                        else
                        {
                            return new IdentityError { Code= "400", Description= "Incorrect password"};
                        }

                    }
                    else
                    {
                        return new IdentityError { Code = "400", Description = "User was not found" };
                }
                   

                    //2. claims (key/value) toevoegen
                    //2.1 Customised of extra claims, komende van het Identity system:
                    await userManager.AddClaimAsync(user, new Claim("myExtraKey", "myExtraValue")); //testpurposes
                                                                                                    //await _userManager.AddToRoleAsync(user,"SuperUser");
                    var userClaims = await userManager.GetClaimsAsync(user);

                    //2.2. Noodzakelijke claims komende vd JWD spec
                    var claims = new List<Claim>
             {
                //JWT claims zijn ingebouwd in de JWT spec: subscriber, JWT Id
                 new Claim(JwtRegisteredClaimNames.Sub, identityModel.Username),  //subscriber
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                 //extra claims (waardoor toch datastore info beschikbaar wordt)
                 new Claim(ClaimTypes.Name, user.Id.ToString())
             }.Union(userClaims);   //nog de extra userClaims toevoegen.

                    //3. Sigin credentials met de symmetric key & encryptie methode
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //key en protocol

                    //4. aanmaken van het token
                    var token = new JwtSecurityToken(
                     issuer: configuration["Tokens:Issuer"],  //onze website
                     audience: configuration["Tokens:Audience"],//gebruikers
                     claims: claims,
                     expires: DateTime.Now.AddMinutes(Convert.ToDouble(40)),
                     signingCredentials: creds  //controleert token v
                     );
                Debug.WriteLine(token.ValidTo);
                    //5. user info returnen (vervaldatum als additionele info)
                    return new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token), 
                        expiration = token.ValidTo
                    };
                }
                catch (Exception exc)
                {
                  logger.LogError($"\r\n\r\nError thrown on JWTService - (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when using JWTService: " + exc + "\r\n\r\n");
                }
            return new { error = "Failed to generate JWT token" }; //minimale info ->meer in de logger
        }
        }

    }
