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
		private UIImage _icon;
		
		public UIImage Icon 
		{
			get { return _icon; }
		}
		
		public Settings (UINavigationController navController)
		{
			_navController = navController;
			_objectStore = new MonoObjectStore();
			_icon = CreateIconImage();
		}
		
		public void PushSettingsDialog()
		{
			var root = CreateRoot ();
			var dv = new DialogViewController (root, true);
			_navController.PushViewController (dv, true);
		}
		
		private UIImage CreateIconImage()
		{
			return UIImage.FromFile("Img/20-gear2.png");
		}
		
		private RootElement CreateRoot ()
		{
			var user = _objectStore.Load<AgileZenUser>("AgileZenUser.txt");
			
			var apiElement = new EntryElement ("Key", "Enter API key", user.ApiKey);
			apiElement.Changed += HandleChangedApiKey;
			
			return new RootElement ("Settings") 
			{
				new Section (null,"API key as activated on AgileZen.com")
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

