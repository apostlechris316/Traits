/********************************************************************************
 * Traits Enabled Pipeline Library
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Pipelines
{
    using System;

    /// <summary>
    /// Interface implemented by readers of TraitsPipelines
    /// </summary>
    public interface ITraitsPipelineReader
    {
        /// <summary>
        /// Determines how to connect with 
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Determines the type used to load the pipeline default is TraitsPipeline
        /// </summary>
        Type TraitsPipelineType { get; set; }

        /// <summary>
        /// Loads the traits pipeline
        /// </summary>
        /// <returns></returns>
        ITraitsPipeline LoadTraitsPipeline();
    }
}
