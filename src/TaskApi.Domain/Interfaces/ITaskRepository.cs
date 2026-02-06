using TaskApi.Domain.Entities;

namespace TaskApi.Domain.Interfaces;

public interface ITaskRepository : IRepository<TaskItem>
{
    Task<IEnumerable<TaskItem>> GetCompletedTasksAsync();
    Task<IEnumerable<TaskItem>> GetPendingTasksAsync();
}
