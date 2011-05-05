using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using Lib.Services;

namespace AgileZen.Lib
{
	public class AgileZenService : RestService
	{
	    public AgileZenService(string apiKey) : base(apiKey) {}

	    public void GetProjects(Action<Result<IEnumerable<AgileZenProject>>> callback)
		{ 
			 Get<AgileZenProject>("", callback, new AgileZenProjectParser());
		}
	}
}


