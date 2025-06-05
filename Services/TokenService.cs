// using System;
// using System.IdentityModel.Tokens.Jwt;
// using System.Text;
// using WebApplication1.Models;
// using Microsoft.IdentityModel.Tokens;
// using System.Security.Claims;

// namespace WebApplication1.Services;

// public class TokenService
// {
//     public string Generate(User user)
//     {
//         var handler = new JwtSecurityTokenHandler();

//         var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);

//         var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

//         var tokenDescriptor = new SecurityTokenDescriptor
//         {
//             Subject = GenerateClaims(user),
//             SigningCredentials = credentials,
//             Expires = DateTime.UtcNow.AddHours(2)
//         };
//         var token = handler.CreateToken(tokenDescriptor);

//         return handler.WriteToken(token);
//     }

//     private static ClaimsIdentity GenerateClaims(User user)
//     {
//         var ci = new ClaimsIdentity();
//         ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
//         foreach (var role in user.Roles)
//         ci.AddClaim(new Claim(ClaimTypes.Role, role));

//             ci.AddClaim(new Claim("Fruta", "Banana"));
//         return ci;
//     }
// }
