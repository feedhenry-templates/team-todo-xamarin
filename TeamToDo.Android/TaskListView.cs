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
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TeamToDo.Android
{
    /// <summary>
    /// The task list view activity
    /// </summary>
    [Activity(Label = "My Tasks")]			
    public class TaskListView : ListActivity
    {
        List<ToDoTask> tasks = new List<ToDoTask>();

        const int DETAIL_TASK = 0;
        const int CREATE_TASK = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            LoadTasks();
        }

        private async Task LoadTasks()
        {
            //list tasks
            tasks = await TodoApp.ListUserTasks();
            //populate the list view
            this.ListAdapter = new TaskListAdapter(this, tasks);
        }

        //show the task details when user click on a task
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ToDoTask task = tasks[position];
            Intent detailItent = new Intent(this, typeof(TaskDetailsView));
            Bundle b = new Bundle();
            b.PutString("task", JsonConvert.SerializeObject(task));
            detailItent.PutExtras(b);
            StartActivityForResult(detailItent, DETAIL_TASK);
        }

        //show the task creation view when user click on the "+" button
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater mi = this.MenuInflater;
            mi.Inflate(Android.Resource.Menu.list_actions, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem menuItem)
        {
            switch(menuItem.ItemId){
                case Android.Resource.Id.action_add:
                    CreateNewTask();
                    return true;
                default:
                    return base.OnOptionsItemSelected(menuItem);
            }
        }

        private void CreateNewTask()
        {
            Intent createTaskIntent = new Intent(this, typeof(TaskCreateView));
            StartActivityForResult(createTaskIntent, CREATE_TASK);
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(resultCode == Result.Ok)
            {
                ReloadTasks();
            }
        }

        private async Task ReloadTasks()
        {
            tasks = await TodoApp.ListUserTasks();
            TaskListAdapter adapter = (TaskListAdapter) this.ListAdapter;
            adapter.Reload(tasks);
            this.ListView.InvalidateViews();
            this.ListView.RefreshDrawableState();
        }

        public override void OnBackPressed()
        {
            AlertDialog.Builder ab = new AlertDialog.Builder(this);
            ab.SetCancelable(true);
            ab.SetTitle("Logout");
            ab.SetMessage("Are you sure to logout?");
            ab.SetPositiveButton(Resource.String.exit_app, OnExit);
            ab.Show();
        }

        private void OnExit(object IntentSender, DialogClickEventArgs e)
        {
            TodoApp.Logout();
            this.Finish();
        }
    }

    public class TaskListAdapter : BaseAdapter<ToDoTask>
    {
        List<ToDoTask> tasks = null;
        Activity context;

        public TaskListAdapter(Activity context, List<ToDoTask> tasks): base()
        {
            this.context = context;
            this.tasks = tasks;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override ToDoTask this[int position] {  
            get { return tasks[position]; }
        }

        public override int Count {
            get { return tasks.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) {
                // otherwise create a new one
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.TasklistItem, null);
            }
            view.FindViewById<TextView>(Android.Resource.Id.taskTitle).Text = tasks[position].Title;
            view.FindViewById<TextView>(Android.Resource.Id.taskDetails).Text = tasks[position].Description;
            return view;
        }

        public void Reload(List<ToDoTask> tasks)
        {
            this.tasks = tasks;
            this.NotifyDataSetChanged();
        }


    }
}

