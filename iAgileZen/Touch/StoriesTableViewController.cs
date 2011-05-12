using System;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AgileZen.Lib;
using System.Linq;
using System.Drawing;

namespace Touch
{
	public class StoriesTableViewController : UITableViewController
	{
		static NSString storieskCellIdentifier = new NSString ("storiesTVC");
		private AgileZenService _service;
		private string _projectId;
		private MonoObjectStore _objectStore;
		public IEnumerable<AgileZenStory> Stories;
		
		public StoriesTableViewController (string projectId)
		{
			_objectStore = new MonoObjectStore();
			var userFromFile = _objectStore.Load<AgileZenUser>("AgileZenUser.txt");
			_service = new AgileZenService(userFromFile.ApiKey);
			_projectId = projectId;
		}
		
		public override void ViewDidLoad()
		{
			Title = "Stories";
			_service.GetStories(_projectId, HandleGetStoriesFinished);
		}
		

		public void HandleGetStoriesFinished (Result<AgileZenStoryResult> storyResult)
		{
			if(storyResult.Error == null)
			{
				Stories = storyResult.Value.Items; 
			}
			else
			{
				Stories = new List<AgileZenStory>{ new AgileZenStory(){Status = "Error", Text = "Kunne ikke hente stories!"}};
			}
			
			InvokeOnMainThread(delegate
					{
						TableView.Delegate = new StoriesTableDelegate(this);
						TableView.DataSource = new StoriesDataSource(this);
						TableView.ReloadData();
					});
		}
		
		class StoriesDataSource : UITableViewDataSource {
			StoriesTableViewController tvc;
			
			public StoriesDataSource (StoriesTableViewController tvc)
			{
				this.tvc = tvc;
			}
			
			public override int RowsInSection (UITableView tableView, int section)
			{
				return tvc.Stories.Count();
			}
	
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (storieskCellIdentifier);
				if (cell == null){
					cell = new UITableViewCell (UITableViewCellStyle.Subtitle, storieskCellIdentifier);
				}
				
				var story = tvc.Stories.ElementAt(indexPath.Row);
				cell.TextLabel.Text = story.Phase.Name;
				cell.DetailTextLabel.Text = story.Text;
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				return cell;
			}
		}
	
		//
		// This class receives notifications that happen on the UITableView
		//
		class StoriesTableDelegate : UITableViewDelegate {
			StoriesTableViewController tvc;
	
			public StoriesTableDelegate (StoriesTableViewController tvc)
			{
				this.tvc = tvc;
			}
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				var currentStory = tvc.Stories.ElementAt(indexPath.Row);
				Console.WriteLine(currentStory.ToString());
				
				var detailsViewController = new UIViewController();
				
			
				var position = new Rectangle(10,50,300,300);
				var beskrivelseTextField = new UITextView(position);
				beskrivelseTextField.TextAlignment = UITextAlignment.Left;
				beskrivelseTextField.BackgroundColor = UIColor.DarkGray;
				beskrivelseTextField.TextColor = UIColor.White;
				beskrivelseTextField.Text = currentStory.Text;
				detailsViewController.View.AddSubview(beskrivelseTextField);
				detailsViewController.Title = "Beskrivelse";
				tvc.NavigationController.PushViewController(detailsViewController, true);
			
			
			}
			
			private void AddLabel(UIViewController vc, int ypos, string text)
			{
				var label = GetLabel(ypos,text);
				vc.View.AddSubview(label);
			}

			private UILabel GetLabel (int ypos, string text)
			{
				var position = new Rectangle(10,ypos,200,30);
				var label = new UILabel(position);
				label.Text = text;
				return label;
			}
		}
		
		
		
	}
}

