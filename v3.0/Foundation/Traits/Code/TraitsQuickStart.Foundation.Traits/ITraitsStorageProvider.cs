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
    /// Interface implemented by all Traits Storage Providers
    /// </summary>
    public interface ITraitsStorageProvider
    {
        /// <summary>
        /// Object To Store Status
        /// </summary>
        IEventLog Status { get; set; }

        /// <summary>
        /// Settings and configuration to use to load providers
        /// </summary>
        ITraits Traits { get; set; }

        /// <summary>
        /// Information needed to connect to this crm data source
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Loads the traits from the given source
        /// </summary>
        /// <returns></returns>
        bool LoadTraits();

        /// <summary>
        /// Save Traits to the given location
        /// </summary>
        /// <returns></returns>
        bool SaveTraits();
    }
}
