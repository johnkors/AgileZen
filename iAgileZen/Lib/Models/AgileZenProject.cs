namespace AgileZen.Lib
{
    public class AgileZenProject
    {
        public string CreateTime { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public AgileZenUser Owner { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

