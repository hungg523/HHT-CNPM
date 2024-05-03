using AppleEShop.Data;
using AppleEShop.Models;

namespace AppleEShop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppleEShopContext _context;

        public CategoryRepository(AppleEShopContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categories = _context.Category.ToList();

            foreach (var category in categories)
            {
                category.ProductCount = _context.Product.Count(p => p.CategoryId == category.Id);
            }

            return categories;
        }
    }
}
