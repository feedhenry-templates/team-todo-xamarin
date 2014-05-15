using System;

namespace TeamToDo.PCL
{
	/// <summary>
	/// Represents a user session
	/// </summary>
	public class Session
	{
		public Session (string sessionId)
		{
			this.SessionId = sessionId;
		}

		/// <summary>
		/// The session id
		/// </summary>
		/// <value>The session id returned by login response</value>
		public string SessionId { set; get;}
	}
}

