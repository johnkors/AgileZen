using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing; // Rectangle
using AgileZen.Lib;

namespace Touch
{
	public partial class AppDelegate : UIApplicationDelegate
	{
		public static string APIKEY = "40092c42cfd64a309df016dc8afcf826";
		
		private LoginHandler _loginHandler;
		private LoginViewController _loginViewController;
		private UINavigationController _navigationController;
		private ProjectTableViewController _projectTableViewController;
		
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			_loginHandler = new LoginHandler();
			_loginHandler.OnSuccessfulLogin += AddNavigationController;
			
			if(_loginHandler.HasStoredApiKey())
			{
				AddNavigationController ();
			}
			else
			{
				ShowLoginView ();
			}
			
			
			window.MakeKeyAndVisible ();
			return true;
		}

		private void AddNavigationController ()
		{
			_navigationController = new UINavigationController();
			_navigationController.NavigationBar.BarStyle = UIBarStyle.Black;
			window.AddSubview(_navigationController.View);
			
			_projectTableViewController = new ProjectTableViewController();
			_navigationController.PushViewController(_projectTableViewController,false);
		}

		private void ShowLoginView ()
		{
			_loginViewController = new LoginViewController(_loginHandler);
			window.AddSubview(_loginViewController.View); 
		}
	}
	

	

}
