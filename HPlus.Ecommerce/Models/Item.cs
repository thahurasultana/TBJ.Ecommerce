using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TBJ.Ecommerce.Models
{
    public class Item
    {
        private Product product = new Product();
        private int quantity;
        public int Quantity { get => quantity; set => quantity = value; }
        public Product Product { get => product; set => product = value; }

        public Item(){

            }
        public Item(Product product,int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }

       
    }
}