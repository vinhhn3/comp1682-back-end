using comp1682_back_end.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace comp1682_back_end.Repositories.Interfaces
{
  public interface IProductRepository
  {
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductById(int id);
    Task<Product> AddProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task<bool> DeleteProduct(int id);
  }
}
