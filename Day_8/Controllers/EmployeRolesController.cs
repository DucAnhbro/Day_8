using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day_8.Models;

namespace Day_8.Controllers
{
    public class EmployeRolesController : Controller
    {
        private readonly EmployessContext _context;

        public EmployeRolesController(EmployessContext context)
        {
            _context = context;
        }

        // GET: EmployeRoles
        public async Task<IActionResult> Index()
        {
              return View(await _context.EmployeRoles.ToListAsync());
        }

        // GET: EmployeRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeRoles == null)
            {
                return NotFound();
            }

            var employeRole = await _context.EmployeRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeRole == null)
            {
                return NotFound();
            }

            return View(employeRole);
        }

        // GET: EmployeRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeId,RoleId")] EmployeRole employeRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeRole);
        }

        // GET: EmployeRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeRoles == null)
            {
                return NotFound();
            }

            var employeRole = await _context.EmployeRoles.FindAsync(id);
            if (employeRole == null)
            {
                return NotFound();
            }
            return View(employeRole);
        }

        // POST: EmployeRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeId,RoleId")] EmployeRole employeRole)
        {
            if (id != employeRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeRoleExists(employeRole.Id))
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
            return View(employeRole);
        }

        // GET: EmployeRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeRoles == null)
            {
                return NotFound();
            }

            var employeRole = await _context.EmployeRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeRole == null)
            {
                return NotFound();
            }

            return View(employeRole);
        }

        // POST: EmployeRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeRoles == null)
            {
                return Problem("Entity set 'EmployessContext.EmployeRoles'  is null.");
            }
            var employeRole = await _context.EmployeRoles.FindAsync(id);
            if (employeRole != null)
            {
                _context.EmployeRoles.Remove(employeRole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeRoleExists(int id)
        {
          return _context.EmployeRoles.Any(e => e.Id == id);
        }
    }
}
