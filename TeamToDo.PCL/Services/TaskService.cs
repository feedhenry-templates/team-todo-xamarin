using System;
using System.Threading.Tasks;
using FHSDK;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TeamToDo.PCL
{
	/// <summary>
	/// ToDoTask access service provider
	/// </summary>
	public class TaskService
	{
		public TaskService ()
		{
		}

		public Session UserSession { set; get;}


		public async Task CreateTask(ToDoTask task)
		{
			await this.CallService ("createToDoAction", "createToDo", task);
		}

		public async Task UpdateTask(ToDoTask task)
		{
			await this.CallService ("updateToDoAction", "updateToDo", task);
		}

		public async Task CompleteTask(ToDoTask task)
		{
			await this.CallService ("completeToDoAction", "completeToDo", task);
		}

		public async Task<List<ToDoTask>> ListTasks()
		{
			FHResponse listRes = await this.CallService ("fetchToDoAction", null, null);
			JObject resJson = listRes.GetResponseAsJObject ();

			JArray todolist = (JArray)resJson["response"]["payload"]["fetchToDos"]["toDoList"];
			List<ToDoTask> tasks = JsonConvert.DeserializeObject<List<ToDoTask>> (todolist.ToString());
			return tasks;
		}


		private async Task<FHResponse> CallService(string serviceName, string payloadKey, ToDoTask task)
		{
			RequestParam rp = new RequestParam ();
			rp.AddHeader ("sessionId", this.UserSession.SessionId);
			if (null != payloadKey && null != task) {
				rp.AddPayload (payloadKey, task);
			}

			FHResponse res = await FH.Cloud ("/cloud/" + serviceName, "POST", null, rp.ToDictionary ());
			if (null == res.Error) {
				return res;
			} else {
				throw res.Error;
			}
		}

	}
}

