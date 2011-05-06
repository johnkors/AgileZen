using System.Collections.ObjectModel;
using AgileZen.Lib;
using Android.App;
using Android.OS;

namespace ZenDroid
{
    [Activity(Label = "Stories")]
    public class StoriesActivity : ListActivity
    {
        private ObservableCollection<AgileZenStory> stories;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var projectId = this.Intent.GetStringExtra("projectId");

            stories = new ObservableCollection<AgileZenStory>();
            ListAdapter = new Adapters.ObservableAdapter<AgileZenStory>(this, stories);

            var service = new AgileZenService("91470a1b1c794efbbe8db073f252cd4b");
            service.GetStories(projectId, (a) => RunOnUiThread(() =>
            {
                foreach (AgileZenStory project in a.Value)
                {
                    stories.Add(project);
                }
            }));
        }
    }
}