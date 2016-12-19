using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;

namespace Web.Controllers
{
    public class AttributeTypesController : Controller
    {
        private readonly DatabaseContext _context;

        public AttributeTypesController(DatabaseContext context)
        {
            _context = context;    
        }

        // GET: AttributeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AttributeTypes.ToListAsync());
        }

        // GET: AttributeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributeType = await _context.AttributeTypes
                .SingleOrDefaultAsync(m => m.AttributeTypeId == id);
            if (attributeType == null)
            {
                return NotFound();
            }

            return View(attributeType);
        }

        // GET: AttributeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AttributeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttributeTypeId,Name")] AttributeType attributeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attributeType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(attributeType);
        }

        // GET: AttributeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributeType = await _context.AttributeTypes.SingleOrDefaultAsync(m => m.AttributeTypeId == id);
            if (attributeType == null)
            {
                return NotFound();
            }
            return View(attributeType);
        }

        // POST: AttributeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttributeTypeId,Name")] AttributeType attributeType)
        {
            if (id != attributeType.AttributeTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attributeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeTypeExists(attributeType.AttributeTypeId))
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
            return View(attributeType);
        }

        // GET: AttributeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributeType = await _context.AttributeTypes
                .SingleOrDefaultAsync(m => m.AttributeTypeId == id);
            if (attributeType == null)
            {
                return NotFound();
            }

            return View(attributeType);
        }

        // POST: AttributeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attributeType = await _context.AttributeTypes.SingleOrDefaultAsync(m => m.AttributeTypeId == id);
            _context.AttributeTypes.Remove(attributeType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AttributeTypeExists(int id)
        {
            return _context.AttributeTypes.Any(e => e.AttributeTypeId == id);
        }
    }
}
