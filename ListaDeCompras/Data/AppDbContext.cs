using Microsoft.EntityFrameworkCore;
using ListaDeCompras.Models;
using System.Collections.Generic;

namespace ListaDeCompras.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<ListaDeCompra> ListasDeCompra { get; set; }
        public DbSet<Item> Itens { get; set; }
    }
}
