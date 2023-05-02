using Microsoft.AspNetCore.Mvc;
using QLBanCay.Models;

namespace QLBanCay.Controllers
{
    public class AccessController : Controller
    {
        QlbanCayContext csdl = new QlbanCayContext();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = csdl.Users.Where(x => x.UserName == user.UserName &&
                x.Password == user.Password).FirstOrDefault();

                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    /*return RedirectToAction("DanhMucSanPham", "Admin");*/
                    if (u.LoaiUser == "user")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("DanhsachNV", "Admin");
                }
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên đăng nhập đã được sử dụng chưa
                if (HttpContext.Session.GetString(user.UserName) != null)
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập đã tồn tại");
                    return View(user);
                }
                else
                {
                    csdl.Add(user);
                    csdl.SaveChanges();
                    // Đăng nhập người dùng và chuyển hướng đến trang chủ
                    HttpContext.Session.SetString("UserName", user.UserName.ToString());
                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index", "Home");
                }
            }
            // Trả về View với model và thông tin lỗi (nếu có)
            return View(user);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }


    }
}
