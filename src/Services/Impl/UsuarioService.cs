using Api.Entities;
using Api.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Common.Attributes;

namespace Api.Services.Impl
{
    [Service(Scope="Transient")]
    public class UsuarioService : IUsuarioService
    {
        private readonly IConfiguration config;
        private readonly IUsuarioRepository oRepository;
        private readonly ILogger<UsuarioService> logger;

        public UsuarioService(
            IConfiguration config,
            IUsuarioRepository oRepository,
            ILogger<UsuarioService> logger)
        {
            this.config = config;
            this.oRepository = oRepository;
            this.logger = logger;
        }

        public List<UsuarioResult> Listar()
        {
            List<UsuarioResult> lst;

            try
            {
                lst = this.oRepository.Listar();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error en el metodo.");
                lst = new List<UsuarioResult>();
            }

            return lst;
        }

        public List<UsuarioResult> Buscar( 
            string texto )
        {
            List<UsuarioResult> lst;

            try
            {
                lst = this.oRepository.Buscar( texto );
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el metodo.", ex);
            }

            return lst;
        }

        public List<UsuarioResult> Login(string codigo, string pwd)
        {
            List<UsuarioResult> lst;

            try
            {
                lst = this.oRepository.Login(codigo, pwd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el metodo.", ex);
            }

            return lst;
        }
    }
}
