using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.DbServices
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext db;

        public TaskService(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<TaskModel> AddTask(TaskModel task)
        {
            await db.Tasks.AddAsync(task);
            await db.SaveChangesAsync();

            return task;
        }

        public async Task<List<TaskModel>> GetAllTasks()
        {
            return await db.Tasks.ToListAsync();

        }

        public async Task<TaskModel> GetTaskById(Guid taskId)
        {
            var task = await db.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);
            return task;
        }

        public async Task<TaskModel> UpdateTask(TaskModel updatedTask)
        {
            var existingTask = await db.Tasks.FirstOrDefaultAsync(t => t.TaskId == updatedTask.TaskId);

            if (existingTask != null)
            {
                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.Deadline = updatedTask.Deadline;
                existingTask.Status = updatedTask.Status;
                existingTask.ProjectName = updatedTask.ProjectName;
                existingTask.AssignedTo = updatedTask.AssignedTo;

                await db.SaveChangesAsync();
            }

            return existingTask;
        }

        public async Task<TaskModel> DeleteTask(Guid taskId)
        {
            var taskToDelete = await db.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);

            if (taskToDelete != null)
            {
                db.Tasks.Remove(taskToDelete);
                await db.SaveChangesAsync();
            }

            return taskToDelete;
        }

    }
}
