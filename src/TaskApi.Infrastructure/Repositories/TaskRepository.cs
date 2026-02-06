using Microsoft.EntityFrameworkCore;
using TaskApi.Domain.Entities;
using TaskApi.Domain.Interfaces;
using TaskApi.Infrastructure.Data;

namespace TaskApi.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetCompletedTasksAsync()
    {
        return await _context.Tasks
            .Where(t => t.IsCompleted)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetPendingTasksAsync()
    {
        return await _context.Tasks
            .Where(t => !t.IsCompleted)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<TaskItem> AddAsync(TaskItem entity)
    {
        _context.Tasks.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(TaskItem entity)
    {
        _context.Tasks.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
