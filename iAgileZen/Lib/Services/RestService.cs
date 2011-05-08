using System;
using System.Net;
using Lib.Services;
using JsonSerializer = Lib.Services.JsonSerializer;

namespace AgileZen.Lib
{
	/// <summary>
	/// Rest service (stjålet villt fra  Jonas Follesøs Flytider-kodebase 
	/// https://github.com/follesoe/FlightsNorway.git
	/// </summary>
    public class RestService
    {
	    private ISerializer serializer;

	    public RestService()
        {
	        serializer = new JsonSerializer();
        }
        public RestService(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public void Get<T>(string url, Action<Result<T>> callback)
        {
			var webRequest = (HttpWebRequest)WebRequest.Create(url);
	        webRequest.Accept = "application/json";
            webRequest.BeginGetResponse(responseResult =>
            {
                try
                {
                    var response = webRequest.EndGetResponse(responseResult);
                    if (response != null)
                    {
                        var result = serializer.Deserialize<T>(response.GetResponseStream());
                        response.Close();
                        callback(new Result<T>(result));
                    }
                }
                catch (Exception ex)
                {
                    callback(new Result<T>(ex));
                }
     
            }, webRequest);
		}
    }
}
