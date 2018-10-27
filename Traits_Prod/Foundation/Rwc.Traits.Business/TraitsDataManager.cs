/********************************************************************************
 * CSHARP Traits Data Object Library 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace Rwc.Traits.Business
{
    using CSHARPStandard.Data.Common;
    using CSHARPStandard.Diagnostics;
    using Rwc.Traits.Data;
    using System;

    /// <summary>
    /// Base implemention of all Data Reader Providers
    /// </summary>
    public class TraitsDataManager : ITraitsDataReader, ITraitsDataWriter
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
        /// Applies the trait
        /// </summary>
        /// <param name="traitPair"></param>
        /// <returns></returns>
        public ICustomField ApplyTrait(ICustomField traitPair)
        {
            if (traitPair == null) throw new ArgumentNullException("ERROR: traitPair is required");
            if (Traits == null) throw new ArgumentNullException("ERROR: Traits is required");

            // The basic trait application simply uses the trait helper.
            var traitHelper = new TraitHelper();
            return traitHelper.ApplyTrait(Traits, traitPair);
        }
    }
}
