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
    /// Pipeline of providers executed based on a given set of rules
    /// </summary>
    [DataContract]
    /// TraitsPipelineStep
    public class TraitsPipelineProvider : ITraitsPipelineProvider
    {
        /// <summary>
        /// Settings that affect the behavior of the provider
        /// </summary>
        [DataMember]
        public Dictionary<string, string> ProviderSettings { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Returns true if this provider has a potential cost related to it.
        /// </summary>
        public bool IsMeteredProvider { get { return false; } }

        /// <summary>
        /// This determines how the pipeline handles errors. Possible values are:
        /// 
        ///     SKIP_ON_ERROR:                  If an error occurs in a provider it will log the errror and go onto the next step
        ///     STOP_ON_ERROR:                  If an error occurs all processing will stop and an error will be displayed.
        ///
        /// </summary>
        [DataMember]
        public string ErrorMode { get; set; }

        /// <summary>
        /// Validate The Provider based on content passed in. Skips call to APIs and returns a sample result that may not match content. Any analysis will be written to the AdditionalMeta on the results
        /// </summary>
        /// <param name="content">The content to process</param>
        /// <param name="verbose">If true, will write additional information for diagnostics</param>
        /// <returns></returns>
        public List<ITraitsResult> Validate(string content, bool verbose)
        {
            return Validate(string.Empty, content, verbose);
        }

        /// <summary>
        /// Validates the pipeline
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="connectionString"></param>
        /// <param name="verbose"></param>
        /// <returns></returns>
        public virtual List<ITraitsResult> Validate(string sourceType, string connectionString, bool verbose)
        {
            return null;
        }

        /// <summary>
        /// Executes the pipeline
        /// </summary>
        /// <param name="content">The content to process</param>
        /// <param name="verbose">If true, will write additional information for diagnostics</param>
        /// <returns></returns>
        public List<ITraitsResult> Process(string content, bool verbose)
        {
            return Process(string.Empty, content, verbose);
        }

        /// <summary>
        /// Executes the pipeline
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="connectionString"></param>
        /// <param name="verbose"></param>
        /// <returns></returns>
        public virtual List<ITraitsResult> Process(string sourceType, string connectionString, bool verbose)
        {
            return null;
        }
    }
}
