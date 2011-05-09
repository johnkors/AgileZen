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

            var service = new AgileZenService("a77ef979c1da4632a81b2ecfbaeac696");
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

