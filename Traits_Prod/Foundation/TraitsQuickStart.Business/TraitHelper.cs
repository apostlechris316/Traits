/********************************************************************************
 * Traits Business Library 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Business
{
    using JsonQuickStart;
    using StandardDataStructureQuickStart.Data;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using TraitsQuickStart.Data;

    /// <summary>
    /// Assists in manipulationg trait files both JSON and XML versions
    /// </summary>
    public class TraitHelper
    {
        /// <summary>
        /// Full path to the traits file.
        /// </summary>
        public string TraitPath { get; set; }

        /// <summary>
        /// The default is trait (trait.json) however you can put the actual filename to replace it.
        /// </summary>
        /// <remarks>When overriding only pass in the file name and not the extension. ".json" will automatically be appended</remarks>
        public string TraitFileName { get; set; }

        #region Trait Related

        /// <summary>
        /// Gets traits.
        /// </summary>
        /// <returns></returns>
        public ITraits GetTraits()
        {
            // Default the trait path to the same folder as the application
            if (string.IsNullOrEmpty(TraitPath)) TraitPath = AppDomain.CurrentDomain.BaseDirectory;

            // Ensure the directory path ends in a \\
            // (FROM: CSHARPStandard.IO.FileHelper.EnsureTrailingDirectorySeparator)
            TraitPath =  TraitPath + (!TraitPath.EndsWith("\\") ? "\\" : "");

            // Ensure the directory path exists 
            // (FROM: CSHARPStandard.IO.FileHelper.EnsureDirectoryExists)
            if (Directory.Exists(TraitPath) == false) Directory.CreateDirectory(TraitPath);

            var traitsJsonPath = TraitPath + (string.IsNullOrEmpty(TraitFileName) ? "traits" : TraitFileName) + ".json";


            // Set up the default traits json
            var traitsJson = "{\"traits\": { \"#comment\": [],\"trait\": [{\"fieldname\": \"TEST\",\"fieldvalue\": \"TEST_VALUE\"}]}}";

            // if JSON traits file does not exist then create one with defaults
            if (File.Exists(traitsJsonPath) == false)
            {
                // Write the xml to the file system as Json
                File.WriteAllText(traitsJsonPath, traitsJson);
            }

            // Read from the Json file
            traitsJson = File.ReadAllText(traitsJsonPath);
            var jsonHelper = new JsonSerializationHelper();
            var traits = jsonHelper.DeserializeJsonObject(traitsJson, typeof(BaseTraits)) as ITraits;
            if (traits == null) traits = new BaseTraits();
            if (traits.TraitPairs == null) traits.TraitPairs = new List<ICustomField>();

            return traits;
        }

        /// <summary>
        /// Gets all the variable traits from the traits pairs
        /// </summary>
        /// <param name="traits">Dain's traits</param>
        /// <returns></returns>
        public List<ICustomField> GetVariableTraits(ITraits traits)
        {
            if (traits == null) throw new ArgumentNullException("ERROR: traits must be passed in");

            return traits.TraitPairs.Where(t => t.FieldName.StartsWith("[[")).ToList();
        }

        /// <summary>
        /// Expand any variables in the pair value and apply
        /// </summary>
        /// <param name="traits">List of Traits</param>
        /// <param name="traitPair">The trait pair to apply</param>
        /// <returns></returns>
        public ICustomField ApplyTrait(ITraits traits, ICustomField traitPair)
        {
            if (traits == null) throw new ArgumentNullException("ERROR: traits must be passed in");
            if (traitPair == null) throw new ArgumentNullException("ERROR: traitPair must be passed in");

            // Create a copy of the trait that we will apply
            var appliedTrait = new CustomField(traitPair.FieldName, traitPair.FieldValue);

            // Get all the trait variables
            var variables = GetVariableTraits(traits);

            // There may be variables within the variables so we need to replace all those.
            var handlingNestedVariables = new List<ICustomField>();
            handlingNestedVariables.AddRange(variables);
            foreach(var variable in variables) foreach(var nestedVariable in handlingNestedVariables) nestedVariable.FieldValue = nestedVariable.FieldValue.Replace(variable.FieldName, variable.FieldValue);

            // Replaces all the trait variables
            foreach (var variable in handlingNestedVariables) appliedTrait.FieldValue = appliedTrait.FieldValue.Replace(variable.FieldName, variable.FieldValue);

            return appliedTrait;
        }

        /// <summary>
        /// Saves the traits to the JSON file
        /// </summary>
        /// <param name="traits"></param>
        /// <returns></returns>
        public bool SaveTraits(ITraits traits)
        {
            if (traits == null) throw new ArgumentNullException("ERROR: traits must be passed in");
            if (string.IsNullOrEmpty(TraitPath)) throw new FileNotFoundException("ERROR: DainRootFolder needs to be provided");


            // Make sure the folder exists. If it does not then create it.
            var traitsJsonFolder = TraitPath + (!TraitPath.EndsWith("\\") ? "\\" : "");
            if (Directory.Exists(traitsJsonFolder) == false) Directory.CreateDirectory(traitsJsonFolder);
            var traitsJsonPath = traitsJsonFolder + (string.IsNullOrEmpty(TraitFileName) ? "traits" : TraitFileName) + ".json";

            var jsonHelper = new JsonSerializationHelper();
            var traitJson = jsonHelper.SerializeJsonObject(traits, typeof(BaseTraits));

            // Write the trait json to the file system
            File.WriteAllText(traitsJsonPath, traitJson);

            return true;
        }

        #endregion

        #region Trait Template Related

        /// <summary>
        /// Loads the trait template from a Json File.
        /// </summary>
        /// <param name="traitTemplateJsonPath">Full path to trait template Json file</param>
        /// <returns></returns>
        public ITraits GetTraitTemplate(string traitTemplateJsonPath)
        {
            if (string.IsNullOrEmpty(traitTemplateJsonPath)) throw new ArgumentException("ERROR: traitTemplateJsonPath is required");

            // Read from the Json file
            var traitsJson = File.ReadAllText(traitTemplateJsonPath);
            if (string.IsNullOrEmpty(traitsJson)) throw new Exception("ERROR: trait json is empty in the file (" + traitTemplateJsonPath + ")");

            var jsonHelper = new JsonSerializationHelper();
            return jsonHelper.DeserializeJsonObject(traitsJson.ToUpperInvariant(), typeof(BaseTraits)) as ITraits;
        }

        #endregion
    }
}
