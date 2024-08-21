using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IAuthenticate _authentication;
    private readonly IConfiguration _configuration;

    public TokenController(IAuthenticate authentication, IConfiguration configuration)
    {
        _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
        _configuration = configuration;
    }


    [HttpPost("LoginUser")]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel login)
    {

        var result = await _authentication.Authenticate(login.Email, login.Password);

        if (result)
        {
            return GenerateToken(login);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }
    }
    [HttpPost("CreateUser")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> CreateUser([FromBody] RegisterModel user)
    {

        var result = await _authentication.RegisterUser(user.Email, user.Password);

        if (result)
        {
            return Ok($"User {user.Email} was create successfully!");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }
    }

    private UserToken GenerateToken(LoginModel login)
    {
        //declaracoes do usuario
        var claims = new[]
        {
            new Claim("email", login.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


        //gerar chave privada para assinar o token
        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        //gerar a assinatura digital
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        //definir o tempo de expiração
        var expiration = DateTime.UtcNow.AddMinutes(10);

        //gerar o token
        JwtSecurityToken token = new JwtSecurityToken(
            //emissor
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration
        );


        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}

