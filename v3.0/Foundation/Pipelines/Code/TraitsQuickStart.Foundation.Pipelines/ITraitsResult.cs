/********************************************************************************
 * Traits Enabled Pipeline Library
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

using System.Collections.Generic;

namespace TraitsQuickStart.Foundation.Pipelines
{
    /// <summary>
    /// Interface implemented by a result from Traits Execution Providers
    /// </summary>
    public interface ITraitsResult
    {
        /// <summary>
        /// The type of result returned eg. LANGUAGE_DETECTION, IMAGE_TAGGING, SENTIMENT, etc
        /// </summary>
        string ResultType { get; set; }

        /// <summary>
        /// This is used in case different providers return different JSON formats for the same Result Type
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// The result in JSON format
        /// </summary>
        string Result { get; set; }

        /// <summary>
        /// This number determines how likely we feel this is best result
        /// </summary>
        double ProbabilityWeight { get; set; }

        /// <summary>
        /// This is additional inforamation that describes how the result was obtained and if there are any other information known.
        /// </summary>
        Dictionary<string, string> AdditionalMeta { get; set; }
    }
}
