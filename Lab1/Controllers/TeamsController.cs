using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lab1.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Managers,Players")]
        // GET: Teams
        public async Task<IActionResult> Index()
        {
              return View(await _context.Team.ToListAsync());
        }

        [Authorize(Roles = "Managers,Players")]
        // GET: Teams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        [Authorize(Roles = "Managers")]
        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamName,Email,CreationTime,EstablishedDate")] Team team)
        {
            if (ModelState.IsValid)
            {
                team.Id = Guid.NewGuid();
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        [Authorize(Roles = "Managers")]
        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Managers")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TeamName,Email,CreationTime,EstablishedDate")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            return View(team);
        }

        [Authorize(Roles = "Managers")]
        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        [Authorize(Roles = "Managers")]
        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Team == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Team'  is null.");
            }
            var team = await _context.Team.FindAsync(id);
            if (team != null)
            {
                _context.Team.Remove(team);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(Guid id)
        {
          return _context.Team.Any(e => e.Id == id);
        }
    }
}
