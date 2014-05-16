using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using TeamToDo.PCL;

namespace TeamToDo.IOS
{
    public class TodoTaskListViewSource : UITableViewSource
    {
        private List<ToDoTask> tasks = null;
        private string cellIden = "TaskCell";


        public TodoTaskListViewSource(List<ToDoTask> theTasks)
        {
            this.tasks = theTasks;
        }

        public override int NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override int RowsInSection(UITableView tableview, int section)
        {
            return this.tasks.Count;
        }

        public override string TitleForHeader(UITableView tableView, int section)
        {
            return "My Tasks";
        }

        public override string TitleForFooter(UITableView tableView, int section)
        {
            return "";
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            ToDoTask task = this.tasks[indexPath.Row];
            UITableViewCell cell = tableView.DequeueReusableCell(cellIden);
            cell.TextLabel.Text = task.Title;
            cell.DetailTextLabel.Text = task.Description;
			
            return cell;
        }
    }
}

