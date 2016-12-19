using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace csconfignet.Controllers
{
    public class SettingsController : Controller
    {
        private readonly DatabaseContext _context;

        public SettingsController(DatabaseContext context)
        {
            _context = context;    
        }

        // GET: Settings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings.ToListAsync());
        }

        // GET: Settings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .SingleOrDefaultAsync(m => m.ID == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // GET: Settings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,GameDirectory,PreferedLanguage,PreferedTheme")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(setting);
        }

        // GET: Settings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings.SingleOrDefaultAsync(m => m.ID == id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }

        // POST: Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,GameDirectory,PreferedLanguage,PreferedTheme")] Setting setting)
        {
            if (id != setting.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.ID))
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
            return View(setting);
        }

        // GET: Settings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .SingleOrDefaultAsync(m => m.ID == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setting = await _context.Settings.SingleOrDefaultAsync(m => m.ID == id);
            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SettingExists(int id)
        {
            return _context.Settings.Any(e => e.ID == id);
        }
    }
}
