using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models.Note;

namespace Server.Services.NoteServices;

public interface INoteService
{
    Task<bool> CreateNoteAsync(NoteCreate model);
    Task<IEnumerable<NoteListItem>> GetAllNotesAsync();
    Task<NoteDetail?> GetNoteByIdAsync(int noteId);
    Task<bool> UpdateNoteAsync(NoteEdit request);
    Task<bool> DeleteNoteAsync(int noteId);
    Task<bool> DeleteNoteAsync(string userId);

    void SetUserId(string userId);
}
