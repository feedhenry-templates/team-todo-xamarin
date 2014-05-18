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
    [Activity(Label = "My Tasks")]			
    public class TaskListView : ListActivity
    {
        List<ToDoTask> tasks = new List<ToDoTask>();
        ToDoTaskManager taskManager = null;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            TeamToDo.PCL.UserManager um = TeamToDo.PCL.UserManager.GetInstance();
            taskManager = new ToDoTaskManager(um.GetCurrentSession());
            LoadTasks();
        }

        private async Task LoadTasks()
        {

            tasks = await taskManager.ListTasks();
            this.ListAdapter = new TaskListAdapter(this, tasks);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ToDoTask task = tasks[position];
            Intent detailItent = new Intent(this, typeof(TaskDetailsView));
            Bundle b = new Bundle();
            b.PutString("task", JsonConvert.SerializeObject(task));
            detailItent.PutExtras(b);
            StartActivity(detailItent);
        }

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


    }
}

