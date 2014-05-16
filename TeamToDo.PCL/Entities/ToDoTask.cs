using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TeamToDo.PCL
{
	/// <summary>
	/// Represents a ToDoTask
	/// </summary>
	public class ToDoTask
	{
        private Location location = new Location();
		public ToDoTask ()
		{
		}

		[JsonProperty(PropertyName = "toDoId", NullValueHandling = NullValueHandling.Ignore)]
		public string Id{ get; set;}

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("description")]
		public string Description { get; set ;}

		[JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

		[JsonProperty("assignedTo")]
		public User AssignedTo { get; set;}

		[JsonProperty("longitude")]
		public Double Longtitude { get { return this.location.Longitude; } }

		[JsonProperty("latitude")]
		public Double Latitude { get { return this.location.Latitude; }}

		[JsonProperty("location")]
		public Location Location { set { this.location = value; }}

		[JsonProperty("status")]
		public string Status {get; set;}

		[JsonProperty("completedOn")]
		public DateTime CompletedOn {get; set;}

		[JsonProperty("note")]
		public string Note{get; set;}

	}

	public class Location {

		public Location()
		{
		}

		[JsonProperty("address")]
		public string Address {
			get;
			set;
		}

		[JsonProperty("longitude")]
		public Double Longitude {
			get;
			set;
		}

		[JsonProperty("latitude")]
		public Double Latitude {
			get;
			set;
		}
	}
}

