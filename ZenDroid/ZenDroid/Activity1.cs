using System.Collections.ObjectModel;
using AgileZen.Lib;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;


namespace ZenDroid
{
    [Activity(Label = "ZenDroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : ListActivity
    {
        private ObservableCollection<AgileZenProject> projects;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            projects = new ObservableCollection<AgileZenProject>();
            ListAdapter = new Adapters.ObservableAdapter<AgileZenProject>(this, projects);

            var service = new AgileZenService("91470a1b1c794efbbe8db073f252cd4b");
            service.GetProjects((a) => RunOnUiThread(() =>
                {
                    foreach (AgileZenProject project in a.Value.Items)
                    {
                        projects.Add(project);
                    }
                }));

            ListView.ItemClick += ListView_ItemClick;
        }

        void ListView_ItemClick(object sender, ItemEventArgs e)
        {
            var intent = new Intent(this, typeof (StoriesActivity));
            intent.PutExtra("projectId", projects[e.Position].Id);
            StartActivity(intent);
        }
    }
}

