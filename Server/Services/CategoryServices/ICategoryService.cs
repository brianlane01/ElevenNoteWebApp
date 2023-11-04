using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models.Category;

namespace Server.Services.CategoryServices;
public interface ICategoryService
{
    Task<IEnumerable<CategoryListItem>> GetAllCategoriesAsync();
    Task<bool> CreateCategoryAsync(CategoryCreate model);
    Task<CategoryDetail?> GetCategoryByIdAsync(int categoryId);
    Task<bool> UpdateCategoryAsync(CategoryEdit model);
    Task<bool> DeleteCategoryAsync(int categoryId);
}
