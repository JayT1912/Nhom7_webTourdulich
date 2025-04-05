using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom7_webTourdulich.Models;
using QRCoder; // Thư viện QR Code
using System.Threading.Tasks;
using System;

public class KhachHangController : Controller
{
    private readonly QuanLyTourContext _context;

    public KhachHangController(QuanLyTourContext context)
    {
        _context = context;
    }

    // GET: KhachHang/Create
    public async Task<IActionResult> Create()
    {
        // Lấy danh sách Tour và truyền vào ViewBag
        var tours = await _context.Tours.ToListAsync();
        ViewBag.Tours = tours;

        return View(new KhachHang());
    }

  [HttpPost]    
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(KhachHang khachHang)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Lưu thông tin khách hàng vào cơ sở dữ liệu
                _context.Add(khachHang);
                await _context.SaveChangesAsync();

                // Tạo URL cho QR Code
                string qrUrl = $"http://:5180/KhachHang/172.20.10.9AutoSaveHoaDon/{khachHang.MaKhachHang}";

                // Lấy số lượng hóa đơn hiện tại
                int rowCount = await _context.HoaDons.CountAsync();
                ViewBag.InitialRowCount = rowCount;

                // Truyền KhachHangId vào ViewBag
                ViewBag.KhachHangId = khachHang.MaKhachHang;

                // Tạo mã QR từ URL
                using (var qrGenerator = new QRCodeGenerator())
                {
                    var qrData = qrGenerator.CreateQrCode(qrUrl, QRCodeGenerator.ECCLevel.Q);
                    var qrCode = new PngByteQRCode(qrData);
                    var qrCodeImage = qrCode.GetGraphic(20);

                    // Chuyển mã QR sang Base64 để hiển thị trong View
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(qrCodeImage);
                }

                // Trả về View hiển thị mã QR
                return View("QRDisplay");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi tạo khách hàng: {ex.Message}");
            }
        }

        // Nếu có lỗi, tải lại danh sách Tour
        ViewBag.Tours = await _context.Tours.ToListAsync();
        return View(khachHang);
    }

    [HttpGet]
public async Task<IActionResult> AutoSaveHoaDon(int id)
{
    try
    {
        // Check if the customer exists
        var khachHang = await _context.KhachHangs.FindAsync(id);
        if (khachHang == null)
        {
            return Content("<h1 style='color: red;'>Khách hàng không tồn tại</h1>", "text/html");
        }

        // Retrieve MaTour from the customer
        var maTour = khachHang.MaTour;
        if (!maTour.HasValue)
        {
            return Content("<h1 style='color: red;'>Khách hàng chưa chọn tour</h1>", "text/html");
        }

        // Find the Tour using MaTour
        var tour = await _context.Tours
            .Include(t => t.NhomTours) // Include related NhomTours
            .FirstOrDefaultAsync(t => t.MaTour == maTour.Value);

        if (tour == null)
        {
            return Content("<h1 style='color: red;'>Tour không tồn tại</h1>", "text/html");
        }

        // Ensure the NhomTour exists for the selected Tour
        var nhomTour = tour.NhomTours.FirstOrDefault(); // Retrieve the first related NhomTour
        if (nhomTour == null)
        {
            return Content("<h1 style='color: red;'>Nhóm tour không tồn tại</h1>", "text/html");
        }

        // Retrieve GiaTour
        var giaTour = await _context.GiaTours.FindAsync(tour.MaGiaTour);
        if (giaTour == null)
        {
            return Content("<h1 style='color: red;'>Giá tour không tồn tại</h1>", "text/html");
        }

        // Validate GiaTour value
        decimal gia = giaTour.Gia;
        if (gia <= 0)
        {
            return Content("<h1 style='color: red;'>Giá tour không hợp lệ</h1>", "text/html");
        }

        // Create HoaDon with MaNhomTour
        var hoaDon = new HoaDon
        {
            MaKhachHang = id,
            MaNhomTour = nhomTour.MaNhomTour, // Use the retrieved MaNhomTour
            NgayLap = DateOnly.FromDateTime(DateTime.Now),
            TongTien = gia,
            NgayDi = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
            GioDi = TimeOnly.FromDateTime(DateTime.Now),
            DiemDon = "Sân bay Tân Sơn Nhất",
            ThanhToan = "Đã thanh toán",
        };

        // Save HoaDon into the database
        _context.HoaDons.Add(hoaDon);
        await _context.SaveChangesAsync();

        // Generate and return HTML invoice
        string responseHtml = $@"
            <!DOCTYPE html>
            <html lang='vi'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Hóa Đơn Thanh Toán</title>
                <style>
                    body {{
                        font-family: 'Arial', sans-serif;
                        margin: 20px;
                        background-color: #f9f9f9;
                        color: #333;
                    }}
                    .invoice-container {{
                        max-width: 800px;
                        margin: auto;
                        padding: 20px;
                        background: #fff;
                        border-radius: 10px;
                        box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.1);
                    }}
                    h1 {{
                        text-align: center;
                        color: #4CAF50;
                        margin-bottom: 20px;
                    }}
                    .header {{
                        text-align: center;
                        margin-bottom: 40px;
                    }}
                    .header p {{
                        margin: 5px 0;
                        font-size: 14px;
                        color: #555;
                    }}
                    table {{
                        width: 100%;
                        border-collapse: collapse;
                        margin-top: 20px;
                    }}
                    table th, table td {{
                        padding: 12px;
                        text-align: left;
                        border: 1px solid #ddd;
                    }}
                    table th {{
                        background-color: #4CAF50;
                        color: white;
                    }}
                    .total-row td {{
                        font-weight: bold;
                        text-align: right;
                    }}
                    .footer {{
                        text-align: center;
                        margin-top: 40px;
                        font-size: 12px;
                        color: #777;
                    }}
                </style>
            </head>
            <body>
                <div class='invoice-container'>
                    <h1>HÓA ĐƠN THANH TOÁN</h1>
                    <div class='header'>
                        <p><strong>Tên khách hàng:</strong> {khachHang.Ten}</p>
                        <p><strong>Tên tour:</strong> {tour.Ten}</p>
                        <p><strong>Ngày lập:</strong> {hoaDon.NgayLap:yyyy-MM-dd}</p>
                    </div>
                    <table>
                        <tr>
                            <th>Thông Tin</th>
                            <th>Chi Tiết</th>
                        </tr>
                        <tr>
                            <td>Mã Hóa Đơn</td>
                            <td>{hoaDon.MaHoaDon}</td>
                        </tr>
                        <tr>
                            <td>Tên Khách Hàng</td>
                            <td>{khachHang.Ten}</td>
                        </tr>
                        <tr>
                            <td>Tên Tour</td>
                            <td>{tour.Ten}</td>
                        </tr>
                        <tr>
                            <td>Ngày Đi</td>
                            <td>{hoaDon.NgayDi:yyyy-MM-dd}</td>
                        </tr>
                        <tr>
                            <td>Giờ Đi</td>
                            <td>{hoaDon.GioDi:HH Giờ}</td>
                        </tr>
                        <tr class='total-row'>
                            <td>Tổng Tiền</td>
                            <td>{hoaDon.TongTien:N0} VND</td>
                        </tr>
                    </table>
                    <div class='footer'>
                        <p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi!</p>
                        <p>Khu Công nghệ cao TP.HCM (SHTP), Xa lộ Hà Nội, P. Hiệp Phú, TP. Thủ Đức, TP.HCM</p>
                        <p>Hotline: +012 345 6789</p>
                        <p>Email: dulichhomnay@gmail.com</p>
                    </div>
                </div>
            </body>
            </html>";

        return Content(responseHtml, "text/html");
    }
    catch (Exception ex)
    {
        // Log and return the error message
        Console.WriteLine($"Error: {ex.Message}");
        return Content($"<h1 style='color: red;'>Lỗi: {ex.Message}</h1>", "text/html");
    }
}

    [HttpGet]
    public async Task<IActionResult> GetRowCount()
    {
        int currentRowCount = await _context.HoaDons.CountAsync();
        return Ok(new { rowCount = currentRowCount });
    }


[HttpGet]
public async Task<IActionResult> GetHoaDon(int khachHangId)
{
    try
    {
        // Check if the customer exists
        var khachHang = await _context.KhachHangs.FindAsync(khachHangId);
        if (khachHang == null)
        {
            return Content("<h1 style='color: red;'>Khách hàng không tồn tại</h1>", "text/html");
        }

        // Check if a corresponding invoice (HoaDon) exists
        var hoaDon = await _context.HoaDons.FirstOrDefaultAsync(hd => hd.MaKhachHang == khachHangId);
        if (hoaDon == null)
        {
            // Optionally, you can invoke AutoSaveHoaDon here if no HoaDon exists
            return Content("<h1 style='color: red;'>Hóa đơn không tồn tại</h1>", "text/html");
        }

        // Retrieve additional details for the invoice
        var tour = await _context.Tours
            .Include(t => t.NhomTours) // Include related tour groups
            .FirstOrDefaultAsync(t => t.MaTour == khachHang.MaTour);

        var tenKhachHang = khachHang.Ten ?? "N/A";
        var tenTour = tour?.Ten ?? "N/A";

        // Build and render the invoice HTML
   string responseHtml = $@"
    <!DOCTYPE html>
    <html lang='vi'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Hóa Đơn Thanh Toán</title>
        <style>
            body {{
                font-family: 'Arial', sans-serif;
                margin: 20px;
                background-color: #f9f9f9;
                color: #333;
            }}
            .invoice-container {{
                max-width: 800px;
                margin: auto;
                padding: 20px;
                background: #fff;
                border-radius: 10px;
                box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.1);
            }}
            h1 {{
                text-align: center;
                color: #4CAF50;
                margin-bottom: 20px;
            }}
            table {{
                width: 100%;
                border-collapse: collapse;
                margin-top: 20px;
            }}
            table th, table td {{
                padding: 12px;
                text-align: left;
                border: 1px solid #ddd;
            }}
            table th {{
                background-color: #4CAF50;
                color: white;
            }}
            .footer {{
                text-align: center;
                margin-top: 20px;
            }}
            .back-button {{
                display: inline-block;
                margin-top: 20px;
                padding: 10px 20px;
                background-color: #4CAF50;
                color: white;
                text-decoration: none;
                border-radius: 5px;
                font-size: 16px;
            }}
            .back-button:hover {{
                background-color: #45a049;
            }}
        </style>
    </head>
    <>
                <div class='invoice-container'>
                    <h1>HÓA ĐƠN THANH TOÁN</h1>
                    <div class='header'>
                        <p><strong>Tên khách hàng:</strong> {khachHang.Ten}</p>
                        <p><strong>Tên tour:</strong> {tour.Ten}</p>
                        <p><strong>Ngày lập:</strong> {hoaDon.NgayLap:yyyy-MM-dd}</p>
                    </div>
                    <table>
                        <tr>
                            <th>Thông Tin</th>
                            <th>Chi Tiết</th>
                        </tr>
                        <tr>
                            <td>Mã Hóa Đơn</td>
                            <td>{hoaDon.MaHoaDon}</td>
                        </tr>
                        <tr>
                            <td>Tên Khách Hàng</td>
                            <td>{khachHang.Ten}</td>
                        </tr>
                        <tr>
                            <td>Tên Tour</td>
                            <td>{tour.Ten}</td>
                        </tr>
                        <tr>
                            <td>Ngày Đi</td>
                            <td>{hoaDon.NgayDi:yyyy-MM-dd}</td>
                        </tr>
                        <tr>
                            <td>Giờ Đi</td>
                            <td>{hoaDon.GioDi:HH Giờ}</td> 
                        </tr>
                        <tr class='total-row'>
                            <td>Tổng Tiền</td>
                            <td>{hoaDon.TongTien:N0} VND</td>
                        </tr>
                    </table>
                    <div class='footer'>
                        <p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi!</p>
                        <p>Khu Công nghệ cao TP.HCM (SHTP), Xa lộ Hà Nội, P. Hiệp Phú, TP. Thủ Đức, TP.HCM</p>
                        <p>Hotline: +012 345 6789</p>
                        <p>Email: dulichhomnay@gmail.com</p>
                    </div>
                </div>
            </table>
           
                <a href='/Home/Index' class='back-button'>Quay về Trang Chủ</a>
            </div>
        </div>
    </body>
    </html>";


        return Content(responseHtml, "text/html");
    }
    catch (Exception ex)
    {
        // Handle any unexpected errors and return an error message
        return Content($"<h1 style='color: red;'>Lỗi: {ex.Message}</h1>", "text/html");
    }
}


}
