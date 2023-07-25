using AutoMapper;

using comp1682_back_end.Data;
using comp1682_back_end.Models;
using comp1682_back_end.Repositories.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace comp1682_back_end.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriesController : ControllerBase
  {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
    {
      _categoryRepository = categoryRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
      var categories = await _categoryRepository.GetAllCategories();
      return _mapper.Map<List<CategoryDto>>(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
      var category = await _categoryRepository.GetCategoryById(id);

      if (category == null)
      {
        return NotFound();
      }

      return _mapper.Map<CategoryDto>(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto categoryDto)
    {
      var category = _mapper.Map<Category>(categoryDto);
      var addedCategory = await _categoryRepository.AddCategory(category);
      return CreatedAtAction(nameof(GetCategory), new { id = addedCategory.Id }, _mapper.Map<CategoryDto>(addedCategory));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, CategoryDto categoryDto)
    {
      if (id != categoryDto.Id)
      {
        return BadRequest();
      }

      var existingCategory = await _categoryRepository.GetCategoryById(id);
      if (existingCategory == null)
      {
        return NotFound();
      }

      _mapper.Map(categoryDto, existingCategory);
      var updatedCategory = await _categoryRepository.UpdateCategory(existingCategory);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
      var category = await _categoryRepository.GetCategoryById(id);
      if (category == null)
      {
        return NotFound();
      }

      var deleted = await _categoryRepository.DeleteCategory(id);
      if (deleted)
      {
        return NoContent();
      }
      else
      {
        // Something went wrong with deletion
        return StatusCode(500);
      }
    }
  }
}
