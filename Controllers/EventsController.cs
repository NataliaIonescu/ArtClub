using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtClub.Models;
using Microsoft.AspNetCore.Authorization;

namespace ArtClub.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Events.Include(v => v.Resource);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(v => v.Resource)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }
        [Authorize (Roles ="Member")]
        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("StartDate,EndDate,Description,ResourceId,Id")] Event @event)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(@event);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Id", @event.Id);
        //    return View(@event);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartDate,EndDate,Description,ResourceId,Id")] Event @event)
        {
            if (ModelState.IsValid)
            {
                // check if an event with the same properties already exists
                if (_context.Events.Any(e => e.StartDate == @event.StartDate && e.EndDate == @event.EndDate && e.ResourceId == @event.ResourceId))
                {
                    ModelState.AddModelError("", "An event with the same StartDate, EndDate and ResourceId already exists.");
                }
                else
                {
                    _context.Add(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", @event.Id);
            return View(@event);
        }


        [Authorize(Roles = "Admin, Member")]
        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", @event.Id);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("StartDate,EndDate,Description,ResourceId,Id")] Event @event)
        //{
        //    if (id != @event.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(@event);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EventExists(@event.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Id", @event.ResourceId);
        //    return View(@event);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StartDate,EndDate,Description,ResourceId,Id")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // check if an event with the same properties already exists
                if (_context.Events.Any(e => e.StartDate == @event.StartDate && e.EndDate == @event.EndDate && e.ResourceId == @event.ResourceId && e.Id != @event.Id))
                {
                    ModelState.AddModelError("", "An event with the same StartDate, EndDate and ResourceId already exists.");
                }
                else
                {
                    try
                    {
                        _context.Update(@event);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EventExists(@event.Id))
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
            }
            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", @event.ResourceId);
            return View(@event);
        }
        [Authorize(Roles = "Admin, Member")]
        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(v => v.Resource)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'AppDbContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
          return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
