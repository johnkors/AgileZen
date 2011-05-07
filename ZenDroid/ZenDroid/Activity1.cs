using System.Collections.ObjectModel;
using AgileZen.Lib;
using Android.App;
using Android.Widget;
using Android.OS;


namespace ZenDroid
{
    [Activity(Label = "ZenDroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var projects = new ObservableCollection<AgileZenProject>();
            ListAdapter = new Adapters.ObservableAdapter<AgileZenProject>(this, projects);

            var service = new AgileZenService("91470a1b1c794efbbe8db073f252cd4b");
            service.GetProjects((a) => RunOnUiThread(() =>
                {
                    foreach (AgileZenProject project in a.Value.Items)
                    {
                        projects.Add(project);
                    }
                }));
        }
    }
}

