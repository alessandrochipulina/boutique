
using Api.Entities;
using Api.Services;
using Common.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [ApiController]
    [Route("/v1/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly HttpUtil httpUtil = new HttpUtil();
        private readonly IConfiguration config;
        private readonly ILogger<PedidoController> logger;
        private IPedidoService oService;

        public PedidoController(
            IConfiguration config,
            ILogger<PedidoController> logger,
            IPedidoService oService)
        {
            this.config = config;
            this.logger = logger;
            this.oService = oService;
        }

        [HttpGet]
        [Route("/v1/pedido/listar")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<List<PedidoResult>> Listar()
        {
            var list = this.oService.Listar();
            return Ok(list);
        }

        [HttpGet]
        [Route("/v1/pedido/buscar")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<List<PedidoResult>> Buscar(
            string texto)
        {
            var list = this.oService.Buscar(texto);
            return Ok(list);
        }

        [HttpPatch]
        [Route("/v1/pedido/agregarproducto")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<SimpleResult> AgregarProducto(
            int id_pedido,
            string sku, 
            int precio, 
            int cantidad )
        {
            try
            {
                var result = this.oService.AgregarProducto(
                    id_pedido,
                    sku, 
                    precio, 
                    cantidad );
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            
        }

        [HttpPut]
        [Route("/v1/pedido/crear")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<SimpleResult> Crear(
            string codigo_usuario)
        {
            try
            {
                var result = this.oService.Crear(codigo_usuario);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            
        }

        [HttpDelete]
        [Route("/v1/pedido/cancelar")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<SimpleResult> Cancelar(
            int id_pedido)
        {
            try
            {
                var result = this.oService.Cancelar(id_pedido);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            
        }

        [HttpPost]
        [Route("/v1/pedido/recuperar")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<SimpleResult> Recuperar(
            int id_pedido)
        {
            try
            {
                var result = this.oService.Recuperar(id_pedido);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }           
        }


        [HttpPost]
        [Route("/v1/pedido/atender")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<SimpleResult> Atender(
            int id_pedido,
            string codigo_usuario)
        {
            try
            {
                var result = this.oService.Atender(
                    id_pedido,
                    codigo_usuario);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
