
namespace TraitsQuickStart.Foundation.TraitStorageProvider.File
{
    using JsonQuickStart;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using TraitsQuickStart.Foundation.Traits;

    /// <summary>
    /// Reads traits from a string
    /// </summary>
    public class FileTraitsProvider : BaseTraitsProvider
    {
        /// <summary>
        /// Loads traits from file specified in ConnectionString
        /// </summary>
        /// <returns></returns>
        public override bool LoadTraits()
        {
            if (string.IsNullOrEmpty(ConnectionString)) throw new ArgumentNullException("ERROR: ConnectionString property must include path to file containing traits");

            var absolutePath = Path.GetFullPath(ConnectionString);
            if (File.Exists(absolutePath) == false) throw new FileNotFoundException("ERROR: traits file not found at " + absolutePath);

            var json = File.ReadAllText(absolutePath);
            var jsonHelper = new JsonSerializationHelper();
            Traits = jsonHelper.DeserializeJsonObject(json, typeof(BaseTraits)) as ITraits;
            if (Traits == null) Traits = new BaseTraits();
            if (Traits.TraitPairs == null) Traits.TraitPairs = new Dictionary<string, string>();
            return true;
        }

        /// <summary>
        /// Saves traits back to file specified in ConnectionString
        /// </summary>
        /// <returns></returns>
        public override bool SaveTraits()
        {
            var jsonHelper = new JsonSerializationHelper();
            var json = jsonHelper.SerializeJsonObject(Traits, typeof(BaseTraits));
            File.WriteAllText(ConnectionString, json);
            return true;
        }
    }
}
