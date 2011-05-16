using System;

namespace AgileZen.Lib
{
	public delegate void SuccessfulLoginHandler();
	
	public interface IHandleLogins
	{
		void HandleOkApiKey(string apiKey);
		void HandleErronousApiKey();
		void HandleNoConnection(string errorMsg); 
		bool HasStoredApiKey ();
		event SuccessfulLoginHandler OnSuccessfulLogin;
	}

}

