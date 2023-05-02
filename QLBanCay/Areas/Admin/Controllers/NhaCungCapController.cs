using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanCay.Models;
using X.PagedList;

namespace QLBanCay.Areas.Admin.Controllers
{
    [Area("admin")]
    public class NhaCungCapController : Controller
    {
        QlbanCayContext csdl = new QlbanCayContext();
        public IActionResult Index()
        {
            return View();
        }

        [Route("NhaCungCap")]
        public IActionResult NhaCungCap(int? page)
        {
            //Số đối tượng hiển thị tối đa trên 1 trang
            int pageSize = 3;
            //Số trang hiện tại đang truy cập
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            //Khởi tạo 1 danh sách tự do
            var ncc = csdl.NhaCungCaps.AsNoTracking().OrderBy(x => x.MaNcc);
            PagedList<NhaCungCap> paged_ncc = new PagedList<NhaCungCap>(ncc, pageNumber, pageSize);
            //Khai báo 1 paged danh sách 
            return View(paged_ncc);
        }

        [Route("Themnhacc")]
        [HttpGet]
        public IActionResult ThemNCC()
        {

            return View();
        }

        [Route("Themnhacc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNCC(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                csdl.NhaCungCaps.Add(ncc);
                csdl.SaveChanges();
                return RedirectToAction("Nhacungcap");
            }
            return View(ncc);
        }

        [Route("SuaNCC")]
        [HttpGet]
        public IActionResult SuaNCC(string mancc)
        {
            var ncc = csdl.NhaCungCaps.Find(mancc);
            return View(ncc);
        }

        [Route("SuaNCC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNCC(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                csdl.Update(ncc);
                /*csdl.Entry(sanpham).State = EntityState.Modified;*/
                csdl.SaveChanges();
                return RedirectToAction("Nhacungcap");
            }
            return View(ncc);
        }

        [Route("XoaNCC")]
        [HttpGet]
        public IActionResult XoaNCC(string maNCC)
        {
            TempData["Message"] = "";
            csdl.Remove(csdl.NhaCungCaps.Find(maNCC));
            csdl.SaveChanges();
            TempData["Message"] = "Đã xoá nhà cung cấp thành công";
            return RedirectToAction("Nhacungcap");

        }
    }
}
