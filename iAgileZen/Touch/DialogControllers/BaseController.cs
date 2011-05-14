using System;
using MonoTouch.UIKit;

namespace Touch
{
	public abstract class BaseController
	{
		protected UINavigationController _navController;
		protected MonoObjectStore _objectStore;
		private UIImage _icon;
		
		public BaseController (UINavigationController navController)
		{
			_navController = navController;
			_objectStore = new MonoObjectStore();
			_icon = CreateIconImage();
		}
		
		public UIImage Icon 
		{
			get { return _icon; }
		}
		
		protected abstract UIImage CreateIconImage();
		
		protected void PushViewController(UIViewController viewController, bool animated)
		{
			_navController.PushViewController(viewController, animated);
		}
		
	}
}

