using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Models.Category;

public class CategoryEdit
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } =string.Empty;
}
