using comp1682_back_end.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace comp1682_back_end.Repositories.Interfaces
{
  public interface ICategoryRepository
  {
    Task<IEnumerable<Category>> GetAllCategories();
    Task<Category> GetCategoryById(int id);
    Task<Category> AddCategory(Category category);
    Task<Category> UpdateCategory(Category category);
    Task<bool> DeleteCategory(int id);
  }
}
