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
    using TraitsQuickStart.Foundation.Traits;

    /// <summary>
    /// Interface implemented by all Traits Pipelines
    /// </summary>
    public interface ITraitsPipelineManager
    {
        /// <summary>
        /// The object to log warnings and errors to
        /// </summary>
        IEventLog Status { get; set; }

        /// <summary>
        /// Settings that affect the behavior of the pipeline
        /// </summary>
        ITraits Traits { get; set; }

        /// <summary>
        /// The provider folder of the Langauge Detection Providers
        /// </summary>
        string ProviderRootPath { get; set; }

        /// <summary>
        /// The traits pipeline
        /// </summary>
        ITraitsPipeline Pipeline { get; set; }

        /// <summary>
        /// Type of object used for serialization.
        /// </summary>
        Type PipelineObjectType { get; set; }

        #region ValidateTraitsPipeline Related

        /// <summary>
        /// Validate the pipeline. This includes calling Validate on all the applicable providers in pipeline steps. 
        /// Skips call to APIs and returns a sample result that may not match content. Any analysis will be written to the AdditionalMeta on the results
        /// </summary>
        /// <param name="content">Content to process</param>
        /// <param name="verbose">If true, will write additional information for diagnostics</param>
        /// <returns>A list of validation issues</returns>
        List<ITraitsResult> ValidateTraitsPipeline(string content, bool verbose);

        /// <summary>
        /// Executes the pipeline to determine results
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="connectionString">Content to process</param>
        /// <param name="verbose">If true, will write additional information for diagnostics</param>
        /// <returns>A list of validation issues</returns>
        List<ITraitsResult> ValidateTraitsPipeline(string sourceType, string connectionString, bool verbose);

        #endregion

        #region LoadTraitsPipeline Related

        /// <summary>
        /// Loads the Traits Pipeline using the default pipeline source type and connnection string from Traits
        /// </summary>
        /// <param name="pipelineTypePrefix">This is used in the traits call to prefix the pipeline settings eg. LANGUAGE</param>
        /// <returns></returns>
        bool LoadTraitsPipeline(string pipelineTypePrefix);

        /// <summary>
        /// Loads the Traits Pipeline
        /// </summary>
        /// <param name="pipelineSourceType"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        bool LoadTraitsPipeline(string pipelineTypePrefix, Dictionary<string, string> overridePipelineSettings);

        /// <summary>
        /// Loads the Traits Pipeline
        /// </summary>
        /// <param name="pipelineSourceType"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        bool LoadTraitsPipeline(string pipelineTypePrefix, string pipelineSourceType, string connectionString, Dictionary<string, string> overridePipelineSettings);

        #endregion

        #region SaveTraitsPipeline Related
        /// <summary>
        /// Saves the Traits Pipeline using the default pipeline source type and connnection string from Traits
        /// </summary>
        /// <param name="pipelineTypePrefix">This is used in the traits call to prefix the pipeline settings eg. LANGUAGE</param>
        /// <returns></returns>
        bool SaveTraitsPipeline(string pipelineTypePrefix);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineTypePrefix"></param>
        /// <param name="overridePipelineSettings"></param>
        /// <returns></returns>
        bool SaveTraitsPipeline(string pipelineTypePrefix, Dictionary<string, string> overridePipelineSettings);

        /// <summary>
        /// Save the Traits Pipeline
        /// </summary>
        /// <param name="pipelineTypePrefix"></param>
        /// <param name="pipelineSourceType"></param>
        /// <param name="connectionString"></param>
        /// <param name="overridePipelineSettings"></param>
        /// <returns></returns>
        bool SaveTraitsPipeline(string pipelineTypePrefix, string pipelineSourceType, string connectionString, Dictionary<string, string> overridePipelineSettings);

        /// <summary>
        /// Save the Traits Pipeline
        /// </summary>
        /// <param name="pipelineSourceType"></param>
        /// <param name="connectionString"></param>
        /// <param name="pipelineType"></param>
        /// <param name="overridePipelineSettings"></param>
        /// <returns></returns>
        bool SaveTraitsPipeline(string pipelineTypePrefix, string pipelineSourceType, string connectionString, Type traitsPipelineType, Dictionary<string, string> overridePipelineSettings);

        #endregion

        /// <summary>
        /// Executes all the relevant steps in the traits pipeline
        /// </summary>
        /// <returns></returns>
        List<ITraitsResult> ExecuteTraitsPipeline(string connectionString, bool verbose);

        /// <summary>
        /// Executes the pipeline to determine results
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="connectionString">Content to process</param>
        /// <param name="verbose">If true, will write additional information for diagnostics</param>
        /// <returns>possible results with weightings</returns>
        List<ITraitsResult> ExecuteTraitsPipeline(string sourceType, string connectionString, bool verbose);
    }
}
