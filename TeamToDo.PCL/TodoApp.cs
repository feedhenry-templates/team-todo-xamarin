using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TeamToDo.PCL
{
    /// <summary>
    /// Contains the app's bussiness logic implementations.
    /// </summary>
    public class TodoApp
    {
        public static async Task Login(string username, string password)
        {
            UserManager um = UserManager.GetInstance();
            await um.Login(username, password);
        }

        public static async Task Logout()
        {
            UserManager um = UserManager.GetInstance();
            await um.Logout();
        }

        public static async Task<List<ToDoTask>> ListUserTasks()
        {
            UserManager um = UserManager.GetInstance();
            ToDoTaskManager tm = new ToDoTaskManager(um.GetCurrentSession());
            return await tm.ListTasks();
        }

        public static async Task UpdateTask(ToDoTask task){
            UserManager um = UserManager.GetInstance();
            ToDoTaskManager tm = new ToDoTaskManager(um.GetCurrentSession());
            await tm.UpdateTask(task);
        }

        public static async Task CompleteTask(ToDoTask task){
            UserManager um = UserManager.GetInstance();
            ToDoTaskManager tm = new ToDoTaskManager(um.GetCurrentSession());
            await tm.CompleteTask(task);
        }

        public static async Task<List<User>> ListUsers()
        {
            UserManager um = UserManager.GetInstance();
            return await um.ListUsers();
        }

        public static async Task CreateTask(ToDoTask task)
        {
            UserManager um = UserManager.GetInstance();
            ToDoTaskManager tm = new ToDoTaskManager(um.GetCurrentSession());
            await tm.CreateTask(task);
        }
    }
}

