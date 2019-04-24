/********************************************************************************
 * Traits QuickStart
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Traits
{
    using DiagnosticsQuickStart.Business;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using TraitsQuickStart.Foundation.Reflection;
    using System.IO;

    /// <summary>
    /// Manages loading or saving traits 
    /// </summary>
    public class TraitsManager
    {
        /// <summary>
        /// Object To Store Status
        /// </summary>
        public IEventLog Status { get; set; }

        /// <summary>
        /// Full path to folder containing Trait storage providers
        /// </summary>
        public string TraitsStorageProviderPath { get; set; }

        /// <summary>
        /// Get traits from the give source
        /// </summary>
        /// <param name="sourceType">Type of source to get traits from eg. STRING, FILE</param>
        /// <param name="connectionString">Determines where to get traits</param>
        /// <returns></returns>
        public ITraits GetTraits(string sourceType, string connectionString)
        {
            if (string.IsNullOrEmpty(sourceType)) throw new ArgumentNullException("ERROR: sourceType is required to LoadTraits");
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("ERROR: connectionString is required to LoadTraits");
            if (string.IsNullOrEmpty(TraitsStorageProviderPath)) throw new ArgumentNullException("ERROR: TraitsStorageProviderPath is required to LoadTraits");

            var reflectionManager = new BaseReflectionManager();
            var absoluteTraitsStorageProviderPath = Path.GetFullPath(TraitsStorageProviderPath);
            var sourceDllPath = absoluteTraitsStorageProviderPath + "TraitsQuickStart.Foundation.TraitStorageProvider." + sourceType + ".dll";
            var typeName = "TraitsQuickStart.Foundation.TraitStorageProvider." + sourceType + "." + sourceType + "TraitsProvider";
            ITraitsStorageProvider traitsProvider = reflectionManager.LoadObject(sourceDllPath, typeName) as ITraitsStorageProvider;
            traitsProvider.ConnectionString = connectionString;
            traitsProvider.Status = Status;

            if (traitsProvider.LoadTraits() == false) return null;

            return traitsProvider.Traits;
        }

        /// <summary>
        /// Save traits to the give source
        /// </summary>
        /// <param name="sourceType">Type of source to get traits from eg. STRING, FILE</param>
        /// <param name="connectionString">Determines where to get traits</param>
        /// <returns></returns>
        public bool SaveTraits(string sourceType, string connectionString, ITraits traits)
        {
            if (string.IsNullOrEmpty(sourceType)) throw new ArgumentNullException("ERROR: sourceType is required is required to SaveTraits");
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("ERROR: connectionString is required to SaveTraits");
            if (string.IsNullOrEmpty(TraitsStorageProviderPath)) throw new ArgumentNullException("ERROR: TraitsStorageProviderPath is requried to SaveTraits");
            if (traits == null) throw new ArgumentNullException("ERROR: traits is required to SaveTraits");

            var reflectionManager = new BaseReflectionManager();
            var sourceDllPath = TraitsStorageProviderPath + "TraitsQuickStart.Foundation.TraitStorageProvider." + sourceType + ".dll";
            var typeName = "TraitsQuickStart.Foundation.TraitStorageProvider." + sourceType + "." + sourceType + "TraitsProvider";
            ITraitsStorageProvider traitsProvider = reflectionManager.LoadObject(sourceDllPath, typeName) as ITraitsStorageProvider;
            traitsProvider.ConnectionString = connectionString;
            traitsProvider.Status = Status;
            traitsProvider.Traits = traits;

            return traitsProvider.SaveTraits();
        }

        /// <summary>
        /// Gets all the variable traits from the traits pairs
        /// </summary>
        /// <param name="traits">Dain's traits</param>
        /// <returns></returns>
        public Dictionary<string, string> GetVariableTraits(ITraits traits)
        {
            if (traits == null) throw new ArgumentNullException("ERROR: traits must be passed in");

            var results = new Dictionary<string, string>();
            foreach(var trait in traits.TraitPairs.Where(t => t.Key.StartsWith("[[")))
            {
                results.Add(trait.Key, trait.Value);
            }

            return results;
        }

        /// <summary>
        /// Expand variables in the trait value
        /// </summary>
        /// <param name="traits">List of Traits</param>
        /// <param name="traitValue">The value of the trait</param>
        /// <returns></returns>
        public string ApplyTrait(ITraits traits, string traitValue)
        {
            if (traits == null) throw new ArgumentNullException("ERROR: traits must be passed in");

            // Create a copy of the trait that we will apply
            var appliedTrait = traitValue;

            // Get all the trait variables
            var variables = GetVariableTraits(traits);
            if (appliedTrait.Contains("[["))
            {
                foreach (var variable in variables)
                {
                    appliedTrait = appliedTrait.Replace(variable.Key, variable.Value);
                }
            }

            // if it still contains variables try a second pass
            if (appliedTrait.Contains("[["))
            {
                foreach (var variable in variables)
                {
                    appliedTrait = appliedTrait.Replace(variable.Key, variable.Value);
                }
            }

            // if it still contains variables try a second pass
            if (appliedTrait.Contains("[["))
            {
                throw new NotImplementedException("ERROR: triple nested variables are not currently supported.");
            }

            return appliedTrait;
        }
    }
}
