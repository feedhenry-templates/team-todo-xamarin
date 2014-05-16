using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using TeamToDo.PCL;
using System.Diagnostics;

namespace TeamToDo.IOS
{
	partial class TaskDetailsViewController : UITableViewController
	{
		public TaskDetailsViewController (IntPtr handle) : base (handle)
		{
		}

        ToDoTask currentTask{ set; get;}
        string Action { set; get;}

        public TodoTaskListViewController Delegate{ set; get;}

        public void SetTask(TodoTaskListViewController d, ToDoTask task)
        {
            Delegate = d;
            currentTask = task;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.titleField.Text = currentTask.Title;
            this.descField.Text = currentTask.Description;
            this.noteField.Text = currentTask.Note;
            this.deadlineField.Text = currentTask.Deadline.ToShortDateString();
        }

        partial void saveTask (MonoTouch.Foundation.NSObject sender)
        {
            currentTask.Title = this.titleField.Text;
            currentTask.Description = this.descField.Text;
            currentTask.Note = this.noteField.Text;
            Debug.WriteLine("Task saved");
            if(this.completed.On){
                currentTask.CompletedOn = new DateTime();
            }
            this.Delegate.SaveTask(currentTask, this.completed.On);
            this.NavigationController.PopViewControllerAnimated(true);
        }

        partial void onTap (MonoTouch.Foundation.NSObject sender)
        {
            TableView.EndEditing(true);
        }



	}
}
