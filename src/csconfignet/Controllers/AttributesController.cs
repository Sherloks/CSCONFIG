using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AttributesController : Controller
    {
        private readonly DatabaseContext _context;

        public AttributesController(DatabaseContext context)
        {
            _context = context;    
        }

        // GET: Attributes
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Attributes.Include(a => a.AttributeCategory).Include(a => a.AttributeType);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Attributes/Create
        public IActionResult Create()
        {
            ViewData["AttributeCategoryId"] = new SelectList(_context.AttributeCategories, "ID", "Name");
            ViewData["AttributeTypeId"] = new SelectList(_context.AttributeTypes, "AttributeTypeId", "Name");
            return View();
        }

        // POST: Attributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttributeId,Name,CVar,Tooltip,View,ViewConfig,AttributeTypeId,AttributeCategoryId")] Data.Models.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attribute);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AttributeCategoryId"] = new SelectList(_context.AttributeCategories, "ID", "Name", attribute.AttributeCategoryId);
            ViewData["AttributeTypeId"] = new SelectList(_context.AttributeTypes, "AttributeTypeId", "Name", attribute.AttributeTypeId);
            return View(attribute);
        }

        // GET: Attributes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes.SingleOrDefaultAsync(m => m.AttributeId == id);
            if (attribute == null)
            {
                return NotFound();
            }
            ViewData["AttributeCategoryId"] = new SelectList(_context.AttributeCategories, "ID", "Name", attribute.AttributeCategoryId);
            ViewData["AttributeTypeId"] = new SelectList(_context.AttributeTypes, "AttributeTypeId", "Name", attribute.AttributeTypeId);
            return View(attribute);
        }

        // POST: Attributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttributeId,Name,CVar,Tooltip,View,ViewConfig,AttributeTypeId,AttributeCategoryId")] Data.Models.Attribute attribute)
        {
            if (id != attribute.AttributeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeExists(attribute.AttributeId))
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
            ViewData["AttributeCategoryId"] = new SelectList(_context.AttributeCategories, "ID", "ID", attribute.AttributeCategoryId);
            ViewData["AttributeTypeId"] = new SelectList(_context.AttributeTypes, "AttributeTypeId", "AttributeTypeId", attribute.AttributeTypeId);
            return View(attribute);
        }

        // GET: Attributes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes
                .Include(a => a.AttributeCategory)
                .Include(a => a.AttributeType)
                .SingleOrDefaultAsync(m => m.AttributeId == id);
            if (attribute == null)
            {
                return NotFound();
            }

            return View(attribute);
        }

        // POST: Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attribute = await _context.Attributes.SingleOrDefaultAsync(m => m.AttributeId == id);
            _context.Attributes.Remove(attribute);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AttributeExists(int id)
        {
            return _context.Attributes.Any(e => e.AttributeId == id);
        }
    }
}
