using System;
using AgileZen.Lib;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Touch
{
	public class Settings
	{
		private UINavigationController _navController;
		private MonoObjectStore _objectStore;
		
		public Settings (UINavigationController navController)
		{
			_navController = navController;
			_objectStore = new MonoObjectStore();
		}
		
		public void PushSettingsDialog()
		{
			var root = CreateRoot ();
			var dv = new DialogViewController (root, true);
			_navController.PushViewController (dv, true);
		}
		
		private RootElement CreateRoot ()
		{
			var user = _objectStore.Load<AgileZenUser>("AgileZenUser.txt");
			
			var apiElement = new EntryElement ("API Key", "Enter API key", user.ApiKey);
			apiElement.Changed += HandleChangedApiKey;
					
			return new RootElement ("Settings") 
			{
				new Section ()
				{
					apiElement
				}
			};
		}
		
		private void HandleChangedApiKey(object sender, EventArgs e)
		{
			var user = new AgileZenUser();
			user.ApiKey = ((EntryElement) sender).Value;
			_objectStore.Save<AgileZenUser>(user,"AgileZenUser.txt");
			var alertView = new UIAlertView();
			alertView.Title = "API Key change";
			alertView.Message = "Sucessfully changed API key";
			alertView.AddButton("OK");
			alertView.Show();
		}
		
		
	}
}

