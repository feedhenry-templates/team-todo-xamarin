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
	[Register ("TaskCreateViewController")]
	partial class TaskCreateViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton deadlineBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView descField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton saveBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField titleField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton userBtn { get; set; }

		[Action ("createTask:")]
		partial void createTask (MonoTouch.Foundation.NSObject sender);

		[Action ("onTap:")]
		partial void onTap (MonoTouch.Foundation.NSObject sender);

		[Action ("pickDate:")]
		partial void pickDate (MonoTouch.Foundation.NSObject sender);

		[Action ("pickDeadline:")]
		partial void pickDeadline (MonoTouch.Foundation.NSObject sender);

		[Action ("pickUser:")]
		partial void pickUser (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (deadlineBtn != null) {
				deadlineBtn.Dispose ();
				deadlineBtn = null;
			}

			if (descField != null) {
				descField.Dispose ();
				descField = null;
			}

			if (saveBtn != null) {
				saveBtn.Dispose ();
				saveBtn = null;
			}

			if (titleField != null) {
				titleField.Dispose ();
				titleField = null;
			}

			if (userBtn != null) {
				userBtn.Dispose ();
				userBtn = null;
			}
		}
	}
}
