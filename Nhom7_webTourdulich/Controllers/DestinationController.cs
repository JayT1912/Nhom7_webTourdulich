using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom7_webTourdulich.Models;

namespace Nhom7_webTourdulich.Controllers;

public class DestinationController : Controller
{
    private readonly QuanLyTourContext _quanLyTour;
    private readonly ILogger<DestinationController> _logger;

    public DestinationController(ILogger<DestinationController> logger, QuanLyTourContext quanLyTour)
    {
        _logger = logger;
        _quanLyTour = quanLyTour;
    }
  public IActionResult Index()
{  
    var tours = _quanLyTour.Tours
        .Include(t => t.TourImages)
        .Include(t => t.MaDiemDenNavigation)
        .Include(t => t.MaGiaTourNavigation)
        .Include(t => t.DanhGias)
        .AsQueryable();

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
