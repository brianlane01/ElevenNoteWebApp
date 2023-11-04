using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElvenNoteWepApp.Shared.Models.Note;

public class NoteListItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
    public DateTimeOffset CreatedUtc { get; set; } 
    public DateTimeOffset Modified { get; set; } 
}
