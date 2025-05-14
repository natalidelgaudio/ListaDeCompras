namespace ListaDeCompras.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public bool Comprado { get; set; }

        public int ListaDeCompraId { get; set; }
        public ListaDeCompra ListaDeCompra { get; set; }
    }
}
