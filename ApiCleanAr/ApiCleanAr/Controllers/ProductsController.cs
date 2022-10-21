using ApiCleanAr.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCleanAr.Models;


namespace ApiCleanAr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        public readonly PRODUCTSContext _dbContext;

        public ProductsController(PRODUCTSContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("List Products")]

        public IActionResult ListProducts()
        {
            List<Product> ListProducts = new List<Product>();

            try
            {
                ListProducts = _dbContext.Products.Include(Category => Category.oCategory).ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = ListProducts });
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = Ex.Message, response = ListProducts });
            }
        }

        [HttpGet]
        [Route("ListProductsid/{IdProduct:int}")]
        public IActionResult ListProductsId(int IdProduct)
        {
            Product Product = _dbContext.Products.Find(IdProduct);

            if(Product == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                Product = _dbContext.Products.Include(c => c.oCategory)
                    .Where(p => p.IdProducts == IdProduct).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { message = "Ok", Response = Product });
            }

            catch(Exception Ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = Ex.Message, Response = Product });
            }
        }

        [HttpPost]
        [Route("Create Product")]
        public IActionResult CreateProduct([FromBody] Product obj)
        {
            try
            {
                _dbContext.Products.Add(obj);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });

            }
            catch(Exception Ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = Ex.Message });
            }
        }

        [HttpPut]
        [Route("Update Product")]
        public IActionResult UpdateProduct([FromBody] Product obj)
        {
            Product Product = _dbContext.Products.Find(obj.IdProducts);

            if (Product == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                Product.NameProducts = obj.NameProducts is null ? Product.NameProducts : obj.NameProducts;
                Product.DescriptionProduct = obj.DescriptionProduct is null ? Product.DescriptionProduct : obj.DescriptionProduct;
                Product.Sctok = obj.Sctok is null ? Product.Sctok : obj.Sctok;
                Product.IdCategory = obj.IdCategory is null ? Product.IdCategory : obj.IdCategory;
                Product.Price = obj.Price is null ? Product.Price : obj.Price;

                _dbContext.Products.Update(Product);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "Ok" });

            }catch(Exception Ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = Ex.Message });
            }
        }

        [HttpDelete]
        [Route("Delete Product/{IdProduct:int}")]
        public IActionResult DeleteProduct(int IdProduct)
        {
            Product Product = _dbContext.Products.Find(IdProduct);

            if (Product == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                _dbContext.Products.Remove(Product);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "Ok" });

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = Ex.Message });
            }
        }
    }
}
