using System;

namespace Api.Entities
{
    public class ProductoResult
    {
        public string sku { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public string etiquetas { get; set; }
        public int precio { get; set; }
        public string medida { get; set; }
    }
}
