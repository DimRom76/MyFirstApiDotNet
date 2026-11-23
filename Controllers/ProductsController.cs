using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MyFirstApi.Controllers
{
    [ApiController]

    [Route("api/[controller]")] // means the route is based on the controller name (ProductsController → products)

    public class ProductsController : ControllerBase
    {
        // http://localhost:5243/api/products
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            return new List<string> { "Apple", "Banana", "Orange" };
        }

        
        // http://localhost:5243/api/products/featured
        [HttpGet("featured")]
        public string GetFeaturedProduct() => "Mango";
        
        // http://localhost:5243/api/products/{id}  any string id (e.g., "abc", "xyz") except "featured"
        [HttpGet("{s_id}")]
        public string GetProductById(string s_id) => $"Product {s_id}";
        // [HttpGet("{s_id?}")]  // - optional parameter
        // public string GetProductById(string? s_id = "optional") => $"Product {s_id}";


        // // http://localhost:5243/api/products/{id}  any number id (e.g., 1, 2, 3)
        [HttpGet("{id:int:min(0)}")] // :int - constraints? can be specific range or type
        public string GetProductById(int id) => $"Product {id}";

        [HttpGet("file/{*fileName}")] // catch-all route for file paths
        public string GetFeaturedProduct(string fileName) => $"File {fileName}";

        [HttpGet("search")] // search products by query
        public string SearchProducts(string query, int? page = 1) => $"Search results for {query} on page {page}";


        [HttpPost]
        public ActionResult<string> Post(
            [FromBody, Required, StringLength(100, MinimumLength = 2)] string newProduct)
        {
            return $"Added: {newProduct}";
        }

        [HttpPut("{id}")]
        public ActionResult<string> Put(
            [Range(1, int.MaxValue)] int id,
            [FromBody, Required, StringLength(100, MinimumLength = 2)] string updatedProduct)
        {
            return $"Updated product {id} to: {updatedProduct}";
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete([Range(1, int.MaxValue)] int id)
        {
            return $"Deleted product with ID: {id}";
        }
    }
}