using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CarServicesSystem
{
    public class SafeLoad<T>
    {
        public string FilePath { get; set; }

        public SafeLoad(string filePath)
        {
            FilePath = filePath;
        }

        public void Save(List<T> data)
        {
            var dir = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        public List<T> Load()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<T>>(json) ?? new();
            }
            return new List<T>();
        }
    }
}