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
		private UINavigationController _navigationController;
		
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			_navigationController = new MyNavController();
			window.AddSubview(_navigationController.View); 
			window.MakeKeyAndVisible ();
	
			return true;
		}
	}
	

	

}
