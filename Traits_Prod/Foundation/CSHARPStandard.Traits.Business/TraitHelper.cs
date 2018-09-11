/********************************************************************************
 * CSHARP Traits Business Library 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace CSHARPStandard.Traits.Business
{
    using CSHARPStandard.Data;
    using CSHARPStandard.Data.Common;
    using CSHARPStandard.Data.Json;
    using CSHARPStandard.IO;
    using CSHARPStandard.Traits.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;

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

            var fileHelper = new FileHelper();
            TraitPath = fileHelper.EnsureTrailingDirectorySeparator(TraitPath);

            var jsonHelper = new JsonHelper();

            fileHelper.EnsureDirectoryExists(TraitPath);
            var traitsJsonPath = TraitPath + (string.IsNullOrEmpty(TraitFileName) ? "traits" : TraitFileName) + ".json";

            // Set up the default traits xml
            var traitsXml = string.Format("<traits><trait><fieldname>TraitsJsonPath</fieldname><fieldvalue>{0}</fieldvalue></trait></traits>", traitsJsonPath);

            // if JSON traits file does not exist then create one with defaults
            if (File.Exists(traitsJsonPath) == false)
            {
                // Read from the xml file if it exists
                var traitsXmlPath = traitsJsonPath.Replace(".json", ".xml");
                if (File.Exists(traitsXmlPath)) traitsXml = File.ReadAllText(traitsXmlPath.Replace(".json", ".xml")).Replace("<?xml version=\"1.0\" encoding=\"UTF - 8\"?>", "");

                // Write the xml to the file system as Json
                File.WriteAllText(traitsJsonPath, jsonHelper.XmlToJson(traitsXml));
            }

            // Read from the Json file
            string traitsJson = File.ReadAllText(traitsJsonPath);
            traitsXml = jsonHelper.JsonToXml(traitsJson, "root");
            var traitDataSet = DataSetHelper.ConvertXmlStringToDataSet(traitsXml, false);

            // Get all the raw traits
            ITraits traits = new BaseTraits();
            foreach (DataRow dataRow in traitDataSet.Tables["trait"].Rows) { traits.TraitPairs.Add(new CustomField(dataRow["fieldname"].ToString().ToUpperInvariant(), dataRow["fieldvalue"].ToString())); }

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

            var fileHelper = new FileHelper();
            TraitPath = fileHelper.EnsureTrailingDirectorySeparator(TraitPath);

            fileHelper.EnsureDirectoryExists(TraitPath);
            var traitsJsonPath = TraitPath + (string.IsNullOrEmpty(TraitFileName) ? "traits" : TraitFileName) + ".json";

            var jsonHelper = new JsonHelper();

            StringBuilder traitsXml = new StringBuilder();
            traitsXml.AppendLine("<traits>");
            foreach (ICustomField customField in traits.TraitPairs)
            {
                traitsXml.AppendFormat("<trait><fieldname>{0}</fieldname><fieldvalue>{1}</fieldvalue></trait>\r\n", customField.FieldName, customField.FieldValue);
            }
            traitsXml.AppendLine("</traits>");

            string traitJson = jsonHelper.XmlToJson(traitsXml.ToString());

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

            JsonHelper jsonHelper = new JsonHelper();

            // Read from the Json file
            var traitsJson = File.ReadAllText(traitTemplateJsonPath);
            if (string.IsNullOrEmpty(traitsJson)) throw new Exception("ERROR: trait json is empty in the file (" + traitTemplateJsonPath + ")");

            var traitsXml = jsonHelper.JsonToXml(traitsJson, "root");
            var traitDataSet = DataSetHelper.ConvertXmlStringToDataSet(traitsXml, false);

            // Get all the raw traits
            ITraits traits = new BaseTraits();
            foreach (DataRow dataRow in traitDataSet.Tables["trait"].Rows) { traits.TraitPairs.Add(new CustomField(dataRow["fieldname"].ToString().ToUpperInvariant(), dataRow["fieldvalue"].ToString())); }

            return traits;
        }

        #endregion
    }
}
