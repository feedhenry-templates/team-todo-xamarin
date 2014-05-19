using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using TeamToDo.PCL;

namespace TeamToDo.Android
{
    /// <summary>
    /// Create task view activity
    /// </summary>
    [Activity(Label = "Create Task", ParentActivity = typeof(TaskListView))]			
    public class TaskCreateView : Activity
    {
        private EditText titleField;
        private EditText descField;
        private EditText deadlineField;
        private Spinner userSelector;
        private Button createTaskButton;

        private List<User> users;
        private User selectedUser;
        private DateTime deadline;

        const int DATE_DIALOG_ID = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.TaskCreate);

            titleField = (EditText) FindViewById(Resource.Id.taskTitleField);
            descField = (EditText)FindViewById(Resource.Id.taskDescField);
            deadlineField = (EditText)FindViewById(Resource.Id.deadlineField);
            userSelector = (Spinner)FindViewById(Resource.Id.userSelector);
            createTaskButton = (Button)FindViewById(Resource.Id.createTaskButton);

            deadlineField.Clickable = true;
            deadlineField.Focusable = false;

            deadlineField.Click += delegate
            {
                ShowDialog(DATE_DIALOG_ID);
            };

            createTaskButton.Click += delegate
            {
                CreateTask();
            };

            deadline = DateTime.Today;
            UpdateDeadline();
            LoadUsers();
        }

        private void UpdateDeadline()
        {
            deadlineField.Text = deadline.ToString("d");
        }

        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            this.deadline = e.Date;
            UpdateDeadline();
        }

        protected override Dialog OnCreateDialog (int id)
        {
            switch(id) {
                case DATE_DIALOG_ID:
                    return new DatePickerDialog(this, OnDateSet, deadline.Year, deadline.Month - 1, deadline.Day);
            }
            return null;
        }

        private async Task LoadUsers()
        {
            users = await TodoApp.ListUsers();
            List<string> usernames = new List<string>();
            foreach (User user in users)
            {
                usernames.Add(user.Username);
            }
            ArrayAdapter adapter = new ArrayAdapter<string>(this, global::Android.Resource.Layout.SimpleSpinnerItem, usernames);
            adapter.SetDropDownViewResource(global::Android.Resource.Layout.SimpleSpinnerDropDownItem);
            userSelector.Adapter = adapter;
            userSelector.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(user_ItemSelected);
        }

        private void user_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            User selected = users[e.Position];
            if(null != selected){
                selectedUser = selected;
            } else {
                selectedUser = users[0];
            }
        }

        private async Task CreateTask()
        {
            ToDoTask task = new ToDoTask();
            task.Title = titleField.Text;
            task.Description = descField.Text;
            task.Deadline = deadline;
            task.AssignedTo = selectedUser;
            await TodoApp.CreateTask(task);
            Toast.MakeText(this, "Task Created", ToastLength.Short).Show();
            this.SetResult(Result.Ok);
            this.Finish();
        }
    }
}

