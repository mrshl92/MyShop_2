using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products ;
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }
        public  void Commit() 
        {
            cache["products"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);        
        }
        public void Update(Product produit)
        {
            Product productToUpdate = products.Find(p => p.Id == produit.Id);
            if(productToUpdate != null)
            {
                productToUpdate = produit;
            }
            else
            {
                throw new Exception("Product no found ! ");
            }           
        }
        public Product Find(string Id)
        {
            Product productToFind = products.Find(p => p.Id == Id);
            if (productToFind != null)
            {
                return productToFind;
            }
            else
            {
                throw new Exception("Product no found ! ");
            }
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
           
            }
            else
            {
                throw new Exception("Product no found ! ");
            }
        }

    }
}
