using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;

namespace Itn.Utilities
{
    public class JsonFormatConverter
    {

        public static string Serialize<T>(T dataToSerialize, bool enumToString)
        {
            var settings = new JsonSerializerSettings();
            if (enumToString)
            {
                settings.Converters.Add(new StringEnumConverter());
            }
            return JsonConvert.SerializeObject(dataToSerialize, settings);
        }

        public static string Serialize<T>(T dataToSerialize)
        {
            return Serialize(dataToSerialize, false);
        }

        public static void Serialize<T>(T dataToSerialize, string filePath)
        {
            var jsonString = Serialize(dataToSerialize, false);
            File.WriteAllText(filePath, jsonString);
        }

        public static void Serialize<T>(T dataToSerialize, string filePath, bool enumToString)
        {
            var jsonString = Serialize(dataToSerialize, enumToString);
            File.WriteAllText(filePath, jsonString);
        }

        public bool CanDeserialize<T>(string jsonString)
        {
            try
            {
                JsonConvert.DeserializeObject<T>(jsonString);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static T Deserialize<T>(string jsonString, JsonConverter converter)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, converter);
        }

        public static T DeserializeFromFile<T>(string filePath, JsonConverter converter)
        {
            var jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonString, converter);
        }

        public static T DeserializeFromFile<T>(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
