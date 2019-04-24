/********************************************************************************
 * Traits QuickStart Data Object Library 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Traits
{
    using DiagnosticsQuickStart.Business;

    /// <summary>
    /// Base class for all Traits Storage Providers
    /// </summary>
    public class BaseTraitsProvider : ITraitsStorageProvider
    {
        /// <summary>
        /// Object To Store Status
        /// </summary>
        public IEventLog Status { get; set; }

        /// <summary>
        /// Settings and configuration to use to load providers
        /// </summary>
        public ITraits Traits { get; set; }

        /// <summary>
        /// Information needed to connect to this crm data source
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Loads the traits from the given source
        /// </summary>
        /// <returns></returns>
        public virtual bool LoadTraits()
        {
            return false;
        }

        /// <summary>
        /// Save Traits to the given location
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveTraits()
        {
            return false;
        }

    }
}
