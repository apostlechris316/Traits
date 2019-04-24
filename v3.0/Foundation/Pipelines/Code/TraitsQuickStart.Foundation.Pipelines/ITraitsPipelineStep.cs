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
    /// Interface implemented by all Traits Pipeline steps
    /// </summary>
    public interface ITraitsPipelineStep
    {
        /// <summary>
        /// Name of provider to execute.
        /// </summary>
        string ProviderName { get; set; }

        /// <summary>
        /// The provider weight is applied to the language ProbabilityWeight to reflect the importance given to this provider in determining the result.
        /// </summary>
        double ProviderWeight { get; set; }

        /// <summary>
        /// Additional settings needed to use this provider. For example an API Key.
        /// </summary>
        Dictionary<string, string> ProviderSettings { get; set; }

        /// <summary>
        /// A list of rules in JSON format that determine when to run it and other factors that would affect the execution
        /// </summary>
        List<string> Rules { get; set; }

        /// <summary>
        /// This determines how the pipeline step handles errors. Possible values are:
        /// 
        ///     SKIP_ON_ERROR:                  If an error occurs in a provider it will log the errror and go onto the next step
        ///     STOP_ON_ERROR:                  If an error occurs all processing will stop and an error will be displayed.
        ///     USE_ERROR_HANDLING_RULE:        If an error occurs it will look for the ERROR_HANDLER rule and will use that to 
        ///                                     determine the response to an error.
        ///     USE_PROVIDER_STEP_ERROR_MODE:   If an error occurs it will look at the ErrorMode on the Provider 
        ///                                     to determine the best response.
        /// </summary>
        string ErrorMode { get; set; }

        /// <summary>
        /// Provider to execute
        /// </summary>
        ITraitsPipelineProvider Provider { get; set; }
    }
}
