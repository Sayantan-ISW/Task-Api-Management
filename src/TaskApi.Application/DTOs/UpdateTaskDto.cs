using System.ComponentModel.DataAnnotations;

namespace TaskApi.Application.DTOs;

public class UpdateTaskDto
{
    [StringLength(200, MinimumLength = 1)]
    public string? Title { get; set; }
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    public bool? IsCompleted { get; set; }
}
