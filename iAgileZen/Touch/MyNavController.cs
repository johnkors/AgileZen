using System;
using MonoTouch.UIKit;

namespace Touch
{
	public class MyNavController : UINavigationController
	{	
		private ProjectTableViewController _projectTableViewController;
		public override void ViewDidLoad()
		{	
			NavigationBar.BarStyle = UIBarStyle.Black;
			_projectTableViewController = new ProjectTableViewController();
			PushViewController(_projectTableViewController,false);
		}
	}
}