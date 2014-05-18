﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using FHSDK.Droid;
using FHSDK.Services;
using TeamToDo.PCL;
using Android.OS;

namespace TeamToDo.Android
{
    [Activity(Label = "TeamToDo.Android", MainLauncher = true)]
    public class LoginActivity : Activity
    {
       
        EditText usernameField = null;
        EditText passwordField = null;
        Button loginButton = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            usernameField = FindViewById<EditText>(Resource.Id.usernameField);
            passwordField = FindViewById<EditText>(Resource.Id.passwordField);

            loginButton = FindViewById<Button>(Resource.Id.loginButton);

            InitFH();
			

        }

        private async Task InitFH()
        {
            FHClient.SetLogLevel((int)LogService.LogLevels.DEBUG);
            await FHClient.Init();
            loginButton.Enabled = true;
            loginButton.Click += delegate
            {
                string username = usernameField.Text;
                string password = passwordField.Text;
                if(username.Length == 0 || password.Length == 0){
                    Utils.ShowAlert(this, "Error", "Please enter your username and password");
                } else {
                    DoLogin(username, password);
                }
            };
        }

        private async Task DoLogin(string username, string password)
        {
            ProgressDialog dialog = null;
            try{
                TeamToDo.PCL.UserManager userManager = TeamToDo.PCL.UserManager.GetInstance();
                dialog = Utils.ShowLoading(this, "Logging in...", "Please wait");
                await userManager.Login(username, password);
                Utils.StopLoading(dialog);
                StartActivity(new Intent(this, typeof(TaskListView)));
            } catch (Exception e)
            {
                if(null != dialog){
                    Utils.StopLoading(dialog);
                }
                Utils.ShowAlert(this, "Error", "Login Failed");
            }
           
        }


    }
}


