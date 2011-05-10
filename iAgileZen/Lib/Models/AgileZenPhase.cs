using System.Collections.Generic;

namespace AgileZen.Lib
{
    public class AgileZenPhase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Index { get; set; }
        public IEnumerable<AgileZenStory> Stories { get; set; }
    }
}