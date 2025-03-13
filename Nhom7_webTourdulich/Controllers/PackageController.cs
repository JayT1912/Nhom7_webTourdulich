using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;
using Microsoft.EntityFrameworkCore;
namespace Nhom7_webTourdulich.Controllers;

public class PackageController : Controller
{
    private readonly QuanLyTourContext _quanLyTour;
    private readonly ILogger<PackageController> _logger;

    public PackageController(ILogger<PackageController> logger, QuanLyTourContext quanLyTour)
    {
        _logger = logger;
        _quanLyTour = quanLyTour;
    }

    public IActionResult Index()
    {
        
        var tours = _quanLyTour.Tours
            .Include(t => t.MaGiaTourNavigation)
            .Include(t => t.MaLoaiTourNavigation)
            .Include(t => t.MaDiemDenNavigation) 
            .ToList();

        return View(tours); 
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
