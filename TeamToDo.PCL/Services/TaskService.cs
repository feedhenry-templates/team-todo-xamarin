using System;
using System.Threading.Tasks;
using FHSDK;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

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
			List<ToDoTask> tasks = JsonConvert.DeserializeObject<List<ToDoTask>> ((string)resJson ["payload"] ["fetchToDos"] ["toDoList"]);
			return tasks;
		}


		private async Task<FHResponse> CallService(string serviceName, string payloadKey, ToDoTask task)
		{
			RequestParam rp = new RequestParam ();
			rp.AddHeader ("sessionId", this.UserSession.SessionId);
			if (null != payloadKey && null != task) {
				rp.AddPayload (payloadKey, task);
			}

			FHResponse res = await FH.Cloud (serviceName, "POST", null, rp.ToDictionary ());
			if (null == res.Error) {
				return res;
			} else {
				throw res.Error;
			}
		}

	}
}

