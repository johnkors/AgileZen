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
			azService.GetMyStories(OnStoriesFetched);
		}
		
		private void OnStoriesFetched (Result<AgileZenMyStories> result)
		{
			_navController.InvokeOnMainThread(
				delegate{
				
					IEnumerable<AgileZenStory> myStories;
					if(result.Error == null)
					{
						myStories = result.Value.Items;
					}
					else
					{
						myStories = new List<AgileZenStory>();
						ShowErrorAlert();
					}
					UpdateRoot(myStories);
					StopAnimatingHud();
				
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
		
		private void UpdateRoot(IEnumerable<AgileZenStory> myStories)
		{
			var projects = (from c in myStories select c.Project).DistinctBy( project => project.Id);
			
			var projectSections = new List<Section>();
			foreach(var project in projects)
			{
				var section = new Section(project.Name);
				var elements = GetMatchingStoriesToProject(project, myStories);
				section.Add(elements);
				projectSections.Add(section);
			}
			_dv.Root = UpdatedRoot(projectSections);
		}
		
		private IEnumerable<Element> GetMatchingStoriesToProject(AgileZenProject project,IEnumerable<AgileZenStory> myStories)
		{
			List<Element> elements = new List<Element>();
			foreach (AgileZenStory story in myStories) 
			{
				if(story.Project.Id == project.Id)
				{
					var element = CreateStoryElement (story);
					elements.Add(element);
				}
			}
			return elements;
		}
		
		private Element CreateStoryElement (AgileZenStory story)
		{
			var storyDetails = new StoryDetailsController(_navController, story);
			var element = new StyledStringElement(story.Owner.Name,story.Text,UITableViewCellStyle.Subtitle);
			element.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			element.Tapped += storyDetails.PushViewController;
			return element;
		}
		
		private RootElement UpdatedRoot (IEnumerable<Section> sections)
		{
			var rootElement = new RootElement ("My stories");
			rootElement.Add(sections);
			return rootElement;
		}	
		
		
	}
}

