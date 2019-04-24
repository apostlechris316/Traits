/********************************************************************************
 * Traits Enabled Pipeline Library
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.PipelineStorageProviders
{
    using JsonQuickStart;
    using System;
    using TraitsQuickStart.Foundation.Pipelines;

    /// <summary>
    /// Reads/Writes Traits Pipeline from a JSON string
    /// </summary>
    public class StringTraitsPipelineProvider : ITraitsPipelineReader, ITraitsPipelineWriter
    {
        /// <summary>
        /// Determines how to connect with 
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Determines the type used to load the pipeline default is TraitsPipeline
        /// </summary>
        public Type TraitsPipelineType { get; set; } = typeof(TraitsPipeline);

        #region ITraitsPipelineReader Related 

        /// <summary>
        /// Loads the traits pipeline from text string using TraitsPipeline
        /// </summary>
        /// <param name="connectionString">Where to read the traits pipeline from</param>
        /// <returns></returns>
        public ITraitsPipeline LoadTraitsPipeline()
        {
            if (string.IsNullOrEmpty(ConnectionString)) throw new ArgumentNullException("ERROR connectionString is required");

            var jsonSerializationHelper = new JsonSerializationHelper();
            return jsonSerializationHelper.DeserializeJsonObject(ConnectionString, TraitsPipelineType) as ITraitsPipeline;
        }

        #endregion

        #region ITraitsPipelineWriter Related

        /// <summary>
        /// Saves the traits pipeline to text string
        /// </summary>
        /// <param name="connectionString">Not applicable for Memory</param>
        /// <returns></returns>
        public bool SaveTraitsPipeline(ITraitsPipeline traitsPipeline)
        {
            if (traitsPipeline == null) throw new ArgumentNullException("ERROR: traitsPipeline is required");

            var jsonSerializationHelper = new JsonSerializationHelper();
            ConnectionString = jsonSerializationHelper.SerializeJsonObject(traitsPipeline, TraitsPipelineType);
            return true;
        }

        #endregion
    }
}
