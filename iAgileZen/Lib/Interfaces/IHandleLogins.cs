using System;

namespace AgileZen.Lib
{
	public delegate void LoginEventHandler();
	
	public interface IHandleLogins
	{
		void HandleLoginBegan();
		void HandleOkApiKey(string apiKey);
		void HandleErronousApiKey();
		void HandleNoConnection(string errorMsg); 
		bool HasStoredApiKey ();
		
		event LoginEventHandler OnSuccessfulLogin;
		event LoginEventHandler OnLoginBegan;
		event LoginEventHandler OnLoginFinished;
	}

}

