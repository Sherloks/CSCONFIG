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
    public class AttributeCategoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public AttributeCategoriesController(DatabaseContext context)
        {
            _context = context;    
        }

        // GET: AttributeCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.AttributeCategories.ToListAsync());
        }

        // GET: AttributeCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AttributeCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description")] AttributeCategory attributeCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attributeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(attributeCategory);
        }

        // GET: AttributeCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributeCategory = await _context.AttributeCategories.SingleOrDefaultAsync(m => m.ID == id);
            if (attributeCategory == null)
            {
                return NotFound();
            }
            return View(attributeCategory);
        }

        // POST: AttributeCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description")] AttributeCategory attributeCategory)
        {
            if (id != attributeCategory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attributeCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeCategoryExists(attributeCategory.ID))
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
            return View(attributeCategory);
        }

        // GET: AttributeCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributeCategory = await _context.AttributeCategories
                .SingleOrDefaultAsync(m => m.ID == id);
            if (attributeCategory == null)
            {
                return NotFound();
            }

            return View(attributeCategory);
        }

        // POST: AttributeCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attributeCategory = await _context.AttributeCategories.SingleOrDefaultAsync(m => m.ID == id);
            _context.AttributeCategories.Remove(attributeCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AttributeCategoryExists(int id)
        {
            return _context.AttributeCategories.Any(e => e.ID == id);
        }
    }
}
