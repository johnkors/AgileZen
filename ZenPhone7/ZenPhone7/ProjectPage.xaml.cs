using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace ZenPhone7
{
    public partial class ProjectPage : PhoneApplicationPage
    {
        public ProjectPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var id = NavigationContext.QueryString["id"];
            StoryPivot.Title = NavigationContext.QueryString["name"];

            App.AgileZenService.GetPhasesWithStories(id, (result) => Dispatcher.BeginInvoke(() =>
            {
                StoryPivot.ItemsSource = result.Value.Items;
            }));
        }
    }
}