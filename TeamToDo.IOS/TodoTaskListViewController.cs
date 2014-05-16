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
	partial class TodoTaskListViewController : UITableViewController
	{

		public TodoTaskListViewController (IntPtr handle) : base (handle)
		{
		}

        private List<ToDoTask> tasks = null;
        private ToDoTaskManager taskManager = null;

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
            taskManager = new ToDoTaskManager(um.GetCurrentSession());
            tasks = await taskManager.ListTasks();
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
                taskManager.CompleteTask(task);
            } else {
                taskManager.UpdateTask(task);
            }
        }

        public async Task CreateTask(ToDoTask task)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(task));
            taskManager.CreateTask(task);
            //tasks = await taskManager.ListTasks();
            //ShowTasks();

        }
	}
}
