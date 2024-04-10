using Api.Entities;
using System.Collections.Generic;

namespace Api.Repository
{
    public interface IPedidoRepository
    {
        List<PedidoResult> Listar();
        List<PedidoResult> Buscar(
            string texto);
        SimpleResult AgregarProducto(
            int id_pedido,
            string sku,
            int precioventa,
            int cantidad);
        SimpleResult Crear(
            string codigo_usuario);
        SimpleResult Cancelar(
            int id_pedido);
        SimpleResult Recuperar(
            int id_pedido);
        SimpleResult Atender(
            int id_pedido,
            string codigo_usuario);
    }

}
