using Software.Data;
using Software.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DBShananin.Controllers
{
    public enum SortState
    {
        IdAsc,
        IdDesc,
        NameAsc,
        NameDesc,
        TypeAsc,
        TypeDesc,
        VerAsc,
        VerDesc,
        DateAsc,
        DateDesc,
    }

    public class SoftwareController : Controller
    {
        private SoftwareContext _db;

        public SoftwareController(SoftwareContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string name, SortState order = SortState.IdAsc)
        {
            IQueryable<SoftwareProduct> products = _db.SoftwareProducts.Include(p => p.Type);

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.ProductName.Contains(name));
            }

            ViewData["IdSort"]   =  order == SortState.IdAsc    ? SortState.IdDesc   : SortState.IdAsc;
            ViewData["NameSort"] =  order == SortState.NameAsc  ? SortState.NameDesc : SortState.NameAsc;
            ViewData["TypeSort"] =  order == SortState.TypeAsc  ? SortState.TypeDesc : SortState.TypeAsc;
            ViewData["VerSort"]  =  order == SortState.VerAsc   ? SortState.VerDesc  : SortState.VerAsc;
            ViewData["DateSort"] =  order == SortState.DateAsc  ? SortState.DateDesc : SortState.DateAsc;

            products = order switch
            {
                SortState.IdDesc    => products.OrderByDescending(p => p.ProductId),
                SortState.NameAsc   => products.OrderBy(p => p.ProductName),
                SortState.NameDesc  => products.OrderByDescending(p => p.ProductName),
                SortState.TypeAsc   => products.OrderBy(p => p.Type.Name),
                SortState.TypeDesc  => products.OrderByDescending(p => p.Type.Name),
                SortState.VerAsc    => products.OrderBy(p => p.Version),
                SortState.VerDesc   => products.OrderByDescending(p => p.Version),
                SortState.DateAsc   => products.OrderBy(p => p.ReleaseDate),
                SortState.DateDesc  => products.OrderByDescending(p => p.ReleaseDate),
                _ => products.OrderBy(p => p.ProductId),
            };

            return View(await products.ToListAsync());
        }

        public IActionResult CreateProduct()
        {
            ViewBag.TypeId = new SelectList(_db.ProductTypes.Select(type => type.Name));
            ViewBag.ContractId = new SelectList(_db.Contracts.Select(contract => contract.ContractId));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(SoftwareProduct product)
        {
            _db.SoftwareProducts.Add(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null) return NotFound();

            ViewBag.TypeId = new SelectList(_db.ProductTypes.Select(type => type.Name));
            ViewBag.ContractId = new SelectList(_db.Contracts.Select(contract => contract.ContractId));
            var product = await _db.SoftwareProducts.SingleOrDefaultAsync(p => p.ProductId == id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(SoftwareProduct product)
        {
            _db.SoftwareProducts.Update(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null) return NotFound();

            var product = await _db.SoftwareProducts.SingleOrDefaultAsync(p => p.ProductId == id);
            _db.SoftwareProducts.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
