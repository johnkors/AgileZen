using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;

namespace Lib
{
	/// <summary>
	/// Rest service (stjålet villt fra  Jonas Follesøs Flytider-kodebase 
	/// https://github.com/follesoe/FlightsNorway.git
	/// </summary>
    public abstract class RestService<T> where T : class
    {
        private string _baseUrl = "https://agilezen.com/api/v1/projects?apikey=";

		public abstract IEnumerable<T> ParseJson(XmlReader jsonReader);
        protected void Get(string resource, Action<Result<IEnumerable<T>>> callback)
        {
			var webRequest = (HttpWebRequest)WebRequest.Create(_baseUrl+resource);
            webRequest.BeginGetResponse(responseResult =>
            {
                try
                {
                    var response = webRequest.EndGetResponse(responseResult);
                    if (response != null)
                    {
                        var result = ParseResultJson(response);
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
		
		private IEnumerable<T> ParseResultJson(WebResponse response)
		{
			var responseStream = response.GetResponseStream();
			XmlDictionaryReaderQuotas xmlDictionaryReaderQoutas = new XmlDictionaryReaderQuotas();
            using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(responseStream,xmlDictionaryReaderQoutas))
            {
				return ParseJson(jsonReader);
            }      
		}
		
	
    }
}
