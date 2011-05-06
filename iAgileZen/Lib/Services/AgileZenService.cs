using System;

namespace AgileZen.Lib
{
	public class AgileZenService : RestService
	{
	    public AgileZenService(string apiKey) : base(apiKey) {}

	    public void GetProjects(Action<Result<AgileZenProjectResult>> callback)
		{
            Get<AgileZenProjectResult>("", callback);
		}

        public void GetStories(string projectId, Action<Result<AgileZenStoryResult>> callback)
        {
            Get<AgileZenStoryResult>(string.Format("/{0}/stories", projectId), callback);
        }
	}
}


