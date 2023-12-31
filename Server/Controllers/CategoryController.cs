using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Services.CategoryServices;
using ElvenNoteWepApp.Shared.Models.Category;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Category(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if(category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreate model)
        {
            if(model == null || !ModelState.IsValid)
                return BadRequest();

            bool wasSuccessful = await _categoryService.CreateCategoryAsync(model);

            if(wasSuccessful)
                return Ok();

            return UnprocessableEntity();
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, CategoryEdit model)
        {
            if(model == null || !ModelState.IsValid) 
                return BadRequest();

            bool wasSuccessful = await _categoryService.UpdateCategoryAsync(model);

            if(wasSuccessful)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _categoryService.GetCategoryByIdAsync(id);

            if(note == null)
                return NotFound();

            bool wasSuccessful = await _categoryService.DeleteCategoryAsync(id);

            if(!wasSuccessful)
                return BadRequest();
            
            return Ok();
        }
    }
}