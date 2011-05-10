using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace ZenPhone7
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var service = new AgileZen.Lib.AgileZenService("");
            service.GetProjects((result) => Dispatcher.BeginInvoke(() =>
                                                                   {
                                                                       ProjectList.ItemsSource = result.Value.Items;
                                                                   }));
        }

        private void TextBlock_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var element = (TextBlock)sender;
            var project = (AgileZen.Lib.AgileZenProject) element.DataContext;
            NavigationService.Navigate(new Uri(string.Format("/ProjectPage.xaml?id={0}&name={1}", project.Id, project.Name), UriKind.Relative));
        }
    }
}