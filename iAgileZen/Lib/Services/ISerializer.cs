using System.IO;

namespace Lib.Services
{
    public interface ISerializer
    {
        string Serialize(object input);
        T Deserialize<T>(string input);
        T Deserialize<T>(Stream input);
    }
}