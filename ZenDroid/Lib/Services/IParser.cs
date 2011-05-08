using System.Collections.Generic;
using System.Xml;

namespace Lib.Services
{
    public interface IParser<T>
    {
        IEnumerable<T> Parse(XmlReader reader);
    }
}