/********************************************************************************
 * CSHARP Traits Data Object Library 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace Rwc.Traits.Data
{
    using CSHARPStandard.Diagnostics;

    /// <summary>
    /// Interface implemented by all Data Writer Providers
    /// </summary>
    public interface ITraitsDataWriter
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
    }
}
