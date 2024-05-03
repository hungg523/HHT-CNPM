using AppleEShop.Models;

namespace AppleEShop.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
    }
}
