using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using AgileZen.Lib;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Touch
{
	public class LoginViewController : UIViewController
	{
		UITextField _apiKeyTextField;
		AgileZenService _agileZenService;
		IHandleLogins _loginHandler;
		
		private UIActivityIndicatorView _progressIndicator;
		
		public LoginViewController (IHandleLogins loginHandler)
		{
			_loginHandler = loginHandler;
			_progressIndicator = new UIActivityIndicatorView();
		}
		
		public override void ViewDidLoad()
		{
			View.BackgroundColor = UIColor.Black;
			
			var yposition = 65;
			var labelTextBoxHeighDiff = 40;
			var usernameLbl = GetLabel("API key", yposition);
			_apiKeyTextField = GetTextField(yposition + labelTextBoxHeighDiff);
			var imgView = GetImageView ();
			
			var loginButton = GetLoginButton (yposition + 75);
		
			View.AddSubview(imgView);
			View.AddSubview(usernameLbl);
			View.AddSubview(_apiKeyTextField);
			View.AddSubview(loginButton);
		}
		
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			HideKeyboard ();
		}
		
		private UIButton GetLoginButton (float buttonYposition)
		{
			var loginButton = UIButton.FromType(UIButtonType.RoundedRect);
			var buttonWidth = 160f;
			var center = (View.Frame.Width / 2) - (buttonWidth / 2);
			loginButton.Frame = new RectangleF(center,buttonYposition,160f,40f);
			loginButton.ContentMode = UIViewContentMode.Center;
			loginButton.SetTitle("Logg inn",UIControlState.Normal);
			loginButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			loginButton.TouchDown += AddProgressIndicator;
			loginButton.TouchUpInside += HandleLoginButtonTouchUpInside;
			return loginButton;
		}
		
		private UIImageView GetImageView ()
		{
			var img = UIImage.FromFile("Img/logo.png");
			var imageWidth = 145f;
			var center = (View.Frame.Width / 2)  - (imageWidth / 2);
			var imgView = new UIImageView(new RectangleF(center,30f,145f,60f));
			imgView.Image = img;
			return imgView;
		}
		
		private void AddProgressIndicator(object sender, EventArgs e)
		{
			_progressIndicator.StartAnimating();
			this.View.AddSubview(_progressIndicator);
		}

		private void HandleLoginButtonTouchUpInside (object sender, EventArgs e)
		{
			var enteredApiKey = _apiKeyTextField.Text;
			_agileZenService = new AgileZenService(enteredApiKey);
			var isAuthenticatedResult = _agileZenService.IsAuthenticated();
			_progressIndicator.StopAnimating();
			if(!isAuthenticatedResult.HasError())
			{
				if(isAuthenticatedResult.Value)
				{
					HideKeyboard();
					_loginHandler.HandleOkApiKey(_apiKeyTextField.Text);
				
				}
				else
				{
					_loginHandler.HandleErronousApiKey();
				}
			}
			else
			{
				_loginHandler.HandleNoConnection(isAuthenticatedResult.Error.ToString());
			}
		}
		
		private void HideKeyboard ()
		{
			// hides keyboard
			_apiKeyTextField.ResignFirstResponder();
		}
		
		private UILabel GetLabel(string text, int ypos)
		{
			var rect = new Rectangle(10,ypos,100,50);
			var lbl = new UILabel(rect);
			lbl.TextColor = UIColor.White;
			lbl.BackgroundColor = UIColor.Clear;
			lbl.Text = text;
			return lbl;
		}
		
		private UITextField GetTextField(int ypos)
		{
			var rect = new Rectangle(10,ypos,300,30);
			var textField = new UITextField(rect);
			textField.BorderStyle = UITextBorderStyle.RoundedRect;
			textField.TextAlignment = UITextAlignment.Center;
			
			// Hide keyboard on "return"-key click
		    textField.ShouldReturn = delegate
		    {
		    	textField.ResignFirstResponder();
		    	return true;
		    };
			return textField;
		}
	}
	
	public class LoginTextField : UITextField
	{
		public LoginTextField(RectangleF rectf): base(rectf)
		{
			this.BorderStyle = UITextBorderStyle.RoundedRect;
		}
	}
	
}

