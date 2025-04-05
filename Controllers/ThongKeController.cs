using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using Nhom7_webTourdulich.Models;
using Microsoft.AspNetCore.Authorization;

namespace Nhom7_webTourdulich
{[Authorize(Roles = "Admin")]

    public class ThongKeController : Controller
    {
        private readonly QuanLyTourContext _context;

        public ThongKeController(QuanLyTourContext context)
        {
            _context = context;
        }

public IActionResult Statistics(string? timeType, int? day, int? month, int? year)
{
    try
    {ViewData["TimeType"] = timeType;
    ViewData["Day"] = day;
    ViewData["Month"] = month;
    ViewData["Year"] = year;
        // Base query: Fetch all HoaDon, KhachHang, and Tour data
        var query = from hd in _context.HoaDons
                    join kh in _context.KhachHangs on hd.MaKhachHang equals kh.MaKhachHang
                    join t in _context.Tours on kh.MaTour equals t.MaTour into TourGroup
                    from tour in TourGroup.DefaultIfEmpty() // Handle NULL values for Tour
                    select new
                    {
                        hd.NgayLap,
                        TenKhachHang = kh.Ten,
                        TenTour = tour != null ? tour.Ten : "Chưa xác định",
                        TongTien = hd.TongTien
                    };

        // Check if filters are applied
        if (!string.IsNullOrEmpty(timeType))
        {
            if (timeType == "day" && (!day.HasValue || !month.HasValue || !year.HasValue))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập đầy đủ thông tin ngày, tháng và năm để lọc.";
            }
            else if (timeType == "month" && (!month.HasValue || !year.HasValue))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập đầy đủ thông tin tháng và năm để lọc.";
            }
            else if (timeType == "year" && !year.HasValue)
            {
                ViewBag.ErrorMessage = "Vui lòng nhập thông tin năm để lọc.";
            }
            else
            {
                // Apply filters based on the provided type
                if (timeType == "day" && day.HasValue && month.HasValue && year.HasValue)
                {
                    query = query.Where(x => x.NgayLap.Year == year.Value &&
                                             x.NgayLap.Month == month.Value &&
                                             x.NgayLap.Day == day.Value);
                }
                else if (timeType == "month" && month.HasValue && year.HasValue)
                {
                    query = query.Where(x => x.NgayLap.Year == year.Value &&
                                             x.NgayLap.Month == month.Value);
                }
                else if (timeType == "year" && year.HasValue)
                {
                    query = query.Where(x => x.NgayLap.Year == year.Value);
                }
            }
        }

        // Convert query result to ViewModel
        var data = query.ToList().Select(x => new StatisticsViewModel
        {
            NgayLap = x.NgayLap.ToString("dd/MM/yyyy"),
            TenKhachHang = x.TenKhachHang ?? "Chưa xác định",
            TenTour = x.TenTour ?? "Chưa xác định",
            TongTien = x.TongTien.ToString("C")
        }).ToList();

        // Calculate Total Revenue based on filtered data
        ViewBag.TotalRevenue = data.Sum(x => decimal.Parse(x.TongTien, System.Globalization.NumberStyles.Currency)).ToString("C");

        // Handle no data case
        if (!data.Any())
        {
            ViewBag.Message = "Không có hóa đơn phù hợp với dữ liệu đã chọn.";
        }

        // Return the filtered data
        return View(data);
    }
    catch (Exception ex)
    {
        // Log the exception and pass the error message to the view
        Console.WriteLine($"Error in Statistics action: {ex.Message}");
        ViewBag.ErrorMessage = "Lỗi hệ thống. Vui lòng thử lại sau.";
        return View(new List<StatisticsViewModel>());
    }
}


    [HttpGet]
public IActionResult ExportStatisticsToExcel(string? timeType, int? day, int? month, int? year)
{
    try
    {
        // Base query: Fetch all HoaDon, KhachHang, and Tour data
        var query = from hd in _context.HoaDons
                    join kh in _context.KhachHangs on hd.MaKhachHang equals kh.MaKhachHang
                    join t in _context.Tours on kh.MaTour equals t.MaTour into TourGroup
                    from tour in TourGroup.DefaultIfEmpty() // Handle NULL values for Tour
                    select new
                    {
                        hd.NgayLap,
                        TenKhachHang = kh.Ten,
                        TenTour = tour != null ? tour.Ten : "Chưa xác định",
                        TongTien = hd.TongTien
                    };

        // Apply filters dynamically based on timeType
        if (!string.IsNullOrEmpty(timeType))
        {
            if (timeType == "day" && (!day.HasValue || !month.HasValue || !year.HasValue))
            {
                return BadRequest("Vui lòng nhập đầy đủ thông tin ngày, tháng và năm để lọc.");
            }
            else if (timeType == "month" && (!month.HasValue || !year.HasValue))
            {
                return BadRequest("Vui lòng nhập đầy đủ thông tin tháng và năm để lọc.");
            }
            else if (timeType == "year" && !year.HasValue)
            {
                return BadRequest("Vui lòng nhập thông tin năm để lọc.");
            }
            else
            {
                // Apply filters dynamically based on selected timeType
                if (timeType == "day" && day.HasValue && month.HasValue && year.HasValue)
                {
                    query = query.Where(x => x.NgayLap.Year == year.Value &&
                                             x.NgayLap.Month == month.Value &&
                                             x.NgayLap.Day == day.Value);
                }
                else if (timeType == "month" && month.HasValue && year.HasValue)
                {
                    query = query.Where(x => x.NgayLap.Year == year.Value &&
                                             x.NgayLap.Month == month.Value);
                }
                else if (timeType == "year" && year.HasValue)
                {
                    query = query.Where(x => x.NgayLap.Year == year.Value);
                }
            }
        }

        // Execute and map filtered data
        var rawData = query.ToList();
        var data = rawData.Select(x => new
        {
            NgayLap = x.NgayLap.ToString("dd/MM/yyyy"),
            TenKhachHang = x.TenKhachHang ?? "Chưa xác định",
            TenTour = x.TenTour ?? "Chưa xác định",
            TongTien = x.TongTien.ToString("C") // Format currency for display
        }).ToList();

        // Calculate total revenue based on filtered data
        var totalRevenue = rawData.Sum(x => x.TongTien);

        // Handle no matching data
        if (!data.Any())
        {
            return BadRequest("Không có dữ liệu để xuất.");
        }

        // Use ClosedXML to generate the Excel file
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Statistics");

            // Add headers to the worksheet
            worksheet.Cell(1, 1).Value = "Ngày Lập";
            worksheet.Cell(1, 2).Value = "Tên Khách Hàng";
            worksheet.Cell(1, 3).Value = "Tên Tour";
            worksheet.Cell(1, 4).Value = "Tổng Tiền";

            // Populate rows with filtered data
            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = data[i].NgayLap;
                worksheet.Cell(i + 2, 2).Value = data[i].TenKhachHang;
                worksheet.Cell(i + 2, 3).Value = data[i].TenTour;
                worksheet.Cell(i + 2, 4).Value = data[i].TongTien;
            }

            // Add total revenue footer
            var lastRow = data.Count + 2; // Footer row for total revenue
            worksheet.Cell(lastRow, 3).Value = "Tổng Doanh Thu:";
            worksheet.Cell(lastRow, 3).Style.Font.Bold = true; // Bold for emphasis
            worksheet.Cell(lastRow, 4).Value = totalRevenue.ToString("C");
            worksheet.Cell(lastRow, 4).Style.Font.Bold = true; // Bold for emphasis

            // Save the Excel file to MemoryStream and return it
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return File(stream.ToArray(),
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Statistics.xlsx");
            }
        }
    }
    catch (Exception ex)
    {
        // Log error for debugging
        Console.WriteLine($"Error in ExportStatisticsToExcel: {ex.Message}");
        return StatusCode(500, "Lỗi hệ thống. Vui lòng thử lại sau.");
    }
}



    }
}
