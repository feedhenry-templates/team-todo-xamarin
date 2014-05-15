using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace TeamToDo.IOS
{
	[Register ("DataViewController")]
	partial class DataViewController
	{
		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UILabel dataLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (dataLabel != null) {
				dataLabel.Dispose ();
				dataLabel = null;
			}
		}
	}
}

