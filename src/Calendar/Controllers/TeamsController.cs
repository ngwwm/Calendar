using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calendar.Data;
using Calendar.Models;
using Calendar.Helpers;

namespace Calendar.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        public IEnumerable<Team> ListAll()
        {
            return _context.Team.AsEnumerable();
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            return View(await _context.Team.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (!User.IsInRole(Constants.ROLE_ADMIN))
            //    return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team.SingleOrDefaultAsync(m => m.ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CalendarStyle,Description,Name,DomainGroup")] Team team)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team.SingleOrDefaultAsync(m => m.ID == id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CalendarStyle,Description,Name,DomainGroup")] Team team)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id != team.ID)
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
                    if (!TeamExists(team.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team.SingleOrDefaultAsync(m => m.ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            var team = await _context.Team.SingleOrDefaultAsync(m => m.ID == id);
            _context.Team.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.ID == id);
        }
    }
}
