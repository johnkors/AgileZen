using System.Collections.Generic;
namespace AgileZen.Lib
{
    public class AgileZenUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
		
		public string ApiKey {get;set;}
		public IEnumerable<AgileZenStory> Stories { get; set; }
    }
}