using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutomationExcercise.Utilities
{
    public class JsonDataReader
    {
        public static T LoadJson<T>(string filePath)
        {
            try
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException($"The file at {fullPath} does not exist.");
                }
                string jsonContent = File.ReadAllText(fullPath);
                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    throw new InvalidDataException($"{filePath} Json content is empty or null");
                }
                return JsonConvert.DeserializeObject<T>(jsonContent);
            }
            catch (JsonReaderException jrex)
            {
                throw new InvalidDataException($"Failed to parse json from {filePath}: {jrex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading json test data from {filePath}: {ex.Message}", ex);
            }
        }   
    }
}
