using System.IO;
using Newtonsoft.Json;

namespace Lib.Services
{
    class JsonSerializer : ISerializer
    {
        public string Serialize(object input)
        {
            return JsonConvert.SerializeObject(input);
        }

        public T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public T Deserialize<T>(Stream input)
        {
            using (var sr = new StreamReader(input))
            {
                return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
            }
        }
    }
}