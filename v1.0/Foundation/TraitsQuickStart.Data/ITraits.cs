/********************************************************************************
 * Traits QuickStart Data Object Library 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Data
{
    using StandardDataStructureQuickStart.Data;
    using System.Collections.Generic;

    /// <summary>
    /// Interface implemented by Data Transfer Objects for configuration pairs
    /// </summary>
    public interface ITraits
    {
        /// <summary>
        /// These are pairs of items that make your instance unique.
        /// </summary>
        List<ICustomField> TraitPairs { get; set; }
    }
}
