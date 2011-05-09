using System;

namespace AgileZen.Lib
{
	public class AgileZenService : RestService
	{
        private const string _baseUrl = "https://agilezen.com/api/v1/projects";
	    private string apiKey;
	    public AgileZenService(string apiKey)
	    {
	        this.apiKey = apiKey;
	    }

	    public void GetProjects(Action<Result<AgileZenProjectResult>> callback)
	    {
	        var url = string.Format("{0}?apikey={1}", _baseUrl, apiKey);
            Get(url, callback);
		}

        public void GetStories(string projectId, Action<Result<AgileZenStoryResult>> callback)
        {
            var url = string.Format("{0}/{2}/stories?apikey={1}", _baseUrl, apiKey, projectId);
            Get(url, callback);
        }
	}
}


