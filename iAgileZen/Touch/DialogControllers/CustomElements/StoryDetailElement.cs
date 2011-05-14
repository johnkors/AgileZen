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
	public class StoryDetailElement : OwnerDrawnElement
	{
		CGGradient gradient;
		private UIFont descriptionFont = UIFont.SystemFontOfSize(14.0f);
		private UIFont nameFont = UIFont.BoldSystemFontOfSize(16.0f);
		private string _text;
		

		public StoryDetailElement (AgileZenStory story) : base(UITableViewCellStyle.Default, "sampleOwnerDrawnElement")
		{
			this.Text = story.Text;
			this.Status = story.Status;
			this.User = story.Owner.Name;
			
			CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();
			gradient = new CGGradient(
			    colorSpace,
			    new float[] { 0.95f, 0.95f, 0.95f, 1, 
							  0.85f, 0.85f, 0.85f, 1},
				new float[] { 0, 1 } );
		}
		
		public string Text
		{
			get
			{
				if(string.IsNullOrEmpty(_text))
				{
					return "No description";
				}
				return _text;
			} 
			set
			{
				_text = value;
			} 
		}
		
		public string Status
		{
			get; set; 
		}
		
		public string User
		{
			get;set;
		}
		
		public override void Draw (RectangleF bounds, CGContext context, UIView view)
		{
			UIColor.White.SetFill ();
			context.FillRect (bounds);
			
			context.DrawLinearGradient (gradient, new PointF (bounds.Left, bounds.Top), new PointF (bounds.Left, bounds.Bottom), CGGradientDrawingOptions.DrawsAfterEndLocation);
			
			UIColor.Black.SetColor ();
			view.DrawString("Assigned to: " + this.User, new RectangleF(10, 5, bounds.Width-20, 7), nameFont,UILineBreakMode.TailTruncation);
			
			
			
			UIColor.DarkGray.SetColor();
			view.DrawString(this.Text, new RectangleF(10, 30, bounds.Width - 20, TextHeight(bounds) ), descriptionFont, UILineBreakMode.WordWrap);
		}
		
		public override float Height (RectangleF bounds)
		{
			var height = 100.0f + TextHeight (bounds);
			return height;
		}
		
		private float TextHeight (RectangleF bounds)
		{
			SizeF size;
			using (NSString str = new NSString (this.Text))
			{
				size = str.StringSize (descriptionFont, new SizeF (bounds.Width - 20, 1000), UILineBreakMode.WordWrap);
			}			
			return size.Height;
		}
		
		public override string ToString ()
		{
			return string.Format (Text);
		}
	}
}