using System.IO;

namespace Lib.Services
{
    interface ISerializer
    {
        string Serialize(object input);
        T Deserialize<T>(string input);
        T Deserialize<T>(Stream input);
    }
}