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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
      var categories = await _unitOfWork.Categories.GetAllCategories();
      return _mapper.Map<List<CategoryDto>>(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
      var category = await _unitOfWork.Categories.GetCategoryById(id);

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
      var addedCategory = await _unitOfWork.Categories.AddCategory(category);
      await _unitOfWork.SaveChangesAsync();
      return CreatedAtAction(nameof(GetCategory), new { id = addedCategory.Id }, _mapper.Map<CategoryDto>(addedCategory));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, CategoryDto categoryDto)
    {
      if (id != categoryDto.Id)
      {
        return BadRequest();
      }

      var existingCategory = await _unitOfWork.Categories.GetCategoryById(id);
      if (existingCategory == null)
      {
        return NotFound();
      }

      _mapper.Map(categoryDto, existingCategory);
      await _unitOfWork.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
      var category = await _unitOfWork.Categories.GetCategoryById(id);
      if (category == null)
      {
        return NotFound();
      }

      var deleted = await _unitOfWork.Categories.DeleteCategory(id);
      if (deleted)
      {
        await _unitOfWork.SaveChangesAsync();
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
