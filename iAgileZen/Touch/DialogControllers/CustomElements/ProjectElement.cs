using System;
using System.Drawing;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using AgileZen.Lib;

namespace Touch
{
	public class ProjectElement : OwnerDrawnElement
	{
		CGGradient gradient;
		private UIFont descriptionFont = UIFont.SystemFontOfSize(10.0f);
		private UIFont nameFont = UIFont.BoldSystemFontOfSize(14.0f);
		private string _description;

		public ProjectElement (AgileZenProject project) : base(UITableViewCellStyle.Default, "sampleOwnerDrawnElement")
		{
			this.Description = project.Description;
			this.Text = project.Name;
			this.CreatedTime = project.CreateTime;
			this.Id = project.Id;
			
			CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();
			gradient = new CGGradient(
			    colorSpace,
			    new float[] { 0.95f, 0.95f, 0.95f, 1, 
							  0.85f, 0.85f, 0.85f, 1},
				new float[] { 0, 1 } );
		}
		
		public string Description
		{
			get
			{
				if(string.IsNullOrEmpty(_description))
				{
					return "No description";
				}
				return _description;
			} 
			set
			{
				_description = value;
			} 
		}
		
		public string Text
		{
			get; set; 
		}

		public string CreatedTime
		{
			get; set; 
		}
	
		public string Id
		{
			get;set;
		}
		
		public override void Draw (RectangleF bounds, CGContext context, UIView view)
		{
			UIColor.White.SetFill ();
			context.FillRect (bounds);
			
			context.DrawLinearGradient (gradient, new PointF (bounds.Left, bounds.Top), new PointF (bounds.Left, bounds.Bottom), CGGradientDrawingOptions.DrawsAfterEndLocation);
			
			UIColor.Black.SetColor ();
			view.DrawString(this.Text, new RectangleF(10, 5, bounds.Width-20, 7), nameFont,UILineBreakMode.TailTruncation);
			
			//UIColor.Brown.SetColor ();
			//view.DrawString(this.CreatedTime, new RectangleF(bounds.Width/2, 5, (bounds.Width/2) - 10, 10 ), dateFont, UILineBreakMode.TailTruncation, UITextAlignment.Right);
			
			UIColor.DarkGray.SetColor();
			view.DrawString(this.Description, new RectangleF(10, 30, bounds.Width - 20, TextHeight(bounds) ), descriptionFont, UILineBreakMode.WordWrap);
		}
		
		public override float Height (RectangleF bounds)
		{
			var height = 40.0f + TextHeight (bounds);
			return height;
		}
		
		private float TextHeight (RectangleF bounds)
		{
			SizeF size;
			using (NSString str = new NSString (this.Description))
			{
				size = str.StringSize (descriptionFont, new SizeF (bounds.Width - 20, 1000), UILineBreakMode.WordWrap);
			}			
			return size.Height;
		}
	
		public override void Selected (DialogViewController dvc, UITableView tableView, NSIndexPath path)
		{
			Console.WriteLine("Selected project");
			var phasesWithStoriesController = new PhasesWithStoriesController(dvc.NavigationController, Id);
			phasesWithStoriesController.PushViewController();
		}
		
		public override string ToString ()
		{
			return string.Format (Description);
		}
	}
}