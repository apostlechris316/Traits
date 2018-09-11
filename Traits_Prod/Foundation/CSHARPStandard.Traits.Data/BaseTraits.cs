/********************************************************************************
 * CSHARP Traits Data Object Library 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace CSHARPStandard.Traits.Data
{
    using CSHARPStandard.Data.Common;
    using System.Collections.Generic;

    /// <summary>
    /// Data Transfer Object for configuration pairs
    /// </summary>
    public class BaseTraits : ITraits
    {
        /// <summary>
        /// These are pairs of items that make Sitecore Dain unique.
        /// </summary>
        public List<ICustomField> TraitPairs { get { return _traitPairs; } }
        private List<ICustomField> _traitPairs = new List<ICustomField>();
    }
}
