using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanCay.Models;
using X.PagedList;

namespace QLBanCay.Areas.Admin.Controllers
{
    [Area("admin")]
    public class DSNhanVienController : Controller
    {
        QlbanCayContext csdl = new QlbanCayContext();
        public IActionResult Index()
        {
            return View();
        }

        [Route("admin/DanhsachNV")]
        public IActionResult DanhsachNV(int? page)
        {
            //Số đối tượng hiển thị tối đa trên 1 trang
            int pageSize = 3;
            //Số trang hiện tại đang truy cập
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            //Khởi tạo 1 danh sách tự do
            var dsnv = csdl.NhanViens.AsNoTracking().OrderBy(x => x.TenNv);
            PagedList<NhanVien> paged_dsnv = new PagedList<NhanVien>(dsnv, pageNumber, pageSize);
            //Khai báo 1 paged danh sách 
            return View(paged_dsnv);
        }

        [Route("Themnhanvien")]
        [HttpGet]
        public IActionResult ThemNVMoi()
        {
            ViewBag.UserName = new SelectList(csdl.Users.ToList(), "UserName", "LoaiUser");
            return View();
        }

        [Route("Themnhanvien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNVMoi(NhanVien nhanvien)
        {
            if (ModelState.IsValid)
            {
                csdl.NhanViens.Add(nhanvien);
                csdl.SaveChanges();
                return RedirectToAction("DanhsachNV");
            }
            return View(nhanvien);
        }

        [Route("Suanhanvien")]
        [HttpGet]
        public IActionResult SuaNhanVien(string manv)
        {
            ViewBag.UserName = new SelectList(csdl.Users.ToList(), "UserName", "LoaiUser");
            var nv = csdl.NhanViens.Find(manv);
            return View(nv);
        }

        [Route("Suanhanvien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNhanVien(NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                csdl.Update(nv);
                /*csdl.Entry(sanpham).State = EntityState.Modified;*/
                csdl.SaveChanges();
                return RedirectToAction("DanhsachNV");
            }
            return View(nv);
        }

        [Route("XoaNhanVien")]
        [HttpGet]
        public IActionResult XoaNhanVien(string maNV)
        {                 

            TempData["Message"] = "";
            var chiTietHD = csdl.HoaDonBans.Where(x => x.MaNv == maNV).ToList();
            if (chiTietHD.Count() > 0)
            {
                TempData["Message"] = "Không xoá được nhân viên này";
                return RedirectToAction("DanhsachNV");
            }
            csdl.Remove(csdl.NhanViens.Find(maNV));
            csdl.SaveChanges();
            TempData["Message"] = "Đã xoá nhân viên thành công";
            return RedirectToAction("DanhsachNV");

        }

    }
}
