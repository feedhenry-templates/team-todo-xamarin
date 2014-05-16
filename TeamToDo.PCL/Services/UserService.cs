using System;
using System.Collections.Generic;
using FHSDK;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TeamToDo.PCL
{
	/// <summary>
	/// User access service provider
	/// </summary>
	public class UserService
	{
		public UserService ()
		{
		}

		public async Task Login(User user)
		{
			Dictionary<string, object> loginParams = GetLoginParams(user.Username, user.Password);
			FHResponse loginRes = await FH.Cloud("/cloud/authenticateAction", "POST", null, loginParams);
			if (loginRes.Error == null) {
				JObject resJson = loginRes.GetResponseAsJObject ();
				string sessionId = (string)resJson["response"]["header"]["sessionId"];
				user.Session = new Session (sessionId);
			} else {
				throw loginRes.Error;
			}
		}

		public async Task Logout(User user)
		{
			RequestParam logoutParam = new RequestParam ();
			logoutParam.AddHeader ("sessionId", user.Session.SessionId);
			FHResponse logoutRes = await FH.Cloud ("/cloud/logoutAction", "POST", null, logoutParam.ToDictionary());
			if (logoutRes.Error == null) {
				user.Session = null;
			} else {
				throw logoutRes.Error;
			}
		}

		public async Task<List<User>> ListUsers(Session session)
		{
			RequestParam rp = new RequestParam ();
			rp.AddHeader ("sessionId", session.SessionId);
			FHResponse listRes = await FH.Cloud ("/cloud/fetchUserListAction", "POST", null, rp.ToDictionary ());
			if (null == listRes.Error) {
				JObject resJson = listRes.GetResponseAsJObject ();
				List<User> users = JsonConvert.DeserializeObject<List<User>> ((string)resJson["response"] ["payload"] ["fetchUsers"] ["userList"]);
				return users;
			} else {
				throw listRes.Error;
			}
		}

		private Dictionary<string, object> GetLoginParams(string userName, string password)
		{
			RequestParam loginParams = new RequestParam ();
			loginParams.AddHeader ("appType", "Client");

			IDictionary<string, string> login = new Dictionary<string, string> ();
			login.Add ("userName", userName);
			login.Add ("password", password);

			loginParams.AddPayload ("login", login);

			return loginParams.ToDictionary();
		}


	}
}

