using System;
using MonoTouch.UIKit;
using escoz;

namespace Touch
{
	public abstract class BaseController
	{
		protected UINavigationController _navController;
		protected MonoObjectStore _objectStore;
		private UIImage _icon;
		private LoadingHUDView _hud;
		
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
		
		public abstract void PushViewController();
		
		protected abstract UIImage CreateIconImage();
		
		protected void PushViewController(UIViewController viewController, bool animated)
		{
			StartAnimatingHud();
			_navController.PushViewController(viewController, animated);
		
		}
		
		protected void StartAnimatingHud()
		{
			_hud = new LoadingHUDView("Loading", "Contacting AgileZen");
			_navController.View.AddSubview(_hud);
			_hud.StartAnimating();
		}
		
		protected void StopAnimatingHud()
		{
			_hud.StopAnimating();
			_hud.RemoveFromSuperview();
		}
		
	}
}

