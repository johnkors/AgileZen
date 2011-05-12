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
		
		public Result<bool> IsAuthenticated (string url)
		{
     		var uri = new Uri(url);
	       	var webRequest = (HttpWebRequest)WebRequest.Create(uri);
			webRequest.Timeout = 300000; // in milliseconds
			try
			{
				var response = (HttpWebResponse)webRequest.GetResponse();
				if(response.StatusCode == HttpStatusCode.OK)
				{
					return new Result<bool>(true);
				}
				return new Result<bool>(false);
			}
			catch(WebException we)
			{
				if( we.Response != null && ((HttpWebResponse) we.Response).StatusCode == HttpStatusCode.InternalServerError) //HttpStatusCode.Unauthorized not returned on erronous API key
				{
					return new Result<bool>(false);
				}
				return new Result<bool>(we);
			}
			catch(Exception e)
			{
				return new Result<bool>(e);
			}
		}
    }
}

