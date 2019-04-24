/********************************************************************************
 * Traits Enabled Pipeline Library
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Pipelines
{
    using DiagnosticsQuickStart.Business;
    using System;
    using System.Collections.Generic;
    using TraitsQuickStart.Foundation.Reflection.ProvidersByReflection;
    using TraitsQuickStart.Foundation.Traits;

    /// <summary>
    /// Manages loading and saving pipelines to the proper location by provider loaded by ProviderByReflection
    /// </summary>
    public class TraitsPipelineManager : ITraitsPipelineManager
    {
        /// <summary>
        /// The object to log warnings and errors to
        /// </summary>
        public IEventLog Status { get; set; }

        /// <summary>
        /// Additional information necessary to process the pipeline
        /// </summary>
        public ITraits Traits { get; set; }

        /// <summary>
        /// The provider folder of the Langauge Detection Providers
        /// </summary>
        public string ProviderRootPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ITraitsPipeline Pipeline { get; set; }

        /// <summary>
        /// Type of object used for serialization.
        /// </summary>
        public Type PipelineObjectType { get; set; }

        #region GenerateTraitsPipeline Related

        /// <summary>
        /// Generates JSON to represent a HubAI Pipeline
        /// </summary>
        /// <returns></returns>
        public string GenerateTraitsPipeline(ITraitsPipeline pipeline, Type pipelineType)
        {
            var jsonSerializationHelper = new JsonQuickStart.JsonSerializationHelper();
            return jsonSerializationHelper.SerializeJsonObject(pipeline, pipelineType);
        }

        #endregion

        #region LoadTraitsPipeline Related

        /// <summary>
        /// Loads the Traits Pipeline using the default pipeline source type and connnection string from Traits
        /// </summary>
        /// <param name="pipelineTypePrefix">This is used in the traits call to prefix the pipeline settings eg. LANGUAGE</param>
        /// <returns></returns>
        public virtual bool LoadTraitsPipeline(string pipelineTypePrefix)
        {
            return LoadTraitsPipeline(pipelineTypePrefix, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineTypePrefix"></param>
        /// <param name="overridePipelineSettings"></param>
        /// <returns></returns>
        public virtual bool LoadTraitsPipeline(string pipelineTypePrefix, Dictionary<string, string> overridePipelineSettings)
        {
            var pipelineSourceTypeFieldName = pipelineTypePrefix + "_PIPELINE_SOURCE_TYPE";
            if (Traits.TraitPairs.ContainsKey(pipelineSourceTypeFieldName) == false && 
                overridePipelineSettings.ContainsKey(pipelineSourceTypeFieldName) == false)
            { 
                throw new ArgumentNullException("ERROR: " + pipelineSourceTypeFieldName + " is required in Traits or overridePipelineSettings");
            }

            var pipelineConnectionStringFieldName = pipelineTypePrefix + "_PIPELINE_CONNECTION_STRING";
            if (Traits.TraitPairs.ContainsKey(pipelineConnectionStringFieldName) == false &&
                overridePipelineSettings.ContainsKey(pipelineConnectionStringFieldName) == false)
            {
                throw new ArgumentNullException("ERROR: " + pipelineConnectionStringFieldName + " is required in Traits or overridePipelineSettings");
            }

            var pipelineSourceType = overridePipelineSettings.ContainsKey(pipelineSourceTypeFieldName)
                ? overridePipelineSettings[pipelineSourceTypeFieldName]
                : Traits.TraitPairs[pipelineSourceTypeFieldName];

            var pipelineConnectionString = overridePipelineSettings.ContainsKey(pipelineConnectionStringFieldName) 
                ? overridePipelineSettings[pipelineConnectionStringFieldName]
                : Traits.TraitPairs[pipelineConnectionStringFieldName];

            return LoadTraitsPipeline(pipelineTypePrefix, pipelineSourceType, pipelineConnectionString, overridePipelineSettings);
        }

        /// <summary>
        /// Loads the Traits Pipeline
        /// </summary>
        /// <param name="pipelineTypePrefix"></param>
        /// <param name="pipelineSourceType"></param>
        /// <param name="connectionString"></param>
        /// <param name="overridePipelineSettings"></param>
        /// <returns></returns>
        public virtual bool LoadTraitsPipeline(string pipelineTypePrefix, string pipelineSourceType, string connectionString, Dictionary<string, string> overridePipelineSettings)
        {
            var pipelineSettings = new Dictionary<string, string>();

            if (overridePipelineSettings != null)
            {
                // Copy all the pairs in if they are not overridden by the overridePipelineSettings
                foreach (var setting in overridePipelineSettings)
                {
                    if (pipelineSettings.ContainsKey(setting.Key) == false) pipelineSettings.Add(setting.Key, setting.Value);
                }
            }

            // Copy all the pairs in if they are not overridden by the overridePipelineSettings
            foreach (var traitPair in Traits.TraitPairs)
            {
                if(pipelineSettings.ContainsKey(traitPair.Key) == false) pipelineSettings.Add(traitPair.Key, traitPair.Value);
            }

            // Use reflection to load the pipeline storage provider
            var providerReflectionManager = new ProviderReflectionManager()
            {
                ProviderRootPath = ProviderRootPath,
                Traits = Traits
            };
            var traitsPipelineReader = providerReflectionManager.LoadProvider(pipelineSourceType, "PIPELINE_STORAGE_PROVIDER") as ITraitsPipelineReader;
            traitsPipelineReader.TraitsPipelineType = PipelineObjectType;
            traitsPipelineReader.ConnectionString = connectionString;
            Pipeline = traitsPipelineReader.LoadTraitsPipeline();

            // Ensure all providers have the provider settings
            foreach (var step in Pipeline.PipelineSteps)
            {
                // if provider is not loaded then we need to load it
                if (step.Provider == null)
                {
                    if (string.IsNullOrEmpty(step.ProviderName)) throw new NullReferenceException("Pipeline Steps must have a provider name");
                    step.Provider = providerReflectionManager.LoadProvider(step.ProviderName, pipelineTypePrefix + "_PROVIDER") as ITraitsPipelineProvider;
                }

                // Ensure each provider has traits
                if (step.Provider.ProviderSettings == null) step.Provider.ProviderSettings = new Dictionary<string, string>();
                foreach (var traitPair in Traits.TraitPairs)
                {
                    step.Provider.ProviderSettings.Add(traitPair.Key, traitPair.Value);
                }
            }

            return true;
        }

        #endregion

        #region SaveTratisPipeline Related

        /// <summary>
        /// Saves the Traits Pipeline using the default pipeline source type and connnection string from Traits
        /// </summary>
        /// <param name="pipelineTypePrefix">This is used in the traits call to prefix the pipeline settings eg. LANGUAGE</param>
        /// <returns></returns>
        public virtual bool SaveTraitsPipeline(string pipelineTypePrefix)
        {
            return SaveTraitsPipeline(pipelineTypePrefix, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineTypePrefix"></param>
        /// <param name="overridePipelineSettings"></param>
        /// <returns></returns>
        public virtual bool SaveTraitsPipeline(string pipelineTypePrefix, Dictionary<string, string> overridePipelineSettings)
        {
            var pipelineSourceTypeFieldName = pipelineTypePrefix + "_PIPELINE_SOURCE_TYPE";
            if (Traits.TraitPairs.ContainsKey(pipelineSourceTypeFieldName)  == false &&
                overridePipelineSettings.ContainsKey(pipelineTypePrefix + "_PIPELINE_SOURCE_TYPE") == false)
            {
                throw new ArgumentNullException("ERROR: " + pipelineSourceTypeFieldName + " is required in Traits or overridePipelineSettings");
            }

            var pipelineConnectionStringFieldName = pipelineTypePrefix + "_PIPELINE_CONNECTION_STRING";
            if (Traits.TraitPairs.ContainsKey(pipelineConnectionStringFieldName) == false &&
                overridePipelineSettings.ContainsKey(pipelineTypePrefix + "_PIPELINE_CONNECTION_STRING") == false)
            {
                throw new ArgumentNullException("ERROR: " + pipelineConnectionStringFieldName + " is required in Traits or overridePipelineSettings");
            }

            var pipelineSourceType = overridePipelineSettings.ContainsKey(pipelineSourceTypeFieldName)
                ? overridePipelineSettings[pipelineSourceTypeFieldName]
                : Traits.TraitPairs[pipelineTypePrefix];

            var pipelineConnectionString = overridePipelineSettings.ContainsKey(pipelineConnectionStringFieldName)
                ? overridePipelineSettings[pipelineConnectionStringFieldName]
                : Traits.TraitPairs[pipelineConnectionStringFieldName];

            return SaveTraitsPipeline(pipelineTypePrefix, pipelineSourceType, pipelineConnectionString, overridePipelineSettings);
        }

        /// <summary>
        /// Save the Traits Pipeline
        /// </summary>
        /// <param name="pipelineTypePrefix"></param>
        /// <param name="pipelineSourceType"></param>
        /// <param name="connectionString"></param>
        /// <param name="overridePipelineSettings"></param>
        /// <returns></returns>
        public virtual bool SaveTraitsPipeline(string pipelineTypePrefix, string pipelineSourceType, string connectionString, Dictionary<string, string> overridePipelineSettings)
        {
            if (string.IsNullOrEmpty(ProviderRootPath)) throw new ArgumentNullException("ERROR: providerRootPath is required");

            var pipelineSettings = new Dictionary<string, string>();

            if (overridePipelineSettings != null)
            {
                // Copy all the pairs in if they are not overridden by the overridePipelineSettings
                foreach (var setting in overridePipelineSettings)
                {
                    if (pipelineSettings.ContainsKey(setting.Key) == false) pipelineSettings.Add(setting.Key, setting.Value);
                }
            }

            // Use reflection to load the pipeline storage provider
            var providerReflectionManager = new ProviderReflectionManager()
            {
                Traits = Traits,
                ProviderRootPath = ProviderRootPath
            };
            var traitsPipelineWriter = providerReflectionManager.LoadProvider(pipelineSourceType, "PIPELINE_STORAGE_PROVIDER") as ITraitsPipelineWriter;
            traitsPipelineWriter.ConnectionString = connectionString;

            return traitsPipelineWriter.SaveTraitsPipeline(Pipeline);
        }

        /// <summary>
        /// Save the Traits Pipeline
        /// </summary>
        /// <param name="pipelineSourceType"></param>
        /// <param name="connectionString"></param>
        /// <param name="pipelineType"></param>
        /// <param name="overridePipelineSettings"></param>
        /// <returns></returns>
        public virtual bool SaveTraitsPipeline(string pipelineTypePrefix, string pipelineSourceType, string connectionString, Type traitsPipelineType, Dictionary<string, string> overridePipelineSettings)
        {
            var pipelineSettings = new Dictionary<string, string>();

            if (overridePipelineSettings != null)
            {
                // Copy all the pairs in if they are not overridden by the overridePipelineSettings
                foreach (var setting in overridePipelineSettings)
                {
                    if (pipelineSettings.ContainsKey(setting.Key) == false) pipelineSettings.Add(setting.Key, setting.Value);
                }
            }

            // Use reflection to load the pipeline storage provider
            var providerReflectionManager = new ProviderReflectionManager();
            var traitsPipelineWriter = providerReflectionManager.LoadProvider(pipelineSourceType, "PIPELINE_STORAGE_PROVIDER") as ITraitsPipelineWriter;
            traitsPipelineWriter.ConnectionString = connectionString;

            return traitsPipelineWriter.SaveTraitsPipeline(Pipeline);
        }

        #endregion

        /// <summary>
        /// Executes all the relevant steps in the traits pipeline
        /// </summary>
        /// <param name="content">Content to process</param>
        /// <param name="pipeline">The pipeline to execute</param>
        /// <param name="pipelineContext">Additional information necessary to execute the pipeline</param>
        /// <param name="verbose">If true, will write additional information for diagnostics</param>
        /// <returns>A list of validation issues</returns>
        public virtual List<ITraitsResult> ValidateTraitsPipeline(string content, bool verbose)
        {
            return ValidateTraitsPipeline(string.Empty, content, verbose);
        }

        /// <summary>
        /// Executes all the relevant steps in the traits pipeline
        /// </summary>
        /// <param name="sourceType">Source of the content (eg. STRING, FILE, etc)</param>
        /// <param name="connectionString">Where to get the content</param>
        /// <param name="verbose">If true, will write additional information for diagnostics</param>
        /// <returns>A list of validation issues</returns>
        public virtual List<ITraitsResult> ValidateTraitsPipeline(string sourceType, string connectionString, bool verbose)
        {
            throw new NotImplementedException("ERROR: The base pipeline manager does not implment validate");
        }

        /// <summary>
        /// Executes all the relevant steps in the traits pipeline
        /// </summary>
        /// <param name="pipeline">The pipeline to execute</param>
        /// <param name="pipelineContext">Additional information necessary to execute the pipeline</param>
        /// <param name="verbose">If true, will write additional information for diagnostics</param>
        /// <returns></returns>
        public virtual List<ITraitsResult> ExecuteTraitsPipeline(string content, bool verbose)
        {
            return ExecuteTraitsPipeline(string.Empty, content, verbose);
        }

        /// <summary>
        /// Executes the pipeline to determine results
        /// </summary>
        /// <param name="text">Base64Encoded Content to process</param>
        /// <param name="disableMetered">If true, will override the PipelineExecutionMode and disable executing metered providers. If false will honor the settings in PipelineExecutionMode 
        /// <param name="enableDiagnostics">If true, will write diagnostics in the AdditionalMeta of the results</param>
        /// <returns>possible results with weightings</returns>
        public virtual List<ITraitsResult> ExecuteTraitsPipeline(string sourceType, string connectionString, bool verbose)
        {
            throw new NotImplementedException("ERROR: The base pipeline manager does not implment execute");
        }
    }
}
