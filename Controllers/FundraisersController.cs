using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CS451R_Fundraiser.Data;
using CS451R_Fundraiser.Models;

namespace CS451R_Fundraiser.Controllers
{
    public class FundraisersController : Controller
    {
        private readonly CS451R_FundraiserContext _context;

        public FundraisersController(CS451R_FundraiserContext context)
        {
            _context = context;
        }

        // GET: Fundraisers
        public async Task<IActionResult> Index(string fundraiserCategory, string searchString)
        {
            if (_context.Fundraiser == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> categoryQuery = from m in _context.Fundraiser
                                            orderby m.Category
                                            select m.Category;
            var fundraisers = from m in _context.Fundraiser
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                fundraisers = fundraisers.Where(s => s.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(fundraiserCategory))
            {
                fundraisers = fundraisers.Where(x => x.Category == fundraiserCategory);
            }

            var fundraiserCategoryVM = new FundraiserCategoryViewModel
            {
                Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                Fundraisers = await fundraisers.ToListAsync()
            };

            return View(fundraiserCategoryVM);
        }

        // GET: Fundraisers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fundraiser == null)
            {
                return NotFound();
            }

            var fundraiser = await _context.Fundraiser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fundraiser == null)
            {
                return NotFound();
            }

            return View(fundraiser);
        }

        // GET: Fundraisers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fundraisers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,PostDate,Category,Goal")] Fundraiser fundraiser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fundraiser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fundraiser);
        }

        // GET: Fundraisers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fundraiser == null)
            {
                return NotFound();
            }

            var fundraiser = await _context.Fundraiser.FindAsync(id);
            if (fundraiser == null)
            {
                return NotFound();
            }
            return View(fundraiser);
        }

        // POST: Fundraisers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,PostDate,Category,Goal")] Fundraiser fundraiser)
        {
            if (id != fundraiser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fundraiser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FundraiserExists(fundraiser.Id))
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
            return View(fundraiser);
        }

        // GET: Fundraisers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fundraiser == null)
            {
                return NotFound();
            }

            var fundraiser = await _context.Fundraiser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fundraiser == null)
            {
                return NotFound();
            }

            return View(fundraiser);
        }

        // POST: Fundraisers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fundraiser == null)
            {
                return Problem("Entity set 'CS451R_FundraiserContext.Fundraiser'  is null.");
            }
            var fundraiser = await _context.Fundraiser.FindAsync(id);
            if (fundraiser != null)
            {
                _context.Fundraiser.Remove(fundraiser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FundraiserExists(int id)
        {
          return (_context.Fundraiser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
