// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ActionSheetPicker;
using System.Collections.Generic;
using TeamToDo.PCL;

namespace TeamToDo.IOS
{
	partial class TaskCreateViewController : UITableViewController
	{
        ActionSheetDatePicker datePicker;
        ActionSheetUserPicker userPicker;
        private User selectedUser;
        private DateTime selectedTime;

		public TaskCreateViewController (IntPtr handle) : base (handle)
		{
		}

        public TodoTaskListViewController Delegate{ set; get;}

        public void SetTask(TodoTaskListViewController d)
        {
            Delegate = d;
        }

        partial void pickDate (MonoTouch.Foundation.NSObject sender)
        {
            datePicker = new ActionSheetDatePicker(this.View);
            datePicker.Title = "Choose Date";
            datePicker.DatePicker.Mode = UIDatePickerMode.Date;
            datePicker.DatePicker.MinimumDate = DateTime.Today;
            datePicker.DatePicker.MaximumDate = DateTime.Today.AddDays(7);
            datePicker.DatePicker.ValueChanged += (object s, EventArgs e) => {
                DateTime selected = (DateTime)(s as UIDatePicker).Date;
                this.selectedTime = selected;
                this.deadlineBtn.SetTitle(selected.ToShortDateString(), UIControlState.Normal);
            };
            datePicker.Show();
        }

        partial void pickUser (MonoTouch.Foundation.NSObject sender)
        {
            LoadUserPicker();
        }

        partial void onTap (MonoTouch.Foundation.NSObject sender)
        {
            TableView.EndEditing(true);
        }


        private async void LoadUserPicker()
        {
            UserManager um = UserManager.GetInstance();
            List<User> users = await um.ListUsers();

            userPicker = new ActionSheetUserPicker(this.View, users);

            userPicker.Title = "Choose User";
            userPicker.OnComplete += (s, e) =>
            {
                ActionSheetUserPicker picker = (ActionSheetUserPicker) s;
                UserPickerViewModel model = (UserPickerViewModel) picker.UserPicker.Model;
                User u = model.GetSelected();
                this.selectedUser = u;
                this.userBtn.SetTitle(u.Username, UIControlState.Normal);
            };
            userPicker.Show();
        }

        partial void createTask (MonoTouch.Foundation.NSObject sender)
        {
            ToDoTask task = new ToDoTask();
            task.Title = this.titleField.Text;
            task.Description = this.descField.Text;
            task.Deadline = this.selectedTime;
            task.AssignedTo = this.selectedUser;
            this.Delegate.CreateTask(task);
            this.NavigationController.PopViewControllerAnimated(true);
        }




	}


}
