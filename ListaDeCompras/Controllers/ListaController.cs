using ListaDeCompras.Data;
using ListaDeCompras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ListaDeCompras.Controllers
{
    public class ListaController : Controller
    {
        private readonly AppDbContext _context;

        public ListaController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var listas = await _context.ListasDeCompra.Include(l => l.Itens).ToListAsync();
            return View(listas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ListaDeCompra lista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int listaId, string nome, int quantidade)
        {
            var lista = await _context.ListasDeCompra.Include(l => l.Itens).FirstOrDefaultAsync(l => l.Id == listaId);
            if (lista != null)
            {
                lista.Itens.Add(new Item { Nome = nome, Quantidade = quantidade });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> MarcarComoComprado(int itemId)
        {
            var item = await _context.Itens.FindAsync(itemId);
            if (item != null)
            {
                item.Comprado = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var lista = await _context.ListasDeCompra.FindAsync(id);
            if (lista == null) return NotFound();
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ListaDeCompra lista)
        {
            if (!ModelState.IsValid) return View(lista);

            _context.Update(lista);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Excluir Lista
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var lista = await _context.ListasDeCompra
                .Include(l => l.Itens)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lista == null) return NotFound();

            _context.Itens.RemoveRange(lista.Itens);
            _context.ListasDeCompra.Remove(lista);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Editar Item
        public async Task<IActionResult> EditItem(int id)
        {
            var item = await _context.Itens.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(Item item)
        {
            if (!ModelState.IsValid) return View(item);

            _context.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Excluir Item
        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Itens.FindAsync(id);
            if (item == null) return NotFound();

            _context.Itens.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
