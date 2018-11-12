/********************************************************************************
 * Traits QuickStart Data Object Library 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Data
{
    using StandardDataStructureQuickStart.Data;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Data Transfer Object for configuration pairs
    /// </summary>
    [DataContract]
    [KnownType(typeof(CustomField))]
    public class BaseTraits : ITraits
    {
        /// <summary>
        /// These are pairs of items that make Sitecore Dain unique.
        /// </summary>
        [DataMember]
        public List<ICustomField> TraitPairs { get; set; } 
    }
}
