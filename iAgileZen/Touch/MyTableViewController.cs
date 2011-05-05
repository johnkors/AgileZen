using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using AgileZen.Lib;

namespace Touch
{
	public class MyTableViewController : UITableViewController {
		static NSString kCellIdentifier = new NSString ("myTVC");
		private AgileZenService _service;
		
		public IEnumerable<AgileZenProject> AgileZenProjects;
		
		public MyTableViewController()
		{
			_service = new AgileZenService();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();
			Title = "AgileZen";
			var agileZenApiKey = "40092c42cfd64a309df016dc8afcf826";
			 _service.GetProjects(agileZenApiKey, OnProjectsFetched);
		}

		
		public void OnProjectsFetched (Result<IEnumerable<AgileZenProject>> result)
		{
			InvokeOnMainThread 
			(
				delegate 
				{
					if(result.Error == null)
					{
						AgileZenProjects = result.Value.ToList();
					}
					else
					{
						AgileZenProjects = new List<AgileZenProject>();
						Title = "Error! Nett-tilgang?";
					}
				
					TableView.Delegate = new TableDelegate (this);
					TableView.DataSource = new DataSource (this);
					TableView.ReloadData();
				}
			);
		}	
		
		//
		// The data source for our TableView
		//
		class DataSource : UITableViewDataSource {
			MyTableViewController tvc;
			
			public DataSource (MyTableViewController tvc)
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
		class TableDelegate : UITableViewDelegate {
			MyTableViewController tvc;
	
			public TableDelegate (MyTableViewController tvc)
			{
				this.tvc = tvc;
			}
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				var currentProject = tvc.AgileZenProjects.ElementAt(indexPath.Row);
				Console.WriteLine(currentProject.Name);
				
				var detailsViewController = new UIViewController();
				
				AddLabel(detailsViewController,10,"Beskrivelse");
				AddLabel(detailsViewController,60, currentProject.Description);
				AddLabel(detailsViewController,110,"Id");
				AddLabel(detailsViewController,160, currentProject.Id);
				
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


	
	

