﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StatusController : Controller
    {
        private readonly IRepository<Status> _statusRepository;

        public StatusController(IRepository<Status> statusRepository)
        {   
            _statusRepository = statusRepository;
        }

        // GET: Admin/Status
        public async Task<IActionResult> Index()
        {
            return View(await _statusRepository.AllAsync());
        }

        // GET: Admin/Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _statusRepository.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Admin/Status/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Status status)
        {
            if (!ModelState.IsValid) return View(status);

            _statusRepository.Add(status);
            await _statusRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _statusRepository.FindAsync(id.Value);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: Admin/Status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Status status)
        {
            if (id != status.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(status);
            try
            {
                _statusRepository.Update(status);
                await _statusRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_statusRepository.Exists(status.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _statusRepository.FindAsync(id.Value);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Admin/Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var status = await _statusRepository.FindAsync(id);
            _statusRepository.Remove(status);
            await _statusRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
