using System;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Collections.Generic;

namespace TeamToDo.PCL
{
	/// <summary>
	/// Manage to do tasks.
	/// </summary>
	public class ToDoTaskManager
	{
		private TaskService taskService;

		public ToDoTaskManager (Session userSession)
		{
			this.taskService = new TaskService ();
			this.taskService.UserSession = userSession;
		}

		/// <summary>
		/// Allow to set the task service provider. Mainly for testing
		/// </summary>
		/// <value>The task service.</value>
		public TaskService TaskService {
			set {
				this.taskService = value;
			}
		}

		/// <summary>
		/// Create a new todo task
		/// </summary>
		/// <param name="task">the task to be created</param>
		public async Task CreateTask(ToDoTask task)
		{
			Contract.Assert (null != task, "Task is null");
			await this.taskService.CreateTask (task);
		}

		/// <summary>
		/// Update an existing todo task
		/// </summary>
		/// <param name="task">The task to be updated</param>
		public async Task UpdateTask(ToDoTask task)
		{
			Contract.Assert (null != task, "Task is null");
			await this.taskService.UpdateTask (task);
		}

		/// <summary>
		/// Mark an existing task to be completed
		/// </summary>
		/// <param name="task">The task to be completed</param>
		public async Task CompleteTask(ToDoTask task)
		{
			Contract.Assert (null != task, "Task is null");
			await this.taskService.CompleteTask (task);
		}

		/// <summary>
		/// List the tasks that are assigned to the current user
		/// </summary>
		/// <returns>The tasks assigned to the current user</returns>
		public async Task<List<ToDoTask>> ListTasks()
		{
			return await this.taskService.ListTasks ();
		}

	}
}

