using System;
using MonoTouch.UIKit;

namespace Touch
{
	public class MyNavController : UINavigationController
	{	
		private MyTableViewController _myTableViewController;
		public override void ViewDidLoad()
		{	
			NavigationBar.BarStyle = UIBarStyle.Black;
			_myTableViewController = new MyTableViewController();
			PushViewController(_myTableViewController,false);
		}
	}
}