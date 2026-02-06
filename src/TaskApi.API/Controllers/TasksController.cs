using Microsoft.AspNetCore.Mvc;

namespace TaskApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ILogger<TasksController> _logger;

    public TasksController(ILogger<TasksController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAllTasks()
    {
        // TODO: Implement task retrieval logic
        return Ok(new { message = "Task Management API is running" });
    }
}
