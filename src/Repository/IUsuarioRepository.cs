using Api.Entities;
using System.Collections.Generic;

namespace Api.Repository
{
    public interface IUsuarioRepository
    {
        List<UsuarioResult> Listar();
        List<UsuarioResult> Buscar( string texto);
        List<UsuarioResult> Login(string codigo, string pwd);
    }

}
