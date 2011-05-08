using Android.App;
using Android.Widget;
using Android.OS;

namespace ZenDroid
{
    [Activity(Label = "ZenDroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            var service = new AgileZen.Lib.AgileZenService("91470a1b1c794efbbe8db073f252cd4b");
            service.GetStories("18031", (result) =>
                                {
                                    this.RunOnUiThread(() =>
                                                       {
                                                           button.Text = result.Value.Items.ToString();
                                                       });
                                });

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
    }
}

