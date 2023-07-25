using System.Threading.Tasks;

namespace comp1682_back_end.Repositories.Interfaces
{
  public interface IUnitOfWork
  {
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }

    Task<int> SaveChangesAsync();
  }
}
