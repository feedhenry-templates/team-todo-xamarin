using System;
using Android.App;
using Android.Content;

namespace TeamToDo.Android
{
    public class Utils
    {
        public static void ShowAlert(Context appContext, string title, string message)
        {
            AlertDialog.Builder ab = new AlertDialog.Builder(appContext);
            ab.SetTitle(title);
            ab.SetMessage(message);
            ab.SetCancelable(true);
            ab.Show();
        }

        public static ProgressDialog ShowLoading(Context appContext, string title, string message)
        {
            ProgressDialog dialog = ProgressDialog.Show(appContext, title, message, true);
            return dialog;
        }

        public static void StopLoading(ProgressDialog loadingDialog)
        {
            loadingDialog.Dismiss();
        }



    }
}

