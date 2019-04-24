/********************************************************************************
 * Traits Enabled Pipeline Library
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Pipelines
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface implemented by all Traits Pipelines
    /// </summary>
    public interface ITraitsPipeline
    {
        /// <summary>
        /// If empty uses default pipeline storage format otherwise contains the name of the type used for serialization
        /// </summary>
        string PipelineFormatType { get; set; }

        /// <summary>
        /// Used to determine if this is an older version of the pipeline and provide automatic upgrade
        /// </summary>
        string PipelineVersion { get; set; }

        /// <summary>
        /// This determines how pipeline steps are treated. Possible values are: 
        /// 
        ///     FIRST_MATCHING: The first step that is valid to 
        ///     ALL_MATCHING
        ///     FIRST_NON_METERED_MATCHING
        ///     ALL_NON_METERED_MATCHING
        ///     
        /// </summary>
        string PipelineExecutionMode { get; set; }

        /// <summary>
        /// This determines how the pipeline step handles errors. Possible values are:
        /// 
        ///     SKIP_ON_ERROR:                  If an error occurs in a provider it will log the errror and go onto the next step
        ///     STOP_ON_ERROR:                  If an error occurs all processing will stop and an error will be displayed.
        ///     USE_PIPELINE_STEP_ERROR_MODE:   If an error occurs it will look at the ErrorMode on the PipelineStep 
        ///                                     to determine the best response.
        /// 
        /// </summary>
        string ErrorMode { get; set; }

        /// <summary>
        /// Steps to execute in the pipeline
        /// </summary>
        List<ITraitsPipelineStep> PipelineSteps { get; set; }
    }
}
