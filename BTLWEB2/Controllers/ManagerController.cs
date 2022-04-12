using BTLWEB2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTLWEB2.Controllers.Admin
{
    public class ManagerController : Controller
    {
        SaleShoesEntities1 db = new SaleShoesEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminCustomer()
        {
            var customer = db.tTaiKhoans.ToList();
            return View(customer);
        }

        public ActionResult AdminProduct()
        {
            var sanPhams = db.tSanPhams.ToList();
            return View(sanPhams);
        }

        public ActionResult AdminOrder()
        {
            var order = db.tHoaDons.ToList();
            return View(order);
        }

        [HttpGet]
        public ActionResult EditCustomer(string TenTK)
        {
            var tk = db.tTaiKhoans.Where(x => x.TenTK == TenTK).First();
            return View(tk);
        }

        [HttpPost]
        public ActionResult EditCustomer(tTaiKhoan tk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tk).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult RemoveCustomer(string TenTK)
        {
            var tk = db.tTaiKhoans.Single(n => n.TenTK == TenTK);
            if (tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tk);
        }

        [HttpPost, ActionName("RemoveCustomer")]
        public ActionResult ConfirmRemoveCustomer(string TenTK)
        {
            var tk = db.tTaiKhoans.Single(n => n.TenTK == TenTK);
            if (tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.tTaiKhoans.Remove(tk);
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tSanPham sp, HttpPostedFileBase Anh)
        {
            if (ModelState.IsValid)
            {
                if (Anh != null)
                {
                    var pic = System.IO.Path.GetFileName(Anh.FileName);
                    var path = System.IO.Path.Combine(
                        Server.MapPath("~/Content/images"), pic);
                    // file is uploaded
                    Anh.SaveAs(path);
                    sp.Anh = pic;
                }
                db.tSanPhams.Add(sp);
            }

            db.SaveChanges();
            ViewBag.message = "Add Product successfully!";
            return View();
        }



        // Edit Product 
        public ActionResult Edit(string id)
        {
            var product = db.tSanPhams.Where(x => x.MaSP == id).First();
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(tSanPham sp, HttpPostedFileBase Anh)
        {
            var product = db.tSanPhams.Where(x => x.MaSP == sp.MaSP).First();
            product.MaSP = sp.MaSP;
            product.TenSP = sp.TenSP;
            product.Gia = sp.Gia;
            product.GioiTinh = sp.GioiTinh;
            product.sale = sp.sale;
            product.TheLoai = sp.TheLoai;
            if (Anh != null)
            {
                var pic = System.IO.Path.GetFileName(Anh.FileName);
                var path = System.IO.Path.Combine(
                    Server.MapPath("~/Content/images"), pic);
                // file is uploaded
                Anh.SaveAs(path);
                product.Anh = pic;
            }
            ViewBag.message = "Update Product successfully!";
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();


            return View();
        }

        [HttpGet]
        public ActionResult RemoveProduct(string id)
        {
            var sanpham = db.tSanPhams.Single(n => n.MaSP == id);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(sanpham);
        }

        [HttpPost, ActionName("RemoveProduct")]
        public ActionResult ConfirmRemoveProduct(string id)
        {
            var sanpham = db.tSanPhams.Single(n => n.MaSP == id);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.tSanPhams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("AdminProduct", "Manage");
        }
    }
}