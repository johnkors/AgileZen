using System;
using AgileZen.Lib;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Touch
{
	public class SettingsController : BaseController
	{
		private AgileZenUser _currentUser;
		private DialogViewController _dv;
		private Section _meSection;
		private Section _errorSection;
		
		public SettingsController (UINavigationController navController) : base(navController)
		{
			
		}
		
		public override void PushViewController()
		{
			_currentUser = _objectStore.Load<AgileZenUser>("AgileZenUser.txt");
			var root = CreateRoot ();
			_dv = new DialogViewController (root, true);
			PushViewController (_dv, true);
			GetMe();
		}
		
		protected override UIImage CreateIconImage()
		{
			return UIImage.FromFile("Img/20-gear2.png");
		}
		
		private RootElement CreateRoot ()
		{
			var apiElement = new EntryElement ("Key", "Enter API key", _currentUser.ApiKey);
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
			_currentUser.ApiKey = ((EntryElement) sender).Value;
			_objectStore.Save<AgileZenUser>(_currentUser,"AgileZenUser.txt");
			GetMe();
			
		}
		
		private void GetMe()
		{
			var agService = new AgileZenService(_currentUser.ApiKey);
			agService.GetMe(HandleMeCallFinished, false);
		}
		
		private void HandleMeCallFinished(Result<AgileZenUser> result)
		{
			_navController.InvokeOnMainThread
				(
					delegate
					{
					    bool couldFetchUser = result.Error == null;
						if(couldFetchUser)
						{
							_currentUser = result.Value;
							
						}
						else
						{
							_currentUser = new AgileZenUser();
							Console.WriteLine("Error in call to /me");
						}
						UpdateRoot(couldFetchUser);
					}
				);
		}
		
		private void UpdateRoot(bool couldFetchUser)
		{
			if(couldFetchUser)
			{
				ClearSections();
				
				_meSection = new Section("User info for API key"){
					//new ImageStringElement("Image", UIImage.From) GRAVATAR MD5 hash and http://www.gravatar.com/avatar/HASH-VALUE-HERE
					new StringElement("Name", _currentUser.Name),
					new StringElement("E-mail", _currentUser.Email),
					new StringElement("Username", _currentUser.UserName)
				};
				_dv.Root.Add(_meSection);
			}
			else
			{	
				ClearSections();
				_errorSection = new Section("Error"){
					new StyledStringElement("Invalid API key or no connection"){
						TextColor = UIColor.Red
					}
				};
				_dv.Root.Add(_errorSection);
			}	
		}
		
		private void ClearSections()
		{
			_dv.Root.Remove(_errorSection);
			_dv.Root.Remove(_meSection);
		}
		
		
	}
}

