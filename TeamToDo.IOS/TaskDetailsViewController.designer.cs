// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace TeamToDo.IOS
{
	[Register ("TaskDetailsViewController")]
	partial class TaskDetailsViewController
	{
		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		UITableView taskDetails { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (taskDetails != null) {
				taskDetails.Dispose ();
				taskDetails = null;
			}
		}
	}
}
