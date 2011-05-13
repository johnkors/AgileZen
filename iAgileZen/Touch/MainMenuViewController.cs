using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Touch
{
	public class MainMenuViewController : UIViewController
	{
		public Projects Projects {get;set;}
		public Settings Settings {get;set;}
				
		public override void ViewDidLoad()
		{
			Projects = new Projects(NavigationController);
			Settings = new Settings(NavigationController);
			
			var menu = new RootElement ("Overview")
			{
				new Section ("Select")
				{
					new StringElement ("Projects", Projects.PushProjectsViewController),
					new StringElement ("Settings", Settings.PushSettingsDialog)
				}
			};
			
			var dv = new DialogViewController (menu) {
				Autorotate = true
			};
			
			NavigationController.PushViewController (dv, false);
		}
	}
}

