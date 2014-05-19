using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.Threading.Tasks;
using TeamToDo.PCL;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TeamToDo.IOS
{
    /// <summary>
    /// The view controller for the task list
    /// </summary>
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
            //load tasks for the current user
            tasks = await TodoApp.ListUserTasks();
            //populate the list view
            ShowTasks();
        }
            
        private void ShowTasks()
        {
            TodoTaskListViewSource taskSource = new TodoTaskListViewSource(tasks);
            TableView.Source = taskSource;
            TableView.ReloadData();
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if(segue.Identifier == "detailsSegue"){
                var navctrl = segue.DestinationViewController as TaskDetailsViewController;
                if(navctrl != null){
                    var source = TableView.Source as TodoTaskListViewSource;
                    var rowPath = TableView.IndexPathForSelectedRow;
                    var item = source.GetItem(rowPath.Row);
                    navctrl.SetTask(this, item);
                }
            } else if(segue.Identifier == "createTaskSegue"){
                var navctrl = segue.DestinationViewController as TaskCreateViewController;
                if(navctrl != null){
                    navctrl.SetTask(this);
                }
            }
        }

        public void SaveTask(ToDoTask task, bool completed){
            if(completed){
                TodoApp.CompleteTask(task);
            } else {
                TodoApp.UpdateTask(task);
            }
        }

        public async Task CreateTask(ToDoTask task)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(task));
            TodoApp.CreateTask(task);
            //tasks = await taskManager.ListTasks();
            //ShowTasks();
        }
	}
}
