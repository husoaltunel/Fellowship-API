using Core.Entities.Concrete;
using Core.Utilities.Security.Abstract;
using Core.Utilities.Security.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private IConfiguration _configuration;
        private TokenOptions tokenOptions;
        private DateTime accessTokenExpiration;
        private List<Claim> claims;
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
            accessTokenExpiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);
            claims = new List<Claim>();
        }
        public AccessToken CreateToken(User user, List<OperationClaim> userClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSymmetricSecurityKey(tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            ClaimsHelper.SetClaims(user,claims,userClaims);
            var jwt = SetJwt(tokenOptions,claims,signingCredentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new AccessToken() { Token = token, Expiration = accessTokenExpiration };
        }

        
        public JwtSecurityToken SetJwt(TokenOptions tokenOptions,List<Claim> claims,SigningCredentials signingCredentials)
        {
            return new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: signingCredentials
                );
        }

    }
}
