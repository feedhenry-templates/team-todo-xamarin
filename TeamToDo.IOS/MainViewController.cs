using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using FHSDK.Touch;
using FHSDK.Services;
using TeamToDo.PCL;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TeamToDo.IOS
{
    /// <summary>
    /// The login view for the ios app
    /// </summary>
    public partial class MainViewController : UIViewController
    {
        public MainViewController(IntPtr handle) : base(handle)
        {
            // Custom initialization
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.loginBtn.Enabled = false;
            InitFH();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion

        /// <summary>
        /// Init the FHClient
        /// </summary>
        /// <returns>The F.</returns>
        private async Task InitFH()
        {
            FHClient.SetLogLevel((int)LogService.LogLevels.DEBUG);
            await FHClient.Init();
            this.loginBtn.Enabled = true;
        }

        partial void LoginTouched (UIButton sender)
        {
            LoginTouchedAsync(sender);
        }

        public async Task LoginTouchedAsync (UIButton sender)
        {
            string userName = this.usernameField.Text;
            string password = this.passwordField.Text;
            if(userName.Length == 0 || password.Length == 0){
                ShowAlert("Error", "Please enter your username and password");
            }
            try{
                //add activity spinner
                UIActivityIndicatorView activityView = new UIActivityIndicatorView();
                activityView.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
                activityView.Center = this.View.Center;
                this.View.AddSubview(activityView);
                activityView.StartAnimating();

                //call login
                await TodoApp.Login(userName, password);

                //login success, continue
                activityView.StopAnimating();
                activityView.RemoveFromSuperview();

                UIStoryboard sb = UIStoryboard.FromName("MainStoryboard", null);
                UINavigationController listView = (UINavigationController)sb.InstantiateViewController("tasklistViewController");
                this.PresentViewController(listView, true, null);


            }catch(Exception e){
                //login failed, show the error
                ShowAlert("Error", "Login Failed");
            }
        }

        private void ShowAlert(string title, string message)
        {
            UIAlertView alert = new UIAlertView()
            {
                Title = title, Message = message
            };
            alert.AddButton("OK");
            alert.Show();
        }
    }
}

