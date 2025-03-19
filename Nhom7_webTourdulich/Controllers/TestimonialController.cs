using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;
using Microsoft.EntityFrameworkCore;

namespace Nhom7_webTourdulich.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly QuanLyTourContext _context;

        public TestimonialController (QuanLyTourContext context)
        {
            _context = context;
        }

        // GET: DanhGia
        public IActionResult Index()
        {
            var danhGias = _context.DanhGias
        .Include(d => d.KhachHang) // Bao gồm thông tin khách hàng
        .ToList();
          return View(danhGias);
        }

        // GET: DanhGia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhGia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                _context.DanhGias.Add(danhGia);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(danhGia);
        }

        // GET: DanhGia/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var danhGia = _context.DanhGias.Find(id);
            if (danhGia == null) return NotFound();
            return View(danhGia);
        }

        // POST: DanhGia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                _context.DanhGias.Update(danhGia);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(danhGia);
        }

        // GET: DanhGia/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var danhGia = _context.DanhGias.Find(id);
            if (danhGia == null) return NotFound();
            return View(danhGia);
        }

        // POST: DanhGia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var danhGia = _context.DanhGias.Find(id);
            if (danhGia != null)
            {
                _context.DanhGias.Remove(danhGia);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
