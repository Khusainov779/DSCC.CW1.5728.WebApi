using DSCC.CW1._5728.DbContexts;
using DSCC.CW1._5728.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSCC.CW1._5728.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int productId)
        {
            var product = _dbContext.Products.Find(productId);
            _dbContext.Products.Remove(product);
            Save();
        }

        public Product GetById(int productId)
        {
            var prod = _dbContext.Products.Find(productId);
            _dbContext.Entry(prod).Reference(s => s.Category).Load();
            return prod;
        }

        public IQueryable<Product> GetAll()
        {
            return _dbContext.Products.AsQueryable();
        }

        public void Create(Product product)
        {
            _dbContext.Add(product);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Product product)
        {
            _dbContext.Entry(product).State =
           Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }

    }
}
