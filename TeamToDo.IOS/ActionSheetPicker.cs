using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TeamToDo.PCL;

namespace ActionSheetPicker {
    public delegate void DoneEventHandler(object sender, EventArgs e);

    public class ActionSheetPickerBase {
        #region -= declarations =-

        protected UIActionSheet actionSheet;
        UIButton doneButton = UIButton.FromType (UIButtonType.RoundedRect);
        UIView owner;
        UILabel titleLabel = new UILabel ();
        protected UIView pickerView;

        #endregion

        #region -= properties =-



        public event DoneEventHandler OnComplete;

        protected virtual void OnDone(EventArgs e) 
        {
            if (OnComplete != null)
                OnComplete(this, e);
        }

        /// <summary>
        /// The title that shows up for the date picker
        /// </summary>
        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value; }
        }

        /// <summary>
        /// Set any datepicker properties here
        /// </summary>
        public UIView picker
        {
            get { return pickerView; }
            set { pickerView = value; }
        }


        #endregion

        #region -= constructor =-

        /// <summary>
        /// 
        /// </summary>
        public ActionSheetPickerBase (UIView owner)
        {
            // save our uiview owner
            this.owner = owner;

            // configure the title label
            titleLabel.BackgroundColor = UIColor.Clear;
            titleLabel.TextColor = UIColor.LightTextColor;
            titleLabel.Font = UIFont.BoldSystemFontOfSize (18);

            // configure the done button
            doneButton.SetTitle ("done", UIControlState.Normal);
            doneButton.TouchUpInside += (s, e) => { 
                actionSheet.DismissWithClickedButtonIndex (0, true); 
                OnDone(EventArgs.Empty);
            };

            // create + configure the action sheet
            actionSheet = new UIActionSheet () { Style = UIActionSheetStyle.BlackTranslucent };
            actionSheet.Clicked += (s, e) => { Console.WriteLine ("Clicked on item {0}", e.ButtonIndex); };

            // add our controls to the action sheet
            actionSheet.AddSubview (titleLabel);
            actionSheet.AddSubview (doneButton);
        }

        #endregion

        #region -= public methods =-

        /// <summary>
        /// Shows the action sheet picker from the view that was set as the owner.
        /// </summary>
        public void Show ()
        {
            // declare vars
            float titleBarHeight = 40;
            SizeF doneButtonSize = new SizeF (71, 30);
            SizeF actionSheetSize = new SizeF (owner.Frame.Width, picker.Frame.Height + titleBarHeight);
            RectangleF actionSheetFrame = new RectangleF (0, owner.Frame.Height - actionSheetSize.Height
                , actionSheetSize.Width, actionSheetSize.Height);

            // show the action sheet and add the controls to it
            actionSheet.ShowInView (owner);

            // resize the action sheet to fit our other stuff
            actionSheet.Frame = actionSheetFrame;

            // move our picker to be at the bottom of the actionsheet (view coords are relative to the action sheet)
            picker.Frame = new RectangleF 
                (picker.Frame.X, titleBarHeight, picker.Frame.Width, picker.Frame.Height);

            // move our label to the top of the action sheet
            titleLabel.Frame = new RectangleF (10, 4, owner.Frame.Width - 100, 35);

            // move our button
            doneButton.Frame = new RectangleF (actionSheetSize.Width - doneButtonSize.Width - 10, 7, doneButtonSize.Width, doneButtonSize.Height);
        }

        /// <summary>
        /// Dismisses the action sheet date picker
        /// </summary>
        public void Hide (bool animated)
        {
            actionSheet.DismissWithClickedButtonIndex (0, animated);
        }

        #endregion      

    }


    public class ActionSheetDatePicker : ActionSheetPickerBase {
		
		
		
		#region -= constructor =-
		
		/// <summary>
		/// 
		/// </summary>
        public ActionSheetDatePicker (UIView owner)
            :base(owner)
		{
            this.picker = new UIDatePicker(RectangleF.Empty);
            actionSheet.AddSubview(picker);
		}

        public UIDatePicker DatePicker {
            get {
                return (UIDatePicker)this.picker;
            }
        }
		
		#endregion
	}

    public class ActionSheetUserPicker : ActionSheetPickerBase {

        private List<User> users;

        public ActionSheetUserPicker (UIView owner, List<User> users): base(owner)
        {
            this.users = users;
            UIPickerView userpicker = new UIPickerView();
            userpicker.Model = new UserPickerViewModel(users);
            this.picker = userpicker;
            actionSheet.AddSubview(picker);
        }

        public UIPickerView UserPicker {
            get {
                return (UIPickerView)this.picker;
            }
        }




    }

    public class UserPickerViewModel : UIPickerViewModel
    {
        private List<User> users;
        private User currentSelected;

        public UserPickerViewModel(List<User> users){
            this.users = users;
        }

        public override int GetComponentCount (UIPickerView picker)
        {
            return 1;
        }

        public override int GetRowsInComponent (UIPickerView picker, int component)
        {
            return this.users.Count;
        }

        public override string GetTitle (UIPickerView picker, int row, int component)
        {

            return this.users[row].Username;
        }


        public override void Selected (UIPickerView picker, int row, int component)
        {
            currentSelected = this.users[row];
        }

        public User GetSelected()
        {
            if(null != currentSelected){
                return currentSelected;
            } else {
                return this.users[0];
            }

        }

    }


}

