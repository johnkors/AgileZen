using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Xml;
using System.Runtime.Serialization.Json;
using Lib.Services;
using Newtonsoft.Json;
using JsonSerializer = Lib.Services.JsonSerializer;

namespace AgileZen.Lib
{
	/// <summary>
	/// Rest service (stjålet villt fra  Jonas Follesøs Flytider-kodebase 
	/// https://github.com/follesoe/FlightsNorway.git
	/// </summary>
    public abstract class RestService
    {
        private const string _baseUrl = "https://agilezen.com/api/v1/projects";
	    private string apiKey;
	    private ISerializer serializer;

	    protected RestService(string apiKey)
        {
            this.apiKey = apiKey;
	        serializer = new JsonSerializer();
        }

	    protected void Get<T>(string path, Action<Result<T>> callback)
        {
			var webRequest = (HttpWebRequest)WebRequest.Create(_baseUrl + path + "?apikey=" + apiKey);
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
