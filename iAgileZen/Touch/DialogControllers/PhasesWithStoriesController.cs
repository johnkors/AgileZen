using System;
using System.Linq;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using AgileZen.Lib;

namespace Touch
{
	public class PhasesWithStoriesController : BaseController
	{
		private DialogViewController _dv;
		private string _projectId;
		
		public PhasesWithStoriesController (UINavigationController navController, string projectId) : base(navController)
		{
			_projectId = projectId;
		}
		
		public override void PushViewController()
		{
			var root = new RootElement("Stories");
			_dv = new DialogViewController(root,true);
			PushViewController(_dv, true);
			GetPhasesWithStories();
		}
			
		
		private void GetPhasesWithStories()
		{
			var user = _objectStore.Load<AgileZenUser>("AgileZenUser.txt");
			var azService = new AgileZenService(user.ApiKey);
			azService.GetPhasesWithStories(_projectId, OnPhasesWithStoriesFetched);
		}
		
		private void OnPhasesWithStoriesFetched(Result<AgileZenPhaseResult> result)
		{
			_navController.InvokeOnMainThread 
			(
				delegate 
				{
					IEnumerable<AgileZenPhase> phasesWithoutBacklogAndArchive;
					if(result.Error == null)
					{
						var maxIndex = (from c in result.Value.Items select c.Index).Max();
						phasesWithoutBacklogAndArchive = from c in result.Value.Items where c.Index > 0 && c.Index < maxIndex select c;
					}
					else
					{
						phasesWithoutBacklogAndArchive = new List<AgileZenPhase>();
						ShowErrorAlert();
					}
					UpdateRoot(phasesWithoutBacklogAndArchive);
					StopAnimatingHud();
				}
			);
		}
		
		private void UpdateRoot(IEnumerable<AgileZenPhase> phases)
		{
			var projectSections = new List<Section>();
			foreach (AgileZenPhase phase in phases) {
				var section = new Section (phase.Name,phase.Description);
				
				foreach(var story in phase.Stories)
				{
					var storyDetails = new StoryDetailsController(_navController, story, phase);
					var element = new StyledStringElement(story.Owner.Name,story.Text,UITableViewCellStyle.Subtitle);
					element.Accessory = UITableViewCellAccessory.DisclosureIndicator;
					element.Tapped += storyDetails.PushViewController;
					section.Add(element);
				}
				
				projectSections.Add(section);
			}
			var rootElement = new RootElement ("Stories");
			rootElement.Add(projectSections);
			_dv.Root = rootElement;
		}
		
		private void ShowErrorAlert ()
		{
			var alertView = new UIAlertView();
			alertView.Title = "Oops!";
			alertView.Message = "Could not connect to AgileZen. Check network connection, or API key";
			alertView.AddButton("OK");
			alertView.Show();
		}	
		
		
		#region implemented abstract members of Touch.MainMenuBase
		protected override UIImage CreateIconImage ()
		{
			return null;
		}
		
		#endregion
	}
}

