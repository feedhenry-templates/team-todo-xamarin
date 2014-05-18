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
using TeamToDo.PCL;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TeamToDo.Android
{
    [Activity(Label = "Details", ParentActivity = typeof(TaskListView))]			
    public class TaskDetailsView : Activity
    {
        private EditText titleField;
        private EditText descField;
        private EditText deadlineField;
        private EditText noteField;
        private Switch completeField;
        private Button saveButton;

        ToDoTask currentTask;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Android.Resource.Layout.TaskDetails);
            this.ActionBar.SetDisplayHomeAsUpEnabled(true);

            Bundle data = this.Intent.Extras;

            titleField = (EditText)FindViewById(Resource.Id.taskTitleField);
            descField = (EditText)FindViewById(Resource.Id.taskDescField);
            deadlineField = (EditText)FindViewById(Resource.Id.deadlineField);
            noteField = (EditText)FindViewById(Resource.Id.taskNoteField);
            completeField = (Switch)FindViewById(Resource.Id.taskCompletedField);
            saveButton = (Button)FindViewById(Resource.Id.saveTaskDetailsButton);

            string taskJson = data.GetString("task");
            currentTask = JsonConvert.DeserializeObject<ToDoTask>(taskJson);
            titleField.Text = currentTask.Title;
            descField.Text = currentTask.Description;
            deadlineField.Text = currentTask.Deadline.ToShortDateString();
            noteField.Text = currentTask.Note;
            completeField.Checked = false;

            saveButton.Click += delegate
            {
                currentTask.Title = titleField.Text;
                currentTask.Description = descField.Text;
                currentTask.Note = noteField.Text;
                if(completeField.Checked){
                    currentTask.CompletedOn = DateTime.Now;
                }
                UpdateTask();
            };
        }

        private async Task UpdateTask()
        {
            await TodoApp.UpdateTask(currentTask);
            this.Finish();
        }
    }
}

