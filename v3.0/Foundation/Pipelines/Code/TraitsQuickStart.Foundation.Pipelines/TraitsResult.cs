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
    /// Traits Execution Result
    /// </summary>
    [DataContract]
    public class TraitsResult : ITraitsResult
    {
        #region ITraitsExecutionResult Implementation

        /// <summary>
        /// This is used in case different providers return different JSON formats for the same Result Type
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// The type of result returned eg. LANGUAGE_DETECTION, IMAGE_TAGGING, SENTIMENT, etc
        /// </summary>
        public string ResultType { get; set; } = "TRAITS_GENERAL_RESULT";

        /// <summary>
        /// The result in JSON format
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// This number determines how likely we feel this is best result
        /// </summary>
        public double ProbabilityWeight { get; set; }

        /// <summary>
        /// This is additional meta data that describes how the result was obtained and if there are any other information known.
        /// </summary>
        public Dictionary<string, string> AdditionalMeta { get; set; } = new Dictionary<string, string>();

        #endregion
    }
}
