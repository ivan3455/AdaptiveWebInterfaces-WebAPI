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
                new CategoryModel { CategoryId = 2, Name = "Filters" },
                new CategoryModel { CategoryId = 3, Name = "Suspension" },
                new CategoryModel { CategoryId = 4, Name = "Engine Parts" },
                new CategoryModel { CategoryId = 5, Name = "Electrical Components" },
                new CategoryModel { CategoryId = 6, Name = "Transmission" },
                new CategoryModel { CategoryId = 7, Name = "Exhaust System" },
                new CategoryModel { CategoryId = 8, Name = "Cooling System" },
                new CategoryModel { CategoryId = 9, Name = "Interior Accessories" },
                new CategoryModel { CategoryId = 10, Name = "Exterior Accessories" },
                new CategoryModel { CategoryId = 11, Name = "Wheels & Tires" },
                new CategoryModel { CategoryId = 12, Name = "Body Parts" }
            };
        }

        public async Task<ResponseModel<CategoryModel>> GetCategoryAsync(int categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category != null)
            {
                return new ResponseModel<CategoryModel>
                {
                    Data = category,
                    Success = true,
                    Message = "Category found."
                };
            }
            else
            {
                return new ResponseModel<CategoryModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Category not found."
                };
            }
        }

        public async Task<ResponseModel<IEnumerable<CategoryModel>>> GetAllCategoriesAsync()
        {
            return new ResponseModel<IEnumerable<CategoryModel>>
            {
                Data = _categories,
                Success = true,
                Message = "All categories retrieved."
            };
        }

        public async Task<ResponseModel<CategoryModel>> CreateCategoryAsync(CategoryModel category)
        {
            if (_categories.Any(c => c.CategoryId == category.CategoryId))
            {
                return new ResponseModel<CategoryModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Category with the same code already exists."
                };
            }

            _categories.Add(category);
            return new ResponseModel<CategoryModel>
            {
                Data = category,
                Success = true,
                Message = "Category added successfully."
            };
        }

        public async Task<ResponseModel<CategoryModel>> UpdateCategoryAsync(int categoryId, CategoryModel category)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (existingCategory == null)
            {
                return new ResponseModel<CategoryModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Category not found."
                };
            }

            existingCategory.Name = category.Name;

            return new ResponseModel<CategoryModel>
            {
                Data = existingCategory,
                Success = true,
                Message = "Category updated successfully."
            };
        }

        public async Task<ResponseModel<bool>> DeleteCategoryAsync(int categoryId)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (existingCategory == null)
            {
                return new ResponseModel<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Category not found."
                };
            }

            _categories.Remove(existingCategory);
            return new ResponseModel<bool>
            {
                Data = true,
                Success = true,
                Message = "Category deleted successfully."
            };
        }
    }
}
