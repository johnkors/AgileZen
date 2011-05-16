using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using AgileZen.Lib;
using escoz;

namespace Touch
{
	public partial class AppDelegate : UIApplicationDelegate
	{	
		private LoginHandler _loginHandler;
		private LoginViewController _loginViewController;
		private UINavigationController _navigationController;
		private MainMenuViewController _mainMenuViewController;
		private LoadingHUDView _hud;
		
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			_loginHandler = new LoginHandler();
			_loginHandler.OnLoginBegan += ShowProgressHud;
			_loginHandler.OnSuccessfulLogin += AddNavigationController;
			_loginHandler.OnLoginFinished += RemoveProgressHud;
			
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
			
			_mainMenuViewController = new MainMenuViewController();
			_navigationController.PushViewController(_mainMenuViewController,false);
		}

		private void ShowLoginView ()
		{
			_loginViewController = new LoginViewController(_loginHandler);
			window.AddSubview(_loginViewController.View); 
		}
		
		private void ShowProgressHud()
		{
			_hud = new LoadingHUDView("Contacting AgileZen", "Verifying API key");
			window.AddSubview(_hud);
			_hud.StartAnimating();
		}
		
		private void RemoveProgressHud()
		{
			_hud.StopAnimating();
		}
	}
	

	

}
