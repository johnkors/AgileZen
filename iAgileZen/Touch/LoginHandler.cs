using System;
using System.IO;
using MonoTouch.UIKit;
using AgileZen.Lib;

namespace Touch
{
	public delegate void HandleSuccessfulLogin();
	
	public class LoginHandler : IHandleLogins
	{
		private MonoObjectStore _objectStore;
		private UIApplication app = UIApplication.SharedApplication;
		
		public HandleSuccessfulLogin OnSuccessfulLogin = delegate {};
		
		public LoginHandler ()
		{
			_objectStore = new MonoObjectStore();
		}

		public void HandleOkApiKey (string apiKey)
		{
			var agileZenUser = new AgileZenUser();
			agileZenUser.ApiKey = apiKey;
			SaveUserCredentials (agileZenUser);
			RemoveNetworkIndicator();
			OnSuccessfulLogin();
		}

		public void HandleErronousApiKey ()
		{	
			RemoveNetworkIndicator();
			ShowInvalidCredentialsMessage("Erronous API key!");	
		}

		public void HandleNoConnection (string errorMsg)
		{
			RemoveNetworkIndicator();
			ShowInvalidCredentialsMessage("Could not connect to AgileZen!");
		}
		
		public bool HasStoredApiKey ()
		{
			try
			{
				var agileZenUser = _objectStore.Load<AgileZenUser>("AgileZenUser.txt");
				return !string.IsNullOrEmpty(agileZenUser.ApiKey);
				
			}
			catch (FileNotFoundException)
			{
				return false;
			}
		}
		
		private void SaveUserCredentials (AgileZenUser user )
		{
			_objectStore.Save<AgileZenUser>(user,"AgileZenUser.txt");
		}
		
		private void ShowInvalidCredentialsMessage (string message)
		{
			var alertView = new UIAlertView();
			alertView.Title = "Oops!";
			alertView.Message = message;
			alertView.AddButton("OK");
			alertView.Show();
		}	
		
		private void RemoveNetworkIndicator ()
		{
			app.NetworkActivityIndicatorVisible = false;
		}
	}
}

