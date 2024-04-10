using Api.Entities;
using System.Collections.Generic;

namespace Api.Services
{
    public interface IUsuarioService
    {
        List<UsuarioResult> Listar();
        List<UsuarioResult> Buscar(string texto);
        List<UsuarioResult> Login(string codigo, string pwd);
    }

}