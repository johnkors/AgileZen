using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using AgileZen.Lib;
using MonoTouch.Dialog;

namespace Touch
{
	public class ProjectTableViewController : UITableViewController {
		static NSString kCellIdentifier = new NSString ("projectTVC");
		private AgileZenService _service;
		private MonoObjectStore _objectStore;
		public IEnumerable<AgileZenProject> AgileZenProjects;
		
		public ProjectTableViewController()
		{
			_objectStore = new MonoObjectStore();
			
		}
		
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			SetService ();
		}
		
		public override void ViewDidLoad()
		{
			Title = "Prosjekter";
			if(_service == null)
			{
				SetService();
			}
		    _service.GetProjects(OnProjectsFetched);
		}
		
		private void SetService ()
		{
			var userFromFile = _objectStore.Load<AgileZenUser>("AgileZenUser.txt");
			_service = new AgileZenService(userFromFile.ApiKey);
		}
		
		private void OnProjectsFetched (Result<AgileZenProjectResult> result)
		{
			InvokeOnMainThread 
			(
				delegate 
				{
					if(result.Error == null)
					{
						AgileZenProjects = result.Value.Items;
					}
					else
					{
						AgileZenProjects = new List<AgileZenProject>();
						ShowErrorAlert();
					}
				
					TableView.Delegate = new ProjectTableDelegate (this);
					TableView.DataSource = new ProjectDataSource (this);
					TableView.ReloadData();
					Console.WriteLine("Reloading data");
				}
			);
		}	

		
		private void ShowErrorAlert ()
		{
			var alertView = new UIAlertView();
			alertView.Title = "Oops!";
			alertView.Message = "Could not connect to AgileZen. Check network connection, or API key";
			alertView.AddButton("OK");
			alertView.Show();
		}		
		
		//
		// The data source for our TableView
		//
		class ProjectDataSource : UITableViewDataSource {
			ProjectTableViewController tvc;
			
			public ProjectDataSource (ProjectTableViewController tvc)
			{
				this.tvc = tvc;
			}
			
			public override int RowsInSection (UITableView tableView, int section)
			{
				return tvc.AgileZenProjects.Count();
			}
	
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (kCellIdentifier);
				if (cell == null){
					cell = new UITableViewCell (UITableViewCellStyle.Default, kCellIdentifier);
				}
				
				var currentProject = tvc.AgileZenProjects.ElementAt(indexPath.Row);
				cell.TextLabel.Text = currentProject.Name;
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				return cell;
			}
		}
	
		//
		// This class receives notifications that happen on the UITableView
		//
		class ProjectTableDelegate : UITableViewDelegate {
			ProjectTableViewController tvc;
	
			public ProjectTableDelegate (ProjectTableViewController tvc)
			{
				this.tvc = tvc;
			}
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				var currentProject = tvc.AgileZenProjects.ElementAt(indexPath.Row);
				StoriesTableViewController storiesTvc = new StoriesTableViewController(currentProject.Id);
				tvc.NavigationController.PushViewController(storiesTvc, true);
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


	
	

