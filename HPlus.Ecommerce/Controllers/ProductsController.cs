using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TBJ.Ecommerce.Models;

namespace TBJ.Ecommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DbModels db = new DbModels(); // variable db is created to access the database tables and their records via this variable

        //This method does two jobs, it simply displays all the products on the page as well as helps in
        //Search products operation
        //The incoming "search" string is compared with any of the product names and its stored in a session variable
        public ActionResult Index(string search)
        {
            ViewBag.listProducts = db.Products.Where(x => x.ProductName.Contains(search) || search == null).ToList();  
           
            //if(search != "")
                Session["search"] = search;
           
            return View(ViewBag.listProducts);
        }
       
        // This method helps in getting the product details for a specific product basis its ID
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        //This method is used to add an entry of a product to the Product table in the database
        //The imgfile depicts the image of the product and the product variable depicts the object of type Product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase imgfile)
        {
            string path = UploadImage(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "There was problem uploading the image";

            }
            else
            {
                Product prod = new Product();
                //Session["ProductId"] = prod.ProductId.ToString();

                prod.ProductName = product.ProductName;
                prod.ProductImage = path;
                prod.Discount = product.Discount;
                prod.CurrentPrice = product.CurrentPrice;
                prod.FullPrice = product.FullPrice;
                db.Products.Add(prod);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(product);
        }

        //This method takes care of uploading an image while adding the product to the DB
        //It validates for image path, and basis the file extension it uploads the image to ~/Content/upload/ path
        public string UploadImage(HttpPostedFileBase file)

        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/upload/"), random + Path.GetFileName(file.FileName));

                        file.SaveAs(path);

                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);

                    }

                    catch (Exception ex)
                    {                      
                        path = "-1";

                    }
                }
                else
                {
                    Response.Write("<script>alert('Only png ,jpg or jpeg formats can be uploaded....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('File is not selected'); </script>");
                path = "-1";
            }
            return path;
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        //This method helps in editing the product details in the database through the view
        // POST: Products/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,ProductImage,FullPrice,CurrentPrice,Discount,StarRating")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        //This method helps in Deleting a product from the database through the view

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
