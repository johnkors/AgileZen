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

            var service = new AgileZenService("a77ef979c1da4632a81b2ecfbaeac696");
            service.GetStories(projectId, (a) => RunOnUiThread(() =>
            {
                foreach (AgileZenStory project in a.Value.Items)
                {
                    stories.Add(project);
                }
            }));
        }
    }
}