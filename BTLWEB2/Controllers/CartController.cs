using BTLWEB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTLWEB2.Controllers
{
    public class CartController : Controller
    {
        SaleShoesEntities1 db = new SaleShoesEntities1();

        [Route("Cart/AddToCart/{id?}/{size?}/{quantity?}")]
        public ActionResult AddToCart(string id, string size, int quantity)
        {
            if (Session["tenTKSS"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var tenTk = Session["tenTKSS"].ToString();
                var checkUserHaveCart = db.tGioHangs.FirstOrDefault(x => x.TenTK == tenTk);
                var product = db.tSanPhams.SingleOrDefault(s => s.MaSP == id);
                if (checkUserHaveCart == null)
                {
                    Random rnd = new Random();
                    var MaGioHang = rnd.Next(999999);
                    var newCart = new tGioHang();
                    newCart.TenTK = Session["tenTKSS"].ToString();
                    newCart.MaGioHang = MaGioHang;
                    db.tGioHangs.Add(newCart);
                    db.SaveChanges();
                    var newDetailCart = new tChiTietGioHang();
                    newDetailCart.Gia = product.Gia;
                    newDetailCart.MaGioHang = MaGioHang;
                    newDetailCart.MaSize = size;
                    newDetailCart.MaSP = id;
                    newDetailCart.SoLuong = quantity;
                    db.tChiTietGioHangs.Add(newDetailCart);
                    db.SaveChanges();
                }
                else
                {
                    var checkCTGH = db.tChiTietGioHangs.FirstOrDefault(x => x.MaGioHang == checkUserHaveCart.MaGioHang && x.MaSP == product.MaSP);
                    if(checkCTGH != null)
                    {
                        checkCTGH.SoLuong = checkCTGH.SoLuong + 1;
                        checkCTGH.Gia = checkCTGH.SoLuong * product.Gia;
                        db.SaveChanges();
                    }
                    else
                    {
                        var newDetailCart = new tChiTietGioHang();
                        newDetailCart.Gia = product.Gia * quantity;
                        newDetailCart.MaGioHang = checkUserHaveCart.MaGioHang;
                        newDetailCart.MaSize = size;
                        newDetailCart.MaSP = id;
                        newDetailCart.SoLuong = quantity;
                        db.tChiTietGioHangs.Add(newDetailCart);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("ShowToCart", "Cart");
            }
        }

        public ActionResult ShowToCart()
        {
            if (Session["tenTKSS"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int totalPrice = 0; 
                var tenTK = Session["tenTKSS"].ToString();
                var giohang = db.tGioHangs.FirstOrDefault(x => x.TenTK == tenTK);
                var cart = db.CartViews.Where(x => x.MaGioHang == giohang.MaGioHang);
                foreach(var item in cart)
                {
                    totalPrice += Int32.Parse(item.totalCart.ToString());
                }
                Session["totalPrice"] = totalPrice.ToString();
                return View(cart);
            }  
        }
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            /*Cart cart = Session["Cart"] as Cart;
            string id_pro = form["id_product"];
            int quantity = int.Parse(form["Quantity"]);
            cart.UpdateQuantity(id_pro, quantity);*/
            return RedirectToAction("ShowToCart", "Cart");
        }

        public ActionResult RemoveCart(string id)
        {
            /*Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);*/
            return RedirectToAction("ShowToCart", "Cart");
        }
    }
}