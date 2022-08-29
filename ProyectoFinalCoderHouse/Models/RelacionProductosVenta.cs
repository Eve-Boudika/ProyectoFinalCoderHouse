namespace ProyectoFinalCoderHouse.Models
{
    public class RelacionProductosVenta
    {
        public Venta Venta { get; set; }
        public List<Producto> ListaProducto { get; set; }
    }
}
