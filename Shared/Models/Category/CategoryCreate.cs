using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElvenNoteWepApp.Shared.Models.Category
{
    public class CategoryCreate
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}