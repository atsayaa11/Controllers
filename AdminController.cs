using OnlineMobiles.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMobiles.Controllers
{
    public class AdminController : Controller
    {
        MobilesContext db = new MobilesContext();
        // GET: Admin
        public ActionResult Product()
        {
            return View(db.Products.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product products)
        {
            string fileName = Path.GetFileNameWithoutExtension(products.ImageFile.FileName);
            string fileExtension = Path.GetExtension(products.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + fileExtension;
            products.ProductImage = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            products.ImageFile.SaveAs(fileName);
            db.Products.Add(products);
            db.SaveChanges();
            return View(products);
        }
        public ActionResult Edit(int id)
        {
            var edit = db.Products.Where(model => model.ProductId == id).FirstOrDefault();
            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit(Product products)
        {
            db.Entry(products).State = EntityState.Modified;
            db.SaveChanges();
            return View(products);
        }
        public ActionResult Delete(int id)
        {
            var delete = db.Products.Where(model => model.ProductId == id).FirstOrDefault();
            db.Products.Remove(delete);
            db.SaveChanges();
            return View();
        }
        public ActionResult Customers()
        {
            return View(db.Customers.ToList());
        }
    }
}