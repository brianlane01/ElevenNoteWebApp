using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElvenNoteWepApp.Shared.Models.Note;

namespace Server.Services.NoteServices;

public interface INoteService
{
    Task<bool> CreateNoteAsync(NoteCreate model);
    Task<List<NoteListItem>> GetAllNotesAsync();
    Task<NoteDetail?> GetNoteByIdAsync(int noteId);
    Task<bool> UpdateNoteAsync(NoteEdit request);
    Task<bool> DeleteNoteAsync(int noteId);
    Task<bool> DeleteNoteAsync(string userId);
    Task<NoteDetail?> GetNoteByCategoryIdAsync(int categoryId);
    void SetUserId(string userId);
}
