using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace Lib
{
	public class AgileZenService : RestService<AgileZenProject>
	{
		public void GetProjects(string apiKey, Action<Result<IEnumerable<AgileZenProject>>> callback)
		{ 
			 Get(apiKey, callback);
		}
		
		public override IEnumerable<AgileZenProject> ParseJson (XmlReader jsonReader)
		{
			
			var xml = XDocument.Load(jsonReader);
			var agileZenProjects = 	from c in xml.Elements("root")
							   		from d in c.Elements("items")
									from e in d.Elements("item")
							 		select new AgileZenProject()
										{
											Name = e.Element("name").Value,	
											Id = e.Element("id").Value,
											Description = e.Element("description").Value
										
										};	
			return agileZenProjects;

		}
	}
}


