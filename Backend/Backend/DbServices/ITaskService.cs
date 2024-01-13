using Backend.Models;

namespace Backend.DbServices
{
    public interface ITaskService
    {
        Task<TaskModel> AddTask(TaskModel task);
        Task<List<TaskModel>> GetAllTasks();
        Task<TaskModel> GetTaskById(Guid taskId);
        Task<TaskModel> UpdateTask(TaskModel updatedTask);
        Task<TaskModel> DeleteTask(Guid taskId);
    }
}
