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

        [HttpGet]
        public IActionResult GetProduct()
        {
            return 
                Ok(db.Cruds.ToList());
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
        
        [HttpPut]
        public IActionResult EditProduct(int id)

        {
            var product = db.Cruds.Find(id);
            db.Cruds.Update(product);
            db.SaveChanges();
            return StatusCode(201);
        }



    }
}
