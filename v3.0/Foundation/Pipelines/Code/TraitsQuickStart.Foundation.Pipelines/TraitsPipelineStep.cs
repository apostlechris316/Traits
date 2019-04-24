/********************************************************************************
 * Traits Enabled Pipeline Library
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Pipelines
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Step within a Pipeline of providers executed based on a given set of rules
    /// </summary>
    [DataContract]
    public class TraitsPipelineStep : ITraitsPipelineStep
    {
        /// <summary>
        /// Name of provider to execute.
        /// </summary>
        [DataMember]
        public string ProviderName { get; set; }

        /// <summary>
        /// The provider weight is applied to the language ProbabilityWeight to reflect the importance given to this provider in determining the result.
        /// </summary>
        public double ProviderWeight { get; set; }

        /// <summary>
        /// Additional settings needed to use this provider. For example an API Key.
        /// </summary>
        public Dictionary<string, string> ProviderSettings { get; set; }

        /// <summary>
        /// A list of rules in JSON format that determine when to run it and other factors that would affect the execution
        /// </summary>
        public List<string> Rules { get; set; }

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
        public string ErrorMode { get; set; }

        /// <summary>
        /// Provider to execute
        /// </summary>
        public ITraitsPipelineProvider Provider { get; set; }
    }
}
