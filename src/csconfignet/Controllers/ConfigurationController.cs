using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models.ConfigurationViewModels;
using Data;
using Microsoft.Extensions.Logging;
using Web.Controllers;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin, NormalUser")]
    public class ConfigurationController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> userManager;
        private readonly ILogger logger;

        public ConfigurationController(DatabaseContext context, ILoggerFactory loggerFactory, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this._context = context;
            this.logger = loggerFactory.CreateLogger<ConfigurationController>();
        }

        public async Task<IActionResult> Index()
        {
            var userid = await userManager.GetUserAsync(HttpContext.User);
            return View(await _context.Configurations.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            Configuration viewModel = new Configuration();
            List<Data.Models.Attribute> attributes = await _context.Attributes.Include(a => a.AttributeType).ToListAsync();

            viewModel.ConfigurationAttributes = new List<Data.Models.ConfigurationAttribute>();

            foreach (var item in attributes)
            {
                viewModel.ConfigurationAttributes.Add(new Data.Models.ConfigurationAttribute()
                {
                    Attribute = item                    
                });
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Configuration configuration)
        {
            //Configuration configuration = new Configuration();

            User user = await userManager.GetUserAsync(HttpContext.User);

            configuration.User = user;
            configuration.DateCreated = DateTime.Now;
            configuration.LastModified = DateTime.Now;

            if(ModelState.IsValid)
            {
                _context.Configurations.Add(configuration);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(configuration);
        }

        // GET: Configurations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = await _context.Configurations.Include("ConfigurationAttributes.Attribute.AttributeType")
                .Include("ConfigurationAttributes.Attribute.AttributeCategory")
                                        .SingleOrDefaultAsync(m => m.ConfigurationId == id);
            if (configuration == null)
            {
                return NotFound();
            }
            return View(configuration);
        }

        // POST: Configurations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Configuration configuration)
        {
            if (id != configuration.ConfigurationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigurationExists(configuration.ConfigurationId))
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
            return View(configuration);
        }

        // GET: Configurations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = await _context.Configurations.Include("ConfigurationAttributes.Attribute.AttributeType")
                .Include("ConfigurationAttributes.Attribute.AttributeCategory")
                                        .SingleOrDefaultAsync(m => m.ConfigurationId == id);
            if (configuration == null)
            {
                return NotFound();
            }

            return View(configuration);
        }

        // POST: Configurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configuration = await _context.Configurations.SingleOrDefaultAsync(m => m.ConfigurationId == id);
            _context.Configurations.Remove(configuration);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ConfigurationExists(int id)
        {
            return _context.Configurations.Any(e => e.ConfigurationId == id);
        }
    }
}
