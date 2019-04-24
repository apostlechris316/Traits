/********************************************************************************
 * Traits QuickStart Data Object Library 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Traits
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Data Transfer Object for configuration pairs
    /// </summary>
    [DataContract]
    public class BaseTraits : ITraits
    {
        /// <summary>
        /// Traits Format Version
        /// </summary>
        [DataMember]
        public string Version { get; set; } = "3.0";

        /// <summary>
        /// These are pairs of items.
        /// </summary>
        [DataMember]
        public Dictionary<string, string> TraitPairs { get; set; } 
    }
}
