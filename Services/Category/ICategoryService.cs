using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Category
{
    public interface ICategoryService
    {
        Task<CategoryModel> GetCategoryAsync(int code);
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();
        Task<CategoryModel> CreateCategoryAsync(CategoryModel category);
        Task<CategoryModel> UpdateCategoryAsync(int code, CategoryModel category);
        Task<bool> DeleteCategoryAsync(int code);
    }
}
