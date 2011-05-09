using System;
using MonoTouch.UIKit;

namespace Touch
{
	public class MyNavController : UINavigationController
	{	
		private ProjectTableViewController _myTableViewController;
		public override void ViewDidLoad()
		{	
			NavigationBar.BarStyle = UIBarStyle.Black;
			_myTableViewController = new ProjectTableViewController();
			PushViewController(_myTableViewController,false);
		}
	}
}