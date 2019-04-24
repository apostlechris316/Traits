
namespace TraitsQuickStart.Foundation.TraitStorageProvider.String
{
    using JsonQuickStart;
    using System;
    using System.Collections.Generic;
    using TraitsQuickStart.Foundation.Traits;

    /// <summary>
    /// Reads traits from a string
    /// </summary>
    public class StringTraitsProvider : BaseTraitsProvider
    {
        /// <summary>
        /// Loads traits from string provided in ConnectionString
        /// </summary>
        /// <returns></returns>
        public override bool LoadTraits()
        {
            if (string.IsNullOrEmpty(ConnectionString)) throw new ArgumentNullException("ERROR: ConnectionString property must include containing traits in JSON format");

            var jsonHelper = new JsonSerializationHelper();
            Traits = jsonHelper.DeserializeJsonObject(ConnectionString, typeof(BaseTraits)) as ITraits;
            if (Traits == null) Traits = new BaseTraits();
            if (Traits.TraitPairs == null) Traits.TraitPairs = new Dictionary<string, string>();
            return true;
        }

        /// <summary>
        /// Saves traits back to string provided in ConnectionString
        /// </summary>
        /// <returns></returns>
        public override bool SaveTraits()
        {
            var jsonHelper = new JsonSerializationHelper();
            ConnectionString = jsonHelper.SerializeJsonObject(Traits, typeof(BaseTraits));
            return true;
        }
    }
}
