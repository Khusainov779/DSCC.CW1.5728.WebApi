using DSCC.CW1._5728.DbContexts;
using DSCC.CW1._5728.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSCC.CW1._5728.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly ProductContext _dbContext;

        public CategoryRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int categoryId)
        {
            var category = _dbContext.Categories.Find(categoryId);
            _dbContext.Categories.Remove(category);
            Save();
        }

        public Category GetById(int categoryId)
        {
            return _dbContext.Categories.SingleOrDefault(e => e.Id == categoryId);
        }

        public IQueryable<Category> GetAll()
        {
            return _dbContext.Categories.AsQueryable();
        }

        public void Create(Category category)
        {
            _dbContext.Add(category);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Category category)
        {
            _dbContext.Entry(category).State =
           Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }

    }
}

