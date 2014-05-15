using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TeamToDo.PCL
{
	/// <summary>
	/// Represents a user
	/// </summary>
	public class User
	{
		public User ()
		{
		}
			
		[JsonProperty("userName")]
		public string Username {get; set;}

		[JsonIgnore]
		public string Password { get; set;}

		[JsonProperty("userId")]
		public string Id { get; set;}

		[JsonIgnore]
		public Session Session { get; set; }
	}
}

