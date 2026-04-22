using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CategoryService.Models;
using CategoryService.Services;
using Microsoft.AspNetCore.Authorization;

namespace CategoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

    // Only authenticated users can access this endpoi
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAll());

        // Both Admin and User roles can access this endpoint
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _service.GetById(id));

        // Only Admin role can access this endpoint
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            await _service.Add(category);
            return Ok();
        }

        // Only Admin role can access this endpoint
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            await _service.Update(category);
            return Ok();
        }

        // Only Admin role can access this endpoint
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
