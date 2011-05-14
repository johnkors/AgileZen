using System;
using System.IO;
using System.Xml.Serialization;
using Lib.Services;

namespace Touch
{
	public class MonoObjectStore
	{
		private string _folderPath;
		
		public MonoObjectStore()		
		{
			_folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "iAgileZen");
			if(!Directory.Exists(_folderPath))
				Directory.CreateDirectory(_folderPath);			
		}
		
		public void Save<T>(T item, string fileName)
		{	
			var serializer = new JsonSerializer();
			
            using (var stream = new FileStream(Path.Combine(_folderPath, fileName), FileMode.Create))
            {
				var serialized = serializer.Serialize(item);
				var streamWriter = new StreamWriter(stream);
				streamWriter.Write(serialized);
				streamWriter.Close();
            }	
		}

		public T Load<T>(string fileName)
		{
			string path = Path.Combine(_folderPath, fileName);
			
			if(!File.Exists(path))
			    throw new FileNotFoundException("File not found: " + fileName);
					
			var serializer = new Lib.Services.JsonSerializer();
			T item = default(T);
            using (var stream = new FileStream(path, FileMode.Open))
            {
				item = serializer.Deserialize<T>(stream);
            } 
			
			return item;
		}

		public void Delete(string fileName)
		{
			string path = Path.Combine(_folderPath, fileName);
			if(File.Exists(path))
				File.Delete(path);
		}

		public bool FileExists(string fileName)
		{
			return File.Exists(Path.Combine(_folderPath, fileName));
		}
	}
}