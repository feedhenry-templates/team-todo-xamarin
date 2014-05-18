using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TeamToDo.PCL
{
    public class TodoApp
    {
        public static async Task Login(string username, string password)
        {
            UserManager um = UserManager.GetInstance();
            await um.Login(username, password);
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
    }
}

