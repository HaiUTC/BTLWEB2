using BTLWEB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTLWEB2.Controllers
{
    public class HomeController : Controller
    {
        SaleShoesEntities2 db = new SaleShoesEntities2();
        public ActionResult Index()
        {
            if(Session["tenTKSS"] != null)
            {
                var tenTK = Session["tenTKSS"].ToString();
                var checkUserHaveCart = db.tGioHangs.FirstOrDefault(x => x.TenTK == tenTK);
                if(checkUserHaveCart != null)
                {
                    List<tChiTietGioHang> allCtGH = db.tChiTietGioHangs.Where(x => x.MaGioHang == checkUserHaveCart.MaGioHang).ToList();
                    Session["numberCart"] = allCtGH.Count().ToString();
                }
                else
                {
                    Session["numberCart"] = 0;
                }
                
            }
            var listProduct = db.tSanPhams.ToList().Take(12);
            return View(listProduct);
        }

       public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tTaiKhoan acc)
        {
            var checkLogin = db.tTaiKhoans.Where(x => x.TenTK.Equals(acc.TenTK) && x.Password.Equals(acc.Password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["tenTKSS"] = acc.TenTK.ToString();
                Session["tenKH"] = checkLogin.TenKH;
                
                if (checkLogin.XacMinh == true)
                {
                    var checkUserHaveCart = db.tGioHangs.FirstOrDefault(x => x.TenTK == acc.TenTK);
                    List<tChiTietGioHang> allCtGH = db.tChiTietGioHangs.Where(x => x.MaGioHang == checkUserHaveCart.MaGioHang).ToList();
                    Session["numberCart"] = allCtGH.Count().ToString();
                    return RedirectToAction("Index", "Manager");
                    Session["Rule"] = checkLogin.XacMinh;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                Response.Write("<script>alert('Wrong username or password')</script>");
            }
            
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(tTaiKhoan acc)
        {
            if (db.tTaiKhoans.Any(x => x.TenTK == acc.TenTK))
            {
                ViewBag.Notification = "This account is already exists.";
                return View();
            }
            else
            {
                db.tTaiKhoans.Add(acc);
                db.SaveChanges();

                Session["tenTKSS"] = acc.TenTK.ToString();
                Session["tenKH"] = acc.TenKH.ToString();
                return RedirectToAction("Index", "Home");
            }
        }   

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}