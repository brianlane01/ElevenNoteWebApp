using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElvenNoteWepApp.Server.Data;
using ElvenNoteWepApp.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ElvenNoteWepApp.Shared.Models.Note;

namespace Server.Services.NoteServices;

public class NoteService : INoteService
{
    private readonly ApplicationDbContext _dbContext;
    private string _userId;
    public NoteService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CreateNoteAsync(NoteCreate model)
    {
        Note entity = new()
        {
            Title = model.Title,
            Content = model.Content,
            OwnerId = _userId,
            CreatedUtc = DateTimeOffset.Now,
            CategoryId = model.CategoryId
        };

        _dbContext.Notes.Add(entity);
        var numberOfChanges = await _dbContext.SaveChangesAsync();

        return numberOfChanges == 1;
    }

    public async Task<bool> DeleteNoteAsync(int noteId)
    {
        var entity = await _dbContext.Notes.FindAsync(noteId);
        if(entity?.OwnerId != _userId)
            return false;

        _dbContext.Notes.Remove(entity);
        return await _dbContext.SaveChangesAsync() == 1;
    }

    public Task<bool> DeleteNoteAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<NoteListItem>> GetAllNotesAsync()
    {
        var noteQuery = _dbContext
            .Notes
            .Where(n => n.OwnerId == _userId)
            .Select(n =>
                new NoteListItem
                {
                    Id = n.Id,
                    Title = n.Title,
                    CategoryName = n.Category!.Name,
                    CreatedUtc = n.CreatedUtc
                });

        return await noteQuery.ToListAsync();
    }

    public async Task<NoteDetail?> GetNoteByIdAsync(int noteId)
    {
        Note? entity = await _dbContext.Notes
                .Include(nameof(Category))
                .FirstOrDefaultAsync(e => e.Id == noteId && e.OwnerId == _userId);

            return entity is null ? null : new NoteDetail
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                CreatedUtc = entity.CreatedUtc,
                ModifiedUtc = entity.ModifiedUtc,
                CategoryName = entity.Category!.Name,
                CategoryId = entity.Category.Id
            };
    }

    public async Task<NoteDetail?> GetNoteByCategoryIdAsync(int categoryId)
    {
        Note? entity = await _dbContext.Notes
                .Include(nameof(Category))
                .FirstOrDefaultAsync(e => e.CategoryId == categoryId && e.OwnerId == _userId);

            return entity is null ? null : new NoteDetail
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                CreatedUtc = entity.CreatedUtc,
                ModifiedUtc = entity.ModifiedUtc,
                CategoryName = entity.Category!.Name,
                CategoryId = entity.Category.Id
            };
    }

    public async Task<bool> UpdateNoteAsync(NoteEdit request)
    {
        if (request == null)
            return false;

        var entity = await _dbContext.Notes.FindAsync(request.Id);
        
        if(entity?.OwnerId != _userId)
            return false;

        entity.Title = request.Title;
        entity.Content = request.Content;
        entity.CategoryId = request.CategoryId;
        entity.ModifiedUtc = DateTimeOffset.Now;

        return await _dbContext.SaveChangesAsync() == 1;

    }

    public void SetUserId(string userId) => _userId = userId;
}
