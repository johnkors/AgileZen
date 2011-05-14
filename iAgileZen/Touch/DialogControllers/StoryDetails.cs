using System;
using MonoTouch.UIKit;
using AgileZen.Lib;
using MonoTouch.Dialog;
namespace Touch
{
	public class StoryDetails : BaseController
	{
		private AgileZenStory _story;
		private AgileZenPhase _phase;
		public StoryDetails (UINavigationController navController, AgileZenStory story, AgileZenPhase phase) : base(navController)
		{
			_story = story;
			_phase = phase;
		}
		
		public void PushViewController()
		{
			var root = CreateRoot();
			var dv = new DialogViewController(root,true);
			PushViewController(dv,true);
		}
		
		private RootElement CreateRoot()
		{
			var storyElement = new StoryElement(_story);
			
			var root = new RootElement(_phase.Name){
				new Section("Status: " + _story.Status){
					storyElement
				}
			};
			return root;
		}
		
		#region implemented abstract members of Touch.MainMenuBase
		protected override UIImage CreateIconImage ()
		{
			return null;
		}
		
		#endregion
	}
}

