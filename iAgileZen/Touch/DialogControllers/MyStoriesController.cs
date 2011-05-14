using System;
using System.Linq;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using AgileZen.Lib;


namespace Touch
{
	public class MyStoriesController : BaseController
	{
		private DialogViewController _dv;
		private AgileZenUser _currentUser;
		
		public MyStoriesController (UINavigationController navController): base(navController)
		{
		}
		
		
		public override void PushViewController()
		{
			var rootElement = new RootElement("My stories"); // empty root. Updateded async
			_dv = new DialogViewController(rootElement,true);
			PushViewController(_dv,true);
			GetStoriesGroupedByProjectElements();
		}
		
		protected override UIImage CreateIconImage()
		{
			return UIImage.FromFile("Img/44-shoebox.png");
		}
		
		private void GetStoriesGroupedByProjectElements()
		{
			_currentUser = _objectStore.Load<AgileZenUser>("AgileZenUser.txt");
			var azService = new AgileZenService(_currentUser.ApiKey);
			azService.GetMe(OnStoriesFetched,true);
		}
		
		private void OnStoriesFetched (Result<AgileZenUser> result)
		{
			_navController.InvokeOnMainThread(
				delegate{
				
					IEnumerable<AgileZenStory> simpleStoriesWithoutOwnerOrPhases;
					if(result.Error == null)
					{
						simpleStoriesWithoutOwnerOrPhases = result.Value.Stories;
					}
					else
					{
						simpleStoriesWithoutOwnerOrPhases = new List<AgileZenStory>();
						ShowErrorAlert();
					}
					UpdateRoot(simpleStoriesWithoutOwnerOrPhases);
				
			});

		}	
		
		private void ShowErrorAlert ()
		{
			var alertView = new UIAlertView();
			alertView.Title = "Oops!";
			alertView.Message = "Could not connect to AgileZen. Check network connection, or API key";
			alertView.AddButton("OK");
			alertView.Show();
		}	
		
		private void UpdateRoot(IEnumerable<AgileZenStory> simpleStoriesWithoutOwnerOrPhases)
		{
			List<Element> elements = new List<Element>();
			foreach (var simpleStory in simpleStoriesWithoutOwnerOrPhases) {
				simpleStory.Owner = new AgileZenUser(){Name = "Me"};
				var storyDetails = new StoryDetailsController(_navController, simpleStory);
				var element = new StyledStringElement(simpleStory.Owner.Name,simpleStory.Text,UITableViewCellStyle.Subtitle);
				element.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				element.Tapped += storyDetails.PushViewController;
				elements.Add(element);
			}
			
			_dv.Root = UpdatedRoot(elements);
		}
		
		private RootElement UpdatedRoot (IEnumerable<Element> elements)
		{
			var projectsSection = new Section (null,"Stories assigned to you");
			projectsSection.Add(elements);
			var rootElement = new RootElement ("My stories");
			rootElement.Add(projectsSection);
			return rootElement;
		}	
		
		
	}
}

