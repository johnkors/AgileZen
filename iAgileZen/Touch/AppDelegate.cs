using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing; // Rectangle

namespace Touch
{
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			var navigationController = new UINavigationController();
			navigationController.NavigationBar.BarStyle = UIBarStyle.Black;
			
			window.AddSubview(navigationController.View); // hver kontroller har en kobling mot ett view via property "View"
			window.MakeKeyAndVisible ();

			return true;
		}
	}
}
