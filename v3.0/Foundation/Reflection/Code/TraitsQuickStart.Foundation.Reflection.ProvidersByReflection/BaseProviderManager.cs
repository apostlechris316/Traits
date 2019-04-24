/********************************************************************************
 * TraitsQuickStarta Provider Reflection Library 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Reflection.ProvidersByReflection
{
    using DiagnosticsQuickStart.Business;
    using TraitsQuickStart.Foundation.Traits;

    /// <summary>
    /// Base class for all Data Provider Managers
    /// </summary>
    public class BaseProviderManager
    {
        /// <summary>
        /// Object to write status to
        /// </summary>
        public IEventLog Status { get; set; }

        /// <summary>
        /// Settings and configuration to use to load providers
        /// </summary>
        public ITraits Traits { get; set; }

        /// <summary>
        /// Path to load providers from
        /// </summary>
        public string ProviderRootPath { get; set; }

        /// <summary>
        /// Gets the manager responsible for loading the proper provider
        /// </summary>
        public void LoadProviderReflectionManager()
        {
            _providerReflectionManager = new ProviderReflectionManager
            {
                ProviderRootPath = ProviderRootPath,
                Traits = Traits
            };
        }
        private ProviderReflectionManager _providerReflectionManager = null;

        /// <summary>
        /// Loads the provider object based on the config
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="objectType">Type of object to load</param>
        /// <returns></returns>
        protected object LoadProvider(string providerName, string objectType)
        {
            if (_providerReflectionManager == null) LoadProviderReflectionManager();
            return _providerReflectionManager.LoadProvider(providerName, objectType);
        }

        /// <summary>
        /// Gets the data source for the provider
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="objectType"></param>
        /// <remarks>ASSUMES all trait variables already resolved</remarks>
        protected string GetProviderDataSource(string providerName, string objectType)
        {
            return _providerReflectionManager.GetProviderDataSource(providerName, objectType);
        }
    }
}