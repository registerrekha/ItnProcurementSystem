using Itn.Utilities;
using System.IO;

namespace Itn.Shared.Tests.ModelGeneratorsTest
{
    public abstract class BaseModelGenerator
    {
        internal void Write(string flName, object t)
        {
            File.WriteAllText(
                string.Format("{0}{1}",
                    AppConfig.GetConfigVal("Data.Test.Canonical.Output.Path"),
                    flName),
                JsonFormatConverter.Serialize(t));
        }

        internal T Read<T>(string flName)
        {
            var flPath = string.Format("{0}{1}",
                AppConfig.GetConfigVal("Data.Test.Canonical.Output.Path"),
                flName);
            return JsonFormatConverter.DeserializeFromFile<T>(flPath);
        }

        internal string ReadFileContents(string flName)
        {
            var flPath = string.Format("{0}{1}",
                AppConfig.GetConfigVal("Data.Test.Canonical.Output.Path"),
                flName);
            return File.ReadAllText(flPath);
        }
    }
}
