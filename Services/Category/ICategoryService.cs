using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Category
{
    public interface ICategoryService
    {
        Task<ResponseModel<CategoryModel>> GetCategoryAsync(int code);
        Task<ResponseModel<IEnumerable<CategoryModel>>> GetAllCategoriesAsync();
        Task<ResponseModel<CategoryModel>> CreateCategoryAsync(CategoryModel category);
        Task<ResponseModel<CategoryModel>> UpdateCategoryAsync(int code, CategoryModel category);
        Task<ResponseModel<bool>> DeleteCategoryAsync(int code);
    }
}
