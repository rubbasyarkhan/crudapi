using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Linq;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly CrudContext db;

        public MyController(CrudContext db)
        {
            this.db = db;
        }

        // GET: api/mycontroller
        [HttpGet]
        public IActionResult GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            var products = db.Cruds
                             .Skip((pageNumber - 1) * pageSize) // Skip the appropriate number of records
                             .Take(pageSize) // Take the number of records specified by pageSize
                             .ToList();

            // Get total count for pagination metadata
            var totalCount = db.Cruds.Count();

            var paginationResponse = new
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = products
            };

            return Ok(paginationResponse); // Return the paginated result with metadata
        }

        // GET: api/mycontroller/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = db.Cruds.Find(id);

            // Check if the product exists
            if (product == null)
            {
                return NotFound(); // Return 404 if product not found
            }

            return Ok(product); // Return the product if it exists
        }

        // POST: api/mycontroller
        [HttpPost]
        public IActionResult AddProduct([FromBody] Crud crud)
        {
            if (crud == null)
            {
                return BadRequest("Product cannot be null");
            }

            db.Cruds.Add(crud);
            db.SaveChanges();
            return StatusCode(201); // Created status
        }

        // DELETE: api/mycontroller/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = db.Cruds.Find(id);
            if (product == null)
            {
                return NotFound(); // Return 404 if product not found
            }

            db.Cruds.Remove(product);
            db.SaveChanges();
            return StatusCode(204); // No content status (successfully deleted)
        }

        // PUT: api/mycontroller/{id}
        [HttpPut("{id}")]
        public IActionResult EditProduct(int id, [FromBody] Crud updatedCrud)
        {
            var product = db.Cruds.Find(id);

            // Check if the product exists
            if (product == null)
            {
                return NotFound(); // Return 404 if the product doesn't exist
            }

            // Update the properties of the product
            product.Name = updatedCrud.Name;
            product.Description = updatedCrud.Description;
            product.Price = updatedCrud.Price; // Add all the necessary properties of the product you want to update

            db.Cruds.Update(product);
            db.SaveChanges();

            return Ok(product); // Return the updated product
        }

        // GET: api/mycontroller/Search/{query}
        [HttpGet("Search/{query}")]
        public IActionResult SearchProduct(string query, int pageNumber = 1, int pageSize = 10)
        {
            var products = db.Cruds
                             .Where(o => o.Name.Contains(query) || o.Description.Contains(query))
                             .Skip((pageNumber - 1) * pageSize) // Skip the appropriate number of records
                             .Take(pageSize) // Take the number of records specified by pageSize
                             .ToList();

            // Get total count for pagination metadata
            var totalCount = db.Cruds
                                .Where(o => o.Name.Contains(query) || o.Description.Contains(query))
                                .Count();

            var paginationResponse = new
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = products
            };

            return Ok(paginationResponse); // Return the paginated result with metadata
        }
    }
}
