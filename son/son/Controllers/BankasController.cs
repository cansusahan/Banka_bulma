using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using son.Models;

namespace son.Controllers
{
    public class BankasController : Controller
    {
        private readonly ListeContext _context;

        public BankasController()
        {
            _context = new ListeContext();
        }
        // filtreleyen metod
        public IActionResult Search(string searchTerm)
        {
            // Eğer arama terimi boş ise, tüm liste öğelerini göstermek için tüm veriyi al
            if (string.IsNullOrEmpty(searchTerm))
            {
                var allData = _context.Bankas.ToList();
                return View(allData);
            }

            // Veritabanından verileri çekerken item.Bin null bir int olduğundan ToString() metodu kullanılmıyor.
            // Bu nedenle değeri null olmayanları filtrelemek için GetValueOrDefault() metodunu kulladım.
            var filteredData = _context.Bankas
                .Where(item => item.Bin.GetValueOrDefault().ToString().Contains(searchTerm))
                .ToList();


            return View(filteredData);
        }
        //ajaxta kullanma ve baska sayfaya girmemesi icin yaptım yani bulunca sayfayı yenilemesin sadece listeyi yenilesin
        public JsonResult SearchFilter(string searchTerm)
        {
            // Eğer arama terimi boş ise, tüm liste öğelerini göstermek için tüm veriyi al
            if (string.IsNullOrEmpty(searchTerm))
            {
                var allData = _context.Bankas.ToList();
                return Json(allData);
            }

            // Veritabanından verileri çekerken item.Bin null bir int olduğundan ToString() metodu kullanılmıyor.
            // Bu nedenle değeri null olmayanları filtrelemek için GetValueOrDefault() metodunu kulladım.
            var filteredData = _context.Bankas
                .Where(item => item.Bin.GetValueOrDefault().ToString().Contains(searchTerm))
                .ToList();


            return Json(filteredData);
        }

        // GET: Bankas
        public async Task<IActionResult> Index()
        {
              return _context.Bankas != null ? 
                          View(await _context.Bankas.ToListAsync()) :
                          Problem("Entity set 'ListeContext.Bankas'  is null.");
        }

        // GET: Bankas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bankas == null)
            {
                return NotFound();
            }

            var banka = await _context.Bankas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banka == null)
            {
                return NotFound();
            }

            return View(banka);
        }

        // GET: Bankas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bankas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Bin,BankaKodu,BankaAdi,Tip,AltTip,Taksit")] Banka banka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(banka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banka);
        }

        // GET: Bankas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bankas == null)
            {
                return NotFound();
            }

            var banka = await _context.Bankas.FindAsync(id);
            if (banka == null)
            {
                return NotFound();
            }
            return View(banka);
        }

        // POST: Bankas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Bin,BankaKodu,BankaAdi,Tip,AltTip,Taksit")] Banka banka)
        {
            if (id != banka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(banka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankaExists(banka.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(banka);
        }

        // GET: Bankas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bankas == null)
            {
                return NotFound();
            }

            var banka = await _context.Bankas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banka == null)
            {
                return NotFound();
            }

            return View(banka);
        }

        // POST: Bankas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bankas == null)
            {
                return Problem("Entity set 'ListeContext.Bankas'  is null.");
            }
            var banka = await _context.Bankas.FindAsync(id);
            if (banka != null)
            {
                _context.Bankas.Remove(banka);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankaExists(int id)
        {
          return (_context.Bankas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
