using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom7_webTourdulich.Models;
using System.Linq;
namespace Nhom7_webTourdulich.Controllers;

public class TeamController : Controller
{
    private readonly QuanLyTourContext _quanLyTour;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public TeamController(QuanLyTourContext quanLyTour, IWebHostEnvironment webHostEnvironment)
    {
        _quanLyTour = quanLyTour;
        _webHostEnvironment = webHostEnvironment;
    }

 public async Task<IActionResult> Index(string searchQuery, string searchPosition, int pageNumber = 1, int pageSize = 8)
{
    // Base query to fetch employees
    var nhanViensQuery = _quanLyTour.NhanViens
        .Include(nv => nv.MaChucVuNavigation) // Include navigation property for position
        .AsQueryable();

    // Apply search by name
    if (!string.IsNullOrEmpty(searchQuery))
    {
        nhanViensQuery = nhanViensQuery.Where(nv => nv.Ten.ToLower().Contains(searchQuery.ToLower()));
    }

    // Apply search by position
    if (!string.IsNullOrEmpty(searchPosition))
    {
        nhanViensQuery = nhanViensQuery.Where(nv => nv.MaChucVuNavigation.TenChucVu.ToLower().Contains(searchPosition.ToLower()));
    }

    // Pagination logic
    var totalRecords = await nhanViensQuery.CountAsync(); // Get total record count
    var nhanViens = await nhanViensQuery
        .Skip((pageNumber - 1) * pageSize) // Skip previous pages
        .Take(pageSize) // Take current page items
        .ToListAsync();

    // ViewBag data for rendering pagination
    ViewBag.TotalRecords = totalRecords;
    ViewBag.PageNumber = pageNumber;
    ViewBag.PageSize = pageSize;
    ViewBag.SearchQuery = searchQuery;
    ViewBag.SearchPosition = searchPosition;

    return View(nhanViens);
}
// GET: Admin Index - List all employees
public async Task<IActionResult> IndexAdmin()
{
    // Fetch employees along with their roles
    var nhanViens = await _quanLyTour.NhanViens
                        .Include(nv => nv.MaChucVuNavigation) // Load navigation property
                        .ToListAsync();

    // Pass the employee list to the Razor view
    return View(nhanViens);
}


// GET: Create Employee Form
public IActionResult Create()
{
    ViewBag.ChucVus = _quanLyTour.ChucVus.ToList(); // Load available roles
    return View();
}

// POST: Create Employee
[HttpPost]
public async Task<IActionResult> Create(NhanVien model, IFormFile? file)
{
    if (ModelState.IsValid)
    {
        if (file != null && file.Length > 0)
        {
            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            model.HinhAnh = "/img/" + uniqueFileName;
        }

        _quanLyTour.NhanViens.Add(model);
        await _quanLyTour.SaveChangesAsync();
        return RedirectToAction("IndexAdmin");
    }

    ViewBag.ChucVus = _quanLyTour.ChucVus.ToList();
    return View(model);
}

// GET: Edit Employee Form
public async Task<IActionResult> Edit(int id)
{
    var nhanVien = await _quanLyTour.NhanViens.FindAsync(id);
    if (nhanVien == null)
    {
        return NotFound();
    }

    ViewBag.ChucVus = _quanLyTour.ChucVus.ToList();
    return View(nhanVien);
}

// POST: Edit Employee
[HttpPost]
public async Task<IActionResult> Edit(int id, NhanVien model, IFormFile? file)
{
    if (ModelState.IsValid)
    {
        // Fetch the existing employee record
        var nhanVien = await _quanLyTour.NhanViens.FindAsync(id);
        if (nhanVien == null)
        {
            return NotFound(); // Return 404 if employee not found
        }

        // Handle image update
        if (file != null && file.Length > 0)
        {
            // Remove the old image if it exists
            if (!string.IsNullOrEmpty(nhanVien.HinhAnh))
            {
                string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, nhanVien.HinhAnh.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath); // Delete the old image
                }
            }

            // Save the new image
            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            nhanVien.HinhAnh = "/img/" + uniqueFileName;
        }

        // Update other fields
        nhanVien.Ten = model.Ten;
        nhanVien.MaChucVu = model.MaChucVu;
        nhanVien.LinkFB = model.LinkFB;
        nhanVien.LinkZL = model.LinkZL;
        nhanVien.LinkIG = model.LinkIG;

        await _quanLyTour.SaveChangesAsync(); // Save changes to the database
        return RedirectToAction("IndexAdmin"); // Redirect to the admin page
    }

    // If the ModelState is invalid, reload roles and return the form
    ViewBag.ChucVus = _quanLyTour.ChucVus.ToList();
    return View(model);
}


// GET: Delete Confirmation
public async Task<IActionResult> Delete(int id)
{
    var nhanVien = await _quanLyTour.NhanViens.FindAsync(id);
    if (nhanVien == null)
    {
        return NotFound();
    }

    return View(nhanVien);
}

[HttpPost]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var nhanVien = await _quanLyTour.NhanViens.FirstOrDefaultAsync(nv => nv.MaNhanVien == id);
    if (nhanVien != null)
    {
        if (!string.IsNullOrEmpty(nhanVien.HinhAnh))
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, nhanVien.HinhAnh.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath); // Delete the file
            }
        }

        _quanLyTour.NhanViens.Remove(nhanVien);
        await _quanLyTour.SaveChangesAsync();
    }

    return RedirectToAction("IndexAdmin"); // Redirect to the index page after deletion
}



}
