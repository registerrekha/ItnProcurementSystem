using System.Configuration;
namespace Itn.Utilities
{
    public class AppConfig
    {

        public static string GetConfigVal(string configKey)
        {
            var result = GetConfigValue(configKey);
            if (result == null)
            {
                throw new AppConfigKeyDoesNotExistException("{0} does not appear to exist in app configuration!", configKey);
            }
            return result;
        }

        private static string GetConfigValue(string configKey)
        {
            return ConfigurationManager.AppSettings[configKey];
        }

        public static bool ConfigExists(string configKey)
        {
            return (GetConfigValue(configKey) != null);
        }

        public static bool GetConfigValBool(string configKey)
        {
            var configVal = GetConfigVal(configKey).Trim().ToUpper();
            return configVal.ToBool();
        }
    }
}
