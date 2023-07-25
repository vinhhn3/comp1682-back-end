using comp1682_back_end.Data;
using comp1682_back_end.Models;
using comp1682_back_end.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace comp1682_back_end.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
      return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductById(int id)
    {
      return await _context.Products.FindAsync(id);
    }

    public async Task<Product> AddProduct(Product product)
    {
      _context.Products.Add(product);
      await _context.SaveChangesAsync();
      return product;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
      _context.Entry(product).State = EntityState.Modified;
      await _context.SaveChangesAsync();
      return product;
    }

    public async Task<bool> DeleteProduct(int id)
    {
      var product = await _context.Products.FindAsync(id);
      if (product == null)
        return false;

      _context.Products.Remove(product);
      await _context.SaveChangesAsync();
      return true;
    }
  }
}
