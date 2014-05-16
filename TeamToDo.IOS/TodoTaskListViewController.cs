using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.Threading.Tasks;
using TeamToDo.PCL;
using System.Collections.Generic;
using System.Diagnostics;

namespace TeamToDo.IOS
{
	partial class TodoTaskListViewController : UITableViewController
	{

		public TodoTaskListViewController (IntPtr handle) : base (handle)
		{
		}

        private List<ToDoTask> tasks = null;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Debug.WriteLine("view loaded");
            LoadTasks();
        }

        private async Task LoadTasks()
        {
            Debug.WriteLine("Calling LoadTasks");
            UserManager um = UserManager.GetInstance();
            ToDoTaskManager taskManager = new ToDoTaskManager(um.GetCurrentSession());
            tasks = await taskManager.ListTasks();
            ShowTasks();
        }
            
        private void ShowTasks()
        {
            TodoTaskListViewSource taskSource = new TodoTaskListViewSource(tasks);
            TableView.Source = taskSource;
            TableView.ReloadData();
        }
	}
}
