using ApiCleanAr.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCleanAr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly PRODUCTSContext _dbContext;

        public CategoryController(PRODUCTSContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("List Categorys")]

        public IActionResult ListCategory()
        {
            List<Category> ListCategory = new List<Category>();

            try
            {
                ListCategory = _dbContext.Categories.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = ListCategory });
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = Ex.Message, response = ListCategory });
            }
        }

        [HttpGet]
        [Route("List Category Id/{IdCategory:int}")]
        public IActionResult ListCategoryId(int IdCategory)
        {
            Category category = _dbContext.Categories.Find(IdCategory);

            if(category == null)
            {
                return BadRequest("Category no encontrada");
            }
            try
            {
                category = _dbContext.Categories.Include(c => c.IdCategory)
                    .Where(p => p.IdCategory == IdCategory).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", Response = category });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, Response = category });
            }
        }

        [HttpPost]
        [Route("Create Category")]
        public IActionResult CreateCategory([FromBody] Category objCat)
        {
            try
            {
                _dbContext.Categories.Add(objCat);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch(Exception Ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = Ex.Message });
            }
        }

        [HttpPut]
        [Route("Update Category")]
        public IActionResult UpdateCategory([FromBody] Category objCat)
        {
            Category category = _dbContext.Categories.Find(objCat.IdCategory);

            if (category == null)
            {
                return BadRequest("Categoria no encontrado");
            }

            try
            {
                category.DescriptionCategory = objCat.DescriptionCategory is null ?
                               category.DescriptionCategory : objCat.DescriptionCategory;

                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch(Exception Ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = Ex.Message });
            }
        }

        [HttpDelete]
        [Route("Delete Category/{IdCategory:int}")]
        public IActionResult DeleteCategory(int IdCategory)
        {
            Category category = _dbContext.Categories.Find(IdCategory);

            if (category == null)
            {
                return BadRequest("Categoria no encontrado");
            }
            try
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "Ok" });
            }
            catch(Exception Ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = Ex.Message });
            }
        }
    }
}
