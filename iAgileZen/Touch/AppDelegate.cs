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
		public static string APIKEY = "40092c42cfd64a309df016dc8afcf826";
		
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
