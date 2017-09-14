using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EntityFrameworkCore;
using Domain;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomFieldController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomFieldController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CustomField
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomFields.ToListAsync());
        }

        // GET: Admin/CustomField/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customField = await _context.CustomFields
                .SingleOrDefaultAsync(m => m.CustomFieldId == id);
            if (customField == null)
            {
                return NotFound();
            }

            return View(customField);
        }

        // GET: Admin/CustomField/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CustomField/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomFieldId,FieldName,FieldType,PossibleValues,MinLength,MaxLength,IsRequired")] CustomField customField)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customField);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customField);
        }

        // GET: Admin/CustomField/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customField = await _context.CustomFields.SingleOrDefaultAsync(m => m.CustomFieldId == id);
            if (customField == null)
            {
                return NotFound();
            }
            return View(customField);
        }

        // POST: Admin/CustomField/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomFieldId,FieldName,FieldType,PossibleValues,MinLength,MaxLength,IsRequired")] CustomField customField)
        {
            if (id != customField.CustomFieldId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customField);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomFieldExists(customField.CustomFieldId))
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
            return View(customField);
        }

        // GET: Admin/CustomField/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customField = await _context.CustomFields
                .SingleOrDefaultAsync(m => m.CustomFieldId == id);
            if (customField == null)
            {
                return NotFound();
            }

            return View(customField);
        }

        // POST: Admin/CustomField/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customField = await _context.CustomFields.SingleOrDefaultAsync(m => m.CustomFieldId == id);
            _context.CustomFields.Remove(customField);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomFieldExists(int id)
        {
            return _context.CustomFields.Any(e => e.CustomFieldId == id);
        }
    }
}
