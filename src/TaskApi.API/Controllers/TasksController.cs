using Microsoft.AspNetCore.Mvc;
using TaskApi.Application.DTOs;
using TaskApi.Application.Interfaces;

namespace TaskApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITaskService taskService, ILogger<TasksController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    /// <summary>
    /// Get all tasks
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    /// <summary>
    /// Get a specific task by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetTaskById(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
            return NotFound(new { message = $"Task with ID {id} not found" });

        return Ok(task);
    }

    /// <summary>
    /// Get all completed tasks
    /// </summary>
    [HttpGet("completed")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetCompletedTasks()
    {
        var tasks = await _taskService.GetCompletedTasksAsync();
        return Ok(tasks);
    }

    /// <summary>
    /// Get all pending tasks
    /// </summary>
    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetPendingTasks()
    {
        var tasks = await _taskService.GetPendingTasksAsync();
        return Ok(tasks);
    }

    /// <summary>
    /// Create a new task
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var task = await _taskService.CreateTaskAsync(createTaskDto);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    /// <summary>
    /// Update an existing task
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskDto>> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var task = await _taskService.UpdateTaskAsync(id, updateTaskDto);
        if (task == null)
            return NotFound(new { message = $"Task with ID {id} not found" });

        return Ok(task);
    }

    /// <summary>
    /// Delete a task
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var result = await _taskService.DeleteTaskAsync(id);
        if (!result)
            return NotFound(new { message = $"Task with ID {id} not found" });

        return NoContent();
    }
}
