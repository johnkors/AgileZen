using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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

            App.AgileZenService.GetStories(id, (result) => Dispatcher.BeginInvoke(() =>
            {
                var groups = from items in result.Value.Items
                            group items by items.Phase.Name
                            into groupedItems
                            select new PivotItem
                            {
                                Header = groupedItems.Key,
                                Content = new ListBox
                                {
                                    ItemsSource = groupedItems
                                }
                            };

                foreach (var pivotItem in groups)
                {
                    StoryPivot.Items.Add(pivotItem);
                }
            }));
        }
    }
}