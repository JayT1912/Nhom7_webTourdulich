using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace Nhom7_webTourdulich.Controllers
{
    [Authorize(Roles = "Admin")]

    public class TourController : Controller
    {
        private readonly QuanLyTourContext _context; // Sử dụng trực tiếp DbContext
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TourController(QuanLyTourContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // Hiển thị danh sách tour
    [HttpGet]public async Task<IActionResult> Index(string searchQuery)
{
    // Lấy danh sách tour
    var toursQuery = _context.Tours
        .Include(t => t.MaLoaiTourNavigation)
        .Include(t => t.MaGiaTourNavigation)
        .Include(t => t.MaDiemDenNavigation)
        .AsQueryable(); // Bảo đảm IQueryable để hỗ trợ LINQ động

    // Nếu có từ khóa tìm kiếm, thực hiện lọc danh sách
    if (!string.IsNullOrEmpty(searchQuery))
    {
        // Chuyển thành chữ thường (tolower) để tìm kiếm không phân biệt hoa thường
        toursQuery = toursQuery.Where(t => t.Ten.ToLower().Contains(searchQuery.ToLower()));
    }

    // Thực thi truy vấn và lấy danh sách kết quả
    var tours = await toursQuery.ToListAsync();

    // Lấy danh sách hình ảnh theo từng tour
    var images = await _context.TourImages.ToListAsync();
    ViewBag.Images = images.GroupBy(img => img.MaTour).ToDictionary(g => g.Key, g => g.ToList());

    // Truyền lại từ khóa tìm kiếm để hiển thị trong giao diện
    ViewBag.SearchQuery = searchQuery;

    return View(tours);
}
[HttpGet]
public async Task<IActionResult> Create()
{
    // Fetch data for dropdowns
    ViewBag.MaLoaiTour = new SelectList(await _context.LoaiTours.ToListAsync(), "MaLoaiTour", "TenLoaiTour");
    ViewBag.MaGiaTour = new SelectList(await _context.GiaTours.ToListAsync(), "MaGiaTour", "Gia");
    ViewBag.MaDiemDen = new SelectList(await _context.DiemDens.ToListAsync(), "MaDiemDen", "Ten");

    return View();
}


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Tour model, List<IFormFile> images, DateTime NgayKhoiHanh, DateTime NgayKetThuc)
{
    if (!ModelState.IsValid)
    {
        // Reload dropdowns in case of validation errors
        ViewBag.MaLoaiTour = new SelectList(await _context.LoaiTours.ToListAsync(), "MaLoaiTour", "TenLoaiTour");
        ViewBag.MaGiaTour = new SelectList(await _context.GiaTours.ToListAsync(), "MaGiaTour", "Gia");
        ViewBag.MaDiemDen = new SelectList(await _context.DiemDens.ToListAsync(), "MaDiemDen", "Ten");
        return View(model);
    }

    // Check for duplicate MaTour
    var existingTour = await _context.Tours.FindAsync(model.MaTour);
    if (existingTour != null)
    {
        ModelState.AddModelError("MaTour", "Mã Tour đã tồn tại. Vui lòng nhập mã khác.");
        return View(model);
    }

    // Save the Tour entity
    _context.Tours.Add(model);
    await _context.SaveChangesAsync(); // Save to generate MaTour ID

    // Automatically create Nhóm Tour
    var groupTour = new NhomTour
{
    MaTour = model.MaTour, // Link to the new tour
    NgayKhoiHanh = DateOnly.FromDateTime(NgayKhoiHanh),
    NgayKetThuc = DateOnly.FromDateTime(NgayKetThuc),
    DiemXuatPhat = "Địa điểm mặc định", // Default departure point
    NoiDung = model.MoTa, // Use tour description
    MaTrangThai = 1 // Default status: 'Chờ xử lý'
};

    _context.NhomTours.Add(groupTour);

    // Handle image uploads
    if (images != null && images.Any())
    {
        foreach (var image in images)
        {
            if (image.Length > 0)
            {
                // Generate unique filename
                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                // Save image file to server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Create and add TourImage record
                var tourImage = new TourImage
                {
                    MaTour = model.MaTour,
                    HinhAnh = "/img/" + fileName // Save file path
                };
                _context.TourImages.Add(tourImage);
            }
        }
    }

    // Update Tour_Images table
    
    await _context.SaveChangesAsync();
    var query = "UPDATE Tour_Images SET TourMaTour = MaTour WHERE TourMaTour IS NULL";
    await _context.Database.ExecuteSqlRawAsync(query); // Execute the query
 // Commit all changes (Tour, Group Tour, Images, and Query)
    return RedirectToAction(nameof(Index)); // Redirect to tour list
}



        // Chỉnh sửa tour (Form Edit)
[HttpGet]
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    // Fetch the tour and include related data
    var tour = await _context.Tours
        .Include(t => t.TourImages) // Include images
        .Include(t => t.MaLoaiTourNavigation) // Include LoaiTour details
        .Include(t => t.MaGiaTourNavigation) // Include GiaTour details
        .Include(t => t.MaDiemDenNavigation) // Include DiemDen details
        .Include(t => t.NhomTours) // Include NhomTour data
        .FirstOrDefaultAsync(t => t.MaTour == id);

    if (tour == null)
    {
        return NotFound();
    }

    // Populate dropdowns for form fields
    await PopulateDropdownsAsync(tour);

    // Group images for display
    ViewBag.Images = await _context.TourImages.Where(img => img.MaTour == id).ToListAsync();

    // Pass NhomTour data to the view
    ViewBag.NhomTour = tour.NhomTours?.FirstOrDefault();

    return View(tour);
}

        // Lưu chỉnh sửa tour
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tour model, List<IFormFile> newImages)
        {
            if (id != model.MaTour)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingTour = await _context.Tours.FindAsync(id);
                if (existingTour == null)
                {
                    return NotFound();
                }

                // Cập nhật thông tin tour
                existingTour.Ten = model.Ten;
                existingTour.MaLoaiTour = model.MaLoaiTour;
                existingTour.MaGiaTour = model.MaGiaTour;
                existingTour.MaDiemDen = model.MaDiemDen;
                existingTour.SoNgay = model.SoNgay;
                existingTour.SoLuongNguoi = model.SoLuongNguoi;
                existingTour.MoTa = model.MoTa;

                _context.Tours.Update(existingTour);

                // Thêm hình ảnh mới nếu có
                if (newImages != null && newImages.Count > 0)
                {
                    foreach (var image in newImages)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        _context.TourImages.Add(new TourImage
                        {
                            MaTour = model.MaTour,
                            HinhAnh = "/img/" + fileName
                        });
                    }
                }

                await _context.SaveChangesAsync();
var query = "UPDATE Tour_Images SET TourMaTour = MaTour WHERE TourMaTour IS NULL";
        await _context.Database.ExecuteSqlRawAsync(query); // Thực thi truy vấn SQL
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
   // Helper method: Populate dropdown lists
        private async Task PopulateDropdownsAsync(Tour? tour = null)
        {
            ViewBag.MaLoaiTour = new SelectList(await _context.LoaiTours.ToListAsync(), "MaLoaiTour", "TenLoaiTour", tour?.MaLoaiTour);
            ViewBag.MaGiaTour = new SelectList(await _context.GiaTours.ToListAsync(), "MaGiaTour", "Gia", tour?.MaGiaTour);
            ViewBag.MaDiemDen = new SelectList(await _context.DiemDens.ToListAsync(), "MaDiemDen", "Ten", tour?.MaDiemDen);
        }
[HttpPost]
public async Task<IActionResult> Delete(int id)
{
    // Find the tour and include related data
    var tour = await _context.Tours
        .Include(t => t.TourImages)  // Include related images
        .Include(t => t.NhomTours)  // Include related NhomTours
        .FirstOrDefaultAsync(t => t.MaTour == id);

    if (tour == null)
        return NotFound();

    try
    {
        // Delete related images from the file system
        foreach (var image in tour.TourImages)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, image.HinhAnh.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        // Remove related images from the database
        _context.TourImages.RemoveRange(tour.TourImages);

        // Remove related NhomTour records
        _context.NhomTours.RemoveRange(tour.NhomTours);

        // Remove the tour itself
        _context.Tours.Remove(tour);

        // Save changes to the database
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index)); // Return to the tour list
    }
    catch (Exception ex)
    {
        ModelState.AddModelError("", $"Đã xảy ra lỗi: {ex.Message}");
        return RedirectToAction(nameof(Index));
    }
}



        // Xóa hình ảnh riêng lẻ
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.TourImages.FindAsync(id);
            if (image != null)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, image.HinhAnh.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _context.TourImages.Remove(image);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit", new { id = image.MaTour });
        }
    }
}
