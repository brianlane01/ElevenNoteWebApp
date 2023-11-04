using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElvenNoteWepApp.Shared.Models.Note;

public class NoteDetail
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTimeOffset CreatedUtc { get; set; }
    public DateTimeOffset? ModifiedUtc { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}
