using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApplication1.Models;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Services;

public class TokenService
{
    public string Generate()
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2)
        };
        var token = handler.CreateToken(tokenDescriptor);

      return handler.WriteToken(token);
    }


}
