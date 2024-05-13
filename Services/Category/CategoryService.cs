using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly List<CategoryModel> _categories;

        public CategoryService()
        {
            _categories = new List<CategoryModel>
        {
            new CategoryModel { CategoryId = 1, Name = "Brakes" },
            new CategoryModel { CategoryId = 2, Name = "Filters" }
        };
        }

        public async Task<CategoryModel> GetCategoryAsync(int categoryId)
        {
            return await Task.FromResult(_categories.FirstOrDefault(c => c.CategoryId == categoryId));
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            return await Task.FromResult(_categories);
        }

        public async Task<CategoryModel> CreateCategoryAsync(CategoryModel category)
        {
            if (_categories.Any(c => c.CategoryId == category.CategoryId))
            {
                throw new Exception("Category with the same code already exists.");
            }

            _categories.Add(category);
            return await Task.FromResult(category);
        }

        public async Task<CategoryModel> UpdateCategoryAsync(int categoryId, CategoryModel category)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (existingCategory == null)
            {
                throw new Exception("Category not found.");
            }

            existingCategory.Name = category.Name;

            return await Task.FromResult(existingCategory);
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (existingCategory == null)
            {
                throw new Exception("Category not found.");
            }

            _categories.Remove(existingCategory);
            return await Task.FromResult(true);
        }
    }

}
