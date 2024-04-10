
using Api.Entities;
using Api.Services;
using Common.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

namespace Api.Controllers
{
    [ApiController]
    [Route("/v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly HttpUtil httpUtil = new HttpUtil();
        private readonly IConfiguration config;
        private readonly ILogger<UsuarioController> logger;
        private IUsuarioService oService;

        public UsuarioController(
            IConfiguration config,
            ILogger<UsuarioController> logger,
            IUsuarioService oService)
        {
            this.config = config;
            this.logger = logger;
            this.oService = oService;
        }
        
        [HttpGet]
        [Route("/v1/usuario/listar")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<List<UsuarioResult>> Listar()
        {
            var list = this.oService.Listar();
            return Ok(list);

        }

        [HttpGet]
        [Route("/v1/usuario/buscar")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<List<UsuarioResult>> Buscar( string texto)
        {   
            var list = this.oService.Buscar( texto );
            return Ok(list);
        }

        [HttpPost("/v1/usuario/login")]
        [AllowAnonymous]
        public Task<IActionResult> Login(string codigo, string pwd)
        {
            JwtSecurityToken token;

            //Tu validación de usuario password
            UsuarioResult oU = this.oService.Login(codigo, pwd).FirstOrDefault();

            if (oU != null && oU.usuario != null)
            { 
                var claims = new Claim[] {
                    new(ClaimTypes.Sid, "10"),
                    new(ClaimTypes.Role, "Administrador")
                };

                var llave = Encoding.UTF8.GetBytes(this.config["TOKENS_KEY"]);
                var key = new SymmetricSecurityKey(llave);
                var creds = new SigningCredentials(key,
                                               SecurityAlgorithms.HmacSha256);
                token = new JwtSecurityToken(this.config["TOKENS_ISSUER"],
                            this.config["TOKENS_ISSUER"],
                            claims,
                            expires: DateTime.Now.AddDays(30),
                            signingCredentials: creds);

                var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
                return Task.FromResult<IActionResult>(Ok(tokenHandler));
            }
            else
                return Task.FromResult<IActionResult>(Ok("Acceso Denegado"));
        }
    }
}
