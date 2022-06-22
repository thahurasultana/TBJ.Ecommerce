using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBJ.Ecommerce.Models;
namespace TBJ.Ecommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly DbModels db = new DbModels();
        public ActionResult Index()
        {
            return View();
        }
        // This method is used to check whether an id exists in the List of cart or not
        //If it exists, it returns the index of that id, otherwise returns -1
        private int IsExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)

                if (cart[i].Product.ProductId == id)
                    return i;
            return -1;
        }
        //This method is used to delete the entire selection of product (with quantity > 1) at once
        //It calls isExisting method to check whether the given id exists in the List of product
        //It gets the index of that id and removes it completely from the index
        public ActionResult Delete(int id)
        {
            int index = IsExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
     
                cart.RemoveAt(index);
            
            Session["cart"] = cart;
            return View("Cart");
        }

        //This method is used to decrement the quantity of a product in the shopping cart
        //It first calls IsExisting method to check whether that id which is asked to be removed exists in the list or not
        //If it exists, it gets the index of that id in the list and checks the quantity value
        //If quantity of that product is more than 1 then it decrements it by 1
        // Otherwise(which means that the quantity of the product is equal to 1), then in that case, simply that product is removed from the cart
        public ActionResult RemoveQuantity(int id)
        {
            int index = IsExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            if(cart[index].Quantity>1)
            {
                cart[index].Quantity--;
            }
            else
            {
                cart.RemoveAt(index);
            }
            Session["cart"] = cart;
            return View("Cart");
        }

        //This method is used to Add the quantity of a product in the shopping cart
        //It first calls IsExisting method to check whether that id which is asked to be Added exists in the list or not
        //If it exists, it gets the index of that id in the list and increments its value by 1
        public ActionResult AddQuantity(int id)
        {
            int index = IsExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            
                cart[index].Quantity++;          
            Session["cart"] = cart;
            return View("Cart");
        }
        //
        //To add to cart, this method first checks whether the cart is empty
        //If it is empty then it it adds the item to the cart and saves the session
        //If it is not empty then it checks whether same product already exists in the cart or not
        //If same product already exists then it increments the quantity of that products, otherwise adds it
        public ActionResult AddToCart(int id)
        {
            if(Session["cart"]==null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(db.Products.Find(id),1));

                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = IsExisting(id);
                if (index == -1)
                    cart.Add(new Item(db.Products.Find(id), 1));
                else
                    cart[index].Quantity++;
                Session["cart"] = cart;
            }
            return View("Cart");
        }
    }
}