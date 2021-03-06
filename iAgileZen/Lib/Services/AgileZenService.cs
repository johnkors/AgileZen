using System;

namespace AgileZen.Lib
{
	public class AgileZenService : RestService
	{
		private const string _baseUrl = "https://agilezen.com/api/v1/";
        private const string _baseUrlProjects =  _baseUrl + "projects";
	    private string apiKey;
	    
		public AgileZenService(string apiKey)
	    {
	        this.apiKey = apiKey;
	    }

	    public void GetProjects(Action<Result<AgileZenProjectResult>> callback)
	    {
	        var url = string.Format("{0}?apikey={1}&with=tasks", _baseUrlProjects, apiKey);
            Get(url, callback);
		}

        public void GetStories(string projectId, Action<Result<AgileZenStoryResult>> callback)
        {
            var url = string.Format("{0}/{2}/stories?apikey={1}", _baseUrlProjects, apiKey, projectId);
            Get(url, callback);        
        }

        public void GetPhases(string projectId, Action<Result<AgileZenPhaseResult>> callback)
        {
            var url = string.Format("{0}/{2}/phases?apikey={1}", _baseUrlProjects, apiKey, projectId);
            Get(url, callback);
        }

        public void GetPhasesWithStories(string projectId, Action<Result<AgileZenPhaseResult>> callback)
        {
            var url = string.Format("{0}/{2}/phases?apikey={1}&with=stories", _baseUrlProjects, apiKey, projectId);
            Get(url, callback);
        }
		
		public Result<bool> IsAuthenticated()
		{
			var url = string.Format("{0}/me?apikey={1}",_baseUrl, apiKey);
			return IsAuthenticated(url);
		}
		
		public void GetMe(Action<Result<AgileZenUser>> callback)
		{
			var url	= string.Format("{0}/me?apikey={1}", _baseUrl, apiKey);
			Get(url, callback);
		}
		
		public void GetMyStories(Action<Result<AgileZenMyStories>> callback)
		{
			var url	= string.Format("{0}/me/stories?apikey={1}", _baseUrl, apiKey);
			Get(url, callback);
		}
	}
}


