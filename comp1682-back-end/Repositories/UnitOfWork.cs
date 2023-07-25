using comp1682_back_end.Data;
using comp1682_back_end.Repositories.Interfaces;
using System.Threading.Tasks;

namespace comp1682_back_end.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
      _context = context;
      Products = new ProductRepository(_context);
      Categories = new CategoryRepository(_context);
    }

    public IProductRepository Products { get; private set; }
    public ICategoryRepository Categories { get; private set; }

    public async Task<int> SaveChangesAsync()
    {
      return await _context.SaveChangesAsync();
    }
  }
}
