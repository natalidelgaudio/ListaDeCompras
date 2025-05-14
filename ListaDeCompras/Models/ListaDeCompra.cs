using System.Collections.Generic;

namespace ListaDeCompras.Models
{
    public class ListaDeCompra
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public List<Item> Itens { get; set; } = new List<Item>();
    }
}
