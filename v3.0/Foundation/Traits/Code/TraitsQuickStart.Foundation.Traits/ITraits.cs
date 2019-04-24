/********************************************************************************
 * Traits QuickStart Data Object Library 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Traits
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface implemented by Data Transfer Objects for configuration pairs
    /// </summary>
    public interface ITraits
    {
        /// <summary>
        /// Traits Format Version
        /// </summary>
        string Version { get; }
        /// <summary>
        /// These are pairs of items that make your instance unique.
        /// </summary>
        Dictionary<string, string> TraitPairs { get; set; }
    }
}
