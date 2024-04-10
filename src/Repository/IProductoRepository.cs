using Api.Entities;
using System.Collections.Generic;

namespace Api.Repository
{
    public interface IProductoRepository
    {
        List<ProductoResult> Listar();
        List<ProductoResult> Buscar( string texto);
    }

}
