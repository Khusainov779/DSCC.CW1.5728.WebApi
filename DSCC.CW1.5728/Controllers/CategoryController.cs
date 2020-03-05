using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DSCC.CW1._5728.Model;
using DSCC.CW1._5728.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSCC.CW1._5728.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        // GET: api/Product
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryRepository.GetAll();
            return new OkObjectResult(categories);
            //return new string[] { "value1", "value2" };
        }
        // GET: api/Product/5
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            var product = _categoryRepository.GetById(id);
            return new OkObjectResult(product);
            //return "value";
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody]Category category)
        {
            using (var scope = new TransactionScope())
            {
                _categoryRepository.Create(category);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Category category)
        {
            if (category != null)
            {
                using (var scope = new TransactionScope())
                {
                    _categoryRepository.Update(category);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            return new OkResult();
        }
    }
}
