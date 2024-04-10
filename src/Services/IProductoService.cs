using Api.Entities;
using System.Collections.Generic;

namespace Api.Services
{
    public interface IProductoService
    {
        List<ProductoResult> Listar();
        List<ProductoResult> Buscar( string texto );
    }

}
