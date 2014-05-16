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
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton loginBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField passwordField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField usernameField { get; set; }

		[Action ("LoginTouched:")]
		partial void LoginTouched (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (loginBtn != null) {
				loginBtn.Dispose ();
				loginBtn = null;
			}

			if (passwordField != null) {
				passwordField.Dispose ();
				passwordField = null;
			}

			if (usernameField != null) {
				usernameField.Dispose ();
				usernameField = null;
			}
		}
	}
}
