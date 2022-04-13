using BTLWEB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTLWEB2.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        SaleShoesEntities2 db = new SaleShoesEntities2();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCheckout()
        {
            if (Session["tenTKSS"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var tenTk = Session["tenTKSS"].ToString();
                Random rnd = new Random();
                DateTime time = DateTime.Now;
                var MaHoaDon = rnd.Next(999999);
                var newHoaDon = new tHoaDon();
                newHoaDon.TenTK = tenTk;
                newHoaDon.MaHD = MaHoaDon;
                newHoaDon.NgayDat = DateTime.Now;
                db.tHoaDons.Add(newHoaDon);
                var giohang = db.tGioHangs.FirstOrDefault(x => x.TenTK == tenTk);
                var ctgh = db.tChiTietGioHangs.Where(x => x.MaGioHang == giohang.MaGioHang);
                foreach(var item in ctgh)
                {
                    var newCTHD = new tChiTietHoaDon();
                    newCTHD.MaHD = MaHoaDon;
                    newCTHD.MaSP = item.MaSP;
                    newCTHD.MaChiTietHoaDon = rnd.Next(999999);
                    newCTHD.SoLuong = item.SoLuong;
                    newCTHD.GhiChu = "asdasd";

                    db.tChiTietHoaDons.Add(newCTHD);
                    

                    db.tChiTietGioHangs.Remove(item);
                }
                db.SaveChanges();
            }
            
            Response.Write("<script>alert('Checkout successfully')</script>");
            return RedirectToAction("Index", "Home");
        }
    }
}