using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.Animations;
using System.Threading;
using System.Threading.Tasks;

namespace App2
{
    [Activity(Label = "Task App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string result = "";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button submit = FindViewById<Button>(Resource.Id.btnSubmit);
            EditText txtName = FindViewById<EditText>(Resource.Id.txtName);
            EditText txtLocation = FindViewById<EditText>(Resource.Id.txtLoc);
            TextView txtDisp = FindViewById<TextView>(Resource.Id.txtdisp);

            submit.Click += (object sender, EventArgs e) =>
            {
                Animation shake = AnimationUtils.LoadAnimation(this, Resource.Animation.shake);
                if (txtName.Text == "")
                {
                    txtName.StartAnimation(shake);
                    Toast.MakeText(this, "Enter Name", ToastLength.Short).Show();
                    txtName.SetBackgroundResource(Resource.Drawable.backtext);
                }

                if (txtLocation.Text == "")
                {
                    txtLocation.StartAnimation(shake);
                    Toast.MakeText(this, "Enter Location", ToastLength.Short).Show();
                    txtLocation.SetBackgroundResource(Resource.Drawable.backtext);
                }
                if (txtName.Text != "" && txtLocation.Text != "")
                {
                    ProgressDialog progress = ProgressDialog.Show(this, "Please wait", "Contacting Server", true, false);
                    try
                    {
                        Task.Run(() =>
                        {
                            result = App2.CallApi.Fetch(txtName.Text, txtLocation.Text);
                            RunOnUiThread(() => progress.Dismiss());
                            txtDisp.Text = result;
                        });
                    }
                    catch (Exception ex)
                    {
                        string x = ex.Message;
                    }
                }
            };
            txtName.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
                {
                    if (txtName.Text != "")
                        txtName.SetBackgroundColor(Android.Graphics.Color.White);
                };
            txtLocation.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                if (txtLocation.Text != "")
                    txtLocation.SetBackgroundColor(Android.Graphics.Color.White);
            };
            txtName.FocusChange += (object sender, Android.Views.View.FocusChangeEventArgs e) =>
            {
                Android.Views.InputMethods.InputMethodManager imm = (Android.Views.InputMethods.InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(txtName.WindowToken, 0);
            };
            txtLocation.FocusChange += (object sender, Android.Views.View.FocusChangeEventArgs e) =>
            {
                Android.Views.InputMethods.InputMethodManager imm = (Android.Views.InputMethods.InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(txtLocation.WindowToken, 0);
            };
            
        }
       
    }
}

