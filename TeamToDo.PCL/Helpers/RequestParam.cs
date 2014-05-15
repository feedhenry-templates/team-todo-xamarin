using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TeamToDo.PCL
{
	/// <summary>
	/// A helper class to easily construct requqest parameters
	/// </summary>
	public class RequestParam
	{
		private Dictionary<string, string> header = new Dictionary<string, string>();
		private Dictionary<string, object> payload = new Dictionary<string, object>();

		public RequestParam ()
		{
		}

		[JsonProperty("header")]
		public Dictionary<string, string> Header
		{
			get {
				return header;
			}
		}

		[JsonProperty("payload")]
		public Dictionary<string, object> Payload
		{
			get {
				return payload;
			}
		}

		public void AddHeader(string name, string value)
		{
			header.Add (name, value);
		}

		public void AddPayload(string key, object value)
		{
			payload.Add (key, value);
		}

		public Dictionary<string, object> ToDictionary()
		{
			Dictionary<string, object> dict = new Dictionary<string, object> ();
			dict.Add ("request", this);
			return dict;
		}
	}
}

