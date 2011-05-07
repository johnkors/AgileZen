using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using AgileZen.Lib;

namespace Lib.Services
{
    class AgileZenProjectParser : IParser<AgileZenProject>
    {
        public IEnumerable<AgileZenProject> Parse(XmlReader reader)
        {
            var xml = XDocument.Load(reader);
            var agileZenProjects = from c in xml.Elements("root")
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