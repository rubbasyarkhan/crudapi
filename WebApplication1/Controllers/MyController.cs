using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

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


        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = db.Cruds.Find(id);

            // Check if the product exists
            if (product == null)
            {
                return NotFound(); 
            }

            return Ok(product); // Return the product if it exists
        }


        [HttpPost]
        public IActionResult AddProduct(Crud crud)

        {
            db.Cruds.Add(crud);
            db.SaveChanges();
            return StatusCode(201);
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)

        {
            var product = db.Cruds.Find(id);
            db.Cruds.Remove(product);
            db.SaveChanges();
            return StatusCode(201);
        }

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




    }
}
