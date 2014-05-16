// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace TeamToDo.IOS
{
	[Register ("TaskDetailsViewController")]
	partial class TaskDetailsViewController
	{
		[Outlet]
		MonoTouch.UIKit.UISwitch completed { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField deadlineField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView descField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField noteField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton saveBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView taskDetails { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField titleField { get; set; }

		[Action ("onTap:")]
		partial void onTap (MonoTouch.Foundation.NSObject sender);

		[Action ("saveTask:")]
		partial void saveTask (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (taskDetails != null) {
				taskDetails.Dispose ();
				taskDetails = null;
			}

			if (titleField != null) {
				titleField.Dispose ();
				titleField = null;
			}

			if (completed != null) {
				completed.Dispose ();
				completed = null;
			}

			if (saveBtn != null) {
				saveBtn.Dispose ();
				saveBtn = null;
			}

			if (descField != null) {
				descField.Dispose ();
				descField = null;
			}

			if (noteField != null) {
				noteField.Dispose ();
				noteField = null;
			}

			if (deadlineField != null) {
				deadlineField.Dispose ();
				deadlineField = null;
			}
		}
	}
}
