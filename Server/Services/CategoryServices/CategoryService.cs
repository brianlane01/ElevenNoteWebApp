using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElvenNoteWepApp.Server.Data;
using ElvenNoteWepApp.Server.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Category;

namespace Server.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _dbContext; 
    public CategoryService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CreateCategoryAsync(CategoryCreate model)
    {
        if (model == null)
            return false;

        var categoryEntity = new Category
        {
            Name = model.Name
        };

        _dbContext.Categories.Add(categoryEntity);
        return await _dbContext.SaveChangesAsync() == 1; 
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var entity = await _dbContext.Categories.FindAsync(categoryId);
        if(entity == null)
            return false;

        _dbContext.Categories.Remove(entity);
        return await _dbContext.SaveChangesAsync() == 1;
    }

    public async Task<IEnumerable<CategoryListItem>> GetAllCategoriesAsync()
    {
        var categoryQuery = _dbContext.Categories
            .Select(entity => new CategoryListItem
            {
                Id = entity.Id,
                Name = entity.Name,
            });

        return await categoryQuery.ToListAsync();
    }

    public async Task<CategoryDetail?> GetCategoryByIdAsync(int categoryId)
    {
        Category? entity = await _dbContext.Categories
                .FirstOrDefaultAsync(entity => entity.Id == categoryId);

            return entity is null ? null : new CategoryDetail
            {
                Id = entity.Id,
                Name = entity.Name
            };
    }

    public async Task<bool> UpdateCategoryAsync(CategoryEdit model)
    {
        if (model == null)
            return false;

        var entity = await _dbContext.Categories.FindAsync(model.Id);
        
        if(entity == null)
            return false;

        entity.Id = model.Id;
        entity.Name = model.Name;
        
        return await _dbContext.SaveChangesAsync() == 1;
    }
}
