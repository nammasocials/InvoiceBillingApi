using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using UtilitiesLayer.Models;

namespace UtilitiesLayer.Configuration
{
    public static class ConfigurationUtility
    {
        private static IConfigurationRoot Configuration { get; set; }

        static ConfigurationUtility()
        {
            LoadConfiguration();
        }

        private static void LoadConfiguration()
        {
            // Ensure SetBasePath is available by installing Microsoft.Extensions.Configuration.FileExtensions
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path to the current directory
                .AddJsonFile(ConfigurationConstants.appSettingsFileName, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public static string GetSetting(string key)
        {
            var environment = Configuration["Environment"];
            var Section = Configuration.GetSection(environment + ":" + key);
            //var EnvSection = Configuration.GetSection(key + ":" + environment);
            if (Section.Exists())
            {
                var sectionkey = environment + ":" + key;
                //var sectionkey = key + ":" + environment;
                return Configuration[sectionkey];
            }
            else
            {
                return Configuration[key];
            }
        }

        public static T GetSection<T>(string section) where T : new()
        {
            var configSection = new T();
            var environment = Configuration["Environment"];

            var Section = Configuration.GetSection(environment + ":" + section);
            if (Section.Exists())
            {
                var key = environment + ":" + section;
                Configuration.GetSection(key).Bind(configSection);
            }
            else
            {
                Configuration.GetSection(section).Bind(configSection);
            }
            return configSection;
        }
    }
}
