/********************************************************************************
 * Traits Enabled Pipeline Library
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Pipelines
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface Implemented all Traits Providers
    /// </summary>
    public interface ITraitsPipelineProvider
    {
        /// <summary>
        /// Settings that affect the behavior of the provider
        /// </summary>
        Dictionary<string, string> ProviderSettings { get; set; }

        /// <summary>
        /// Returns true if this provider has a potential cost related to it.
        /// </summary>
        bool IsMeteredProvider { get; }

        /// <summary>
        /// This determines how the pipeline handles errors. Possible values are:
        /// 
        ///     SKIP_ON_ERROR:                  If an error occurs in a provider it will log the errror and go onto the next step
        ///     STOP_ON_ERROR:                  If an error occurs all processing will stop and an error will be displayed.
        ///
        /// </summary>
        string ErrorMode { get; set; }

        /// <summary>
        /// Validate The Provider based on content passed in. Skips call to APIs and returns a sample result that may not match content. Any analysis will be written to the AdditionalMeta on the results
        /// </summary>
        /// <param name="content">The content to process</param>
        /// <param name="verbose">If true, will write additional information for diagnostics</param>
        /// <returns></returns>
        List<ITraitsResult> Validate(string content, bool verbose);

        /// <summary>
        /// Validates the pipeline
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="connectionString"></param>
        /// <param name="verbose"></param>
        /// <returns></returns>
        List<ITraitsResult> Validate(string sourceType, string connectionString, bool verbose);

        /// <summary>
        /// Executes the pipeline
        /// </summary>
        /// <param name="content">The content to process</param>
        /// <param name="verbose">If true, will write additional information for diagnostics</param>
        /// <returns></returns>
        List<ITraitsResult> Process(string content, bool verbose);

        /// <summary>
        /// Executes the pipeline
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="connectionString"></param>
        /// <param name="verbose"></param>
        /// <returns></returns>
        List<ITraitsResult> Process(string sourceType, string connectionString, bool verbose);
    }
}
