using System;

namespace AgileZen.Lib
{
	public interface IHandleLogins
	{
		void HandleOkApiKey(string apiKey);
		void HandleErronousApiKey();
		void HandleNoConnection(string errorMsg); 
		bool HasStoredApiKey ();
	}

}

