using BTLWEB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTLWEB2.Controllers
{
    public class ProductController : Controller
    {
        SaleShoesEntities2 db = new SaleShoesEntities2();
        public ActionResult Index()
        {
            var products = db.tSanPhams.ToList();
            return View(products);
        }

        public ActionResult DetailProduct(string id)
        {
            var product = db.tSanPhams.FirstOrDefault(x => x.MaSP == id);
            return View(product);
        }
    }
}