using System;
using MonoTouch.UIKit;

namespace Touch
{
	public class RootViewController : UIViewController
	{
		private MyNavController _navController;
		
		public override void ViewDidLoad()
		{
			_navController = new MyNavController();
			View.AddSubview(_navController.View);
		}
	}
}

