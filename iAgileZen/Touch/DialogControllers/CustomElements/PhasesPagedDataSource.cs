using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using escoz;
using System.Drawing;
using MonoTouch.Dialog;
using System.Linq;

namespace Touch
{
	public class PhasesPagedDataSource : IPagedViewDataSource
	{
		public PhasesPagedDataSource (IEnumerable<DialogViewController> phasesControllers)
		{
			PhaseViewControllers = phasesControllers.ToArray();
			SetBackgroundImages();
		}
		
		private DialogViewController[] PhaseViewControllers
		{
			get;set;
		}

		public int Pages {get {return PhaseViewControllers.Count();} }

		public UIViewController GetPage(int i)
		{
				Console.WriteLine(i);
				Console.WriteLine(PhaseViewControllers.Length);
				return PhaseViewControllers[i];
		}

		public void Reload(){
			Console.WriteLine("Count of phases: " + PhaseViewControllers.Length);
		}
		
		private void SetBackgroundImages()
		{
			for(int i=0;i< PhaseViewControllers.Length; i++)
			{
				var dv = PhaseViewControllers[i];
				int index = i+1;
				var photo = "Img/agilezen_"+index+".png";
				Console.WriteLine("photo: " +photo);
				var img = UIImage.FromFile(photo);
				var imageView = new UIImageView(img);
				imageView.UserInteractionEnabled = true;
				//imageView.Alpha = 0.5f;
				dv.View.BackgroundColor = UIColor.Clear;
				imageView.AddSubview(dv.View);
				dv.View = imageView;
			}
				
		}
		
	}
}

