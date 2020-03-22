using System.Configuration;

namespace Common.Managers
{
    /// <summary>
    /// Interaction logic for the configuration access manager class.
    /// </summary>
    public static class ConfigurationAccessManager
    {
        #region Methods

        /// <summary>
        /// Gets a specified application setting's value from the configuration.
        /// </summary>
        /// <param name="key">The key for the application setting.</param>
        /// <returns>The value for the application setting.</returns>
        public static string GetApplicationSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Gets a specified connection string's value from the configuration.
        /// </summary>
        /// <param name="name">The name for the connection string.</param>
        /// <returns>The value for the connection string.
        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        #endregion Methods
    }
}
