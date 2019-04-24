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
    /// Interface implemented by writers of TraitsPipelines
    /// </summary>
    public interface ITraitsPipelineWriter
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
        /// Saves the traits pipeline to text string
        /// </summary>
        /// <returns></returns>
        /// <remarks>connectionString is ref to support string provider where connectionstring is the storage</remarks>
        bool SaveTraitsPipeline(ITraitsPipeline traitsPipeline);
    }
}
