using System;

namespace Api.Entities
{
    public class PedidoResult
    {
        public int id_pedido { get; set; }
        public string sku { get; set; }
        public string nombre { get; set; }
        public int precioventa { get; set; }
        public int cantidad { get; set; }
        public string estado { get; set; }
        public string usuario { get; set; }
        public DateTime fecha { get; set; }
    }
}
