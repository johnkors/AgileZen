using System;
using System.Collections.Generic;

namespace AgileZen.Lib
{
	public class AgileZenMyStories
	{
		public string Page {get;set;}
		public string PageSize {get;set;}
		public string TotalPages {get;set;}
		public string TotalItems {get;set;}
		public IEnumerable<AgileZenStory> Items {get;set;}
	}
}

