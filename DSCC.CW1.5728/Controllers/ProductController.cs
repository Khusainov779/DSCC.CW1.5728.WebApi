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
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;
        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        // GET: api/Product
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productRepository.GetAll();
            return new OkObjectResult(products);
            //return new string[] { "value1", "value2" };
        }
        // GET: api/Product/5
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            var product = _productRepository.GetById(id);
            return new OkObjectResult(product);
            //return "value";
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            using (var scope = new TransactionScope())
            {
                _productRepository.Create(product);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    _productRepository.Update(product);
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
            _productRepository.Delete(id);
            return new OkResult();
        }
    }
}
