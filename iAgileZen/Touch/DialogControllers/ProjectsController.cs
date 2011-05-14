using System;
using System.Collections.Generic;
using System.Linq;
using AgileZen.Lib;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Touch
{
	public class ProjectsController : BaseController
	{	
		private DialogViewController _dv;
		
		public ProjectsController(UINavigationController navController) : base(navController)
		{
		}
		
		public void PushViewController()
		{
			var rootElement = new RootElement("Projects"); // empty root. Updateded async
			_dv = new DialogViewController(rootElement,true);
			PushViewController(_dv,true);
			GetProjectElements();
		}
		
		protected override UIImage CreateIconImage()
		{
			return UIImage.FromFile("Img/33-cabinet.png");
		}
		
		private void GetProjectElements()
		{
			var user = _objectStore.Load<AgileZenUser>("AgileZenUser.txt");
			var azService = new AgileZenService(user.ApiKey);
			azService.GetProjects(OnProjectsFetched);
		}
		
		private void OnProjectsFetched (Result<AgileZenProjectResult> result)
		{
			_navController.InvokeOnMainThread 
			(
				delegate 
				{
					IEnumerable<AgileZenProject> projects;
					if(result.Error == null)
					{
						projects = result.Value.Items;
					}
					else
					{
						projects = new List<AgileZenProject>();
						ShowErrorAlert();
					}
					UpdateRoot(projects);
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
		
		private void UpdateRoot(IEnumerable<AgileZenProject> projects)
		{
			var elements = from c in projects select new ProjectElement(c) as Element;
			_dv.Root = UpdatedRoot(elements);
		}
		
		private RootElement UpdatedRoot (IEnumerable<Element> elements)
		{
			var projectsSection = new Section (null,"AgileZen Projects");
			projectsSection.Add(elements);
			var rootElement = new RootElement ("Projects");
			rootElement.Add(projectsSection);
			return rootElement;
		}
	}
}

