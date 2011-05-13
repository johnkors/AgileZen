using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Touch
{
	public class Projects 
	{	
		private UINavigationController _navController;
		private ProjectTableViewController _projectTableViewController;
		private UIImage _icon;
		
		public UIImage Icon 
		{
			get { return _icon; }
		}
		
		public Projects(UINavigationController navController)
		{
			_navController = navController;
			_projectTableViewController = new ProjectTableViewController();
			_icon = CreateIconImage();
		}
		
		public void PushProjectsViewController()
		{
			_navController.PushViewController(_projectTableViewController, true);
		}
		
		private UIImage CreateIconImage()
		{
			return UIImage.FromFile("Img/33-cabinet.png");
		}
	}
}

