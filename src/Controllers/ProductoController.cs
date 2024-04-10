
using Api.Entities;
using Api.Services;
using Common.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Api.Controllers
{
    [ApiController]
    [Route("/v1/producto")]
    public class ProductoController : ControllerBase
    {
        private readonly HttpUtil httpUtil = new HttpUtil();
        private readonly IConfiguration config;
        private readonly ILogger<ProductoController> logger;
        private IProductoService oService;

        public ProductoController(
            IConfiguration config,
            ILogger<ProductoController> logger,
            IProductoService oService)
        {
            this.config = config;
            this.logger = logger;
            this.oService = oService;
        }
        
        [HttpGet]
        [Route("/v1/producto/listar")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<List<ProductoResult>> Listar()
        {
            var list = this.oService.Listar();
            return Ok(list);

        }

        [HttpGet]
        [Route("/v1/producto/buscar")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<List<ProductoResult>> Buscar( string texto)
        {   
            var list = this.oService.Buscar( texto );
            return Ok(list);
        }
    }
}
