
using System.Threading.Tasks;

namespace XamarinCRM.Services
{
    /// <summary>
    /// An interface that fetches settings from embedded resources in a platform.
    /// </summary>
    public interface IConfigFetcher
    {
        /// <summary>
        /// Gets the specified config element value.
        /// </summary>
        /// <param name="configElementName">The XML element name in the config file.</param>
        /// <param name="readFromSensitiveConfig">If set to <c>true</c>, read the element from config-sensitive.xml. Otherwise, read from the config.xml.</param>
        /// <returns>The config value for the specified element.</returns>
        Task<string> GetAsync(string configElementName, bool readFromSensitiveConfig = false);
    }
}

