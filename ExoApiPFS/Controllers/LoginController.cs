using ExoApiPFS.Interfaces;
using ExoApiPFS.Models;
using ExoApiPFS.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExoApiPFS.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUsuarioRepository _iUsuarioRepository;

        public LoginController(IUsuarioRepository iUsuarioRepository)
        {
            _iUsuarioRepository = iUsuarioRepository;
        }



        /// <summary>
        /// Fazer Login com algum usuário
        /// </summary>
        /// <param name="dados"></param>
        /// <returns>O token para o usuário para que o mesmo possa realizar métodos sobre a tabela de Projetos.</returns>
        /// <response code="200">Retorna o token para o usuário</response>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Login(LoginViewModel dados)
        {
            try
            {
                Usuario usuarioEncontrado = _iUsuarioRepository.Login(dados.Email, dados.Senha);

                if (usuarioEncontrado == null)
                {
                    return Unauthorized(new { msg = "E-mail e/ou senha incorretos" });
                }

                var minhasClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuarioEncontrado.Tipo)
                };

                var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao"));

                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                var meuToken = new JwtSecurityToken(
                    issuer: "exoapi.webapi",
                    audience: "exoapi.webapi",
                    claims: minhasClaims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credenciais
                    );

                return Ok( 
                        new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(meuToken)
                    }
                );

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
