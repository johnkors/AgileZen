using System;
using System.Net;
using System.Collections.Generic;
using System.Xml;
using System.Runtime.Serialization.Json;
using Lib.Services;

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

	    protected RestService(string apiKey)
        {
            this.apiKey = apiKey;
        }

	    protected void Get<T>(string path, Action<Result<IEnumerable<T>>> callback, IParser<T> parser)
        {
			var webRequest = (HttpWebRequest)WebRequest.Create(_baseUrl + path + "?apikey=" + apiKey);
            webRequest.BeginGetResponse(responseResult =>
            {
                try
                {
                    var response = webRequest.EndGetResponse(responseResult);
                    if (response != null)
                    {
                        var result = ParseResult<T>(response, parser);
                        response.Close();
                        callback(new Result<IEnumerable<T>>(result));
                    }
                }
                catch (Exception ex)
                {
                    callback(new Result<IEnumerable<T>>(ex));
                }
     
            }, webRequest);
		}
		
		private IEnumerable<T> ParseResult<T>(WebResponse response, IParser<T> parser)
		{
			var responseStream = response.GetResponseStream();
			XmlDictionaryReaderQuotas xmlDictionaryReaderQoutas = new XmlDictionaryReaderQuotas();
            using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(responseStream,xmlDictionaryReaderQoutas))
            {
				return parser.Parse(jsonReader);
            }      
		}
    }
}
