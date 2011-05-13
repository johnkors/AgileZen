using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Touch
{
	public class Projects 
	{	
		private UINavigationController _navController;
		private ProjectTableViewController _projectTableViewController;
		
		public Projects(UINavigationController navController)
		{
			_navController = navController;
			_projectTableViewController = new ProjectTableViewController();
		}
		public void PushProjectsViewController()
		{
			_navController.PushViewController(_projectTableViewController, true);
		}
	}
}

