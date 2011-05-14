using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Touch
{
	public class MainMenuViewController : UIViewController
	{
		public ProjectsController Projects {get;set;}
		public SettingsController Settings {get;set;}
				
		public override void ViewDidLoad()
		{
			Projects = new ProjectsController(NavigationController);
			Settings = new SettingsController(NavigationController);
			
			
			var menu = new RootElement ("Overview")
			{
				new Section ("Select")
				{
					new ImageStringElement ("Projects", Projects.PushViewController, Projects.Icon),
					new ImageStringElement ("Settings", Settings.PushViewController, Settings.Icon)
				}
			};
			
			var dv = new DialogViewController (menu) {
				Autorotate = true
			};
			
			NavigationController.PushViewController (dv, false);
		}
	}
}

