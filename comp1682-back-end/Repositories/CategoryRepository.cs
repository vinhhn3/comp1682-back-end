using comp1682_back_end.Data;
using comp1682_back_end.Models;
using comp1682_back_end.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace comp1682_back_end.Repositories
{
  public class CategoryRepository : ICategoryRepository
  {
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
      return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryById(int id)
    {
      return await _context.Categories.FindAsync(id);
    }

    public async Task<Category> AddCategory(Category category)
    {
      _context.Categories.Add(category);
      await _context.SaveChangesAsync();
      return category;
    }

    public async Task<Category> UpdateCategory(Category category)
    {
      _context.Entry(category).State = EntityState.Modified;
      await _context.SaveChangesAsync();
      return category;
    }

    public async Task<bool> DeleteCategory(int id)
    {
      var category = await _context.Categories.FindAsync(id);
      if (category == null)
        return false;

      _context.Categories.Remove(category);
      await _context.SaveChangesAsync();
      return true;
    }
  }
}
