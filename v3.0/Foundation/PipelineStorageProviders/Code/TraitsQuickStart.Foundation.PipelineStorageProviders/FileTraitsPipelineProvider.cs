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
    using System.IO;
    using TraitsQuickStart.Foundation.Pipelines;

    /// <summary>
    /// Reads/Writes Traits Pipeline from a JSON File
    /// </summary>
    public class FileTraitsPipelineProvider : ITraitsPipelineReader, ITraitsPipelineWriter
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
        /// <returns></returns>
        public ITraitsPipeline LoadTraitsPipeline()
        {
            return LoadTraitsPipeline(TraitsPipelineType);
        }

        /// <summary>
        /// Loads the traits pipeline from text string using specified type.
        /// </summary>
        /// <param name="pipelineType">The DataType of the Pipeline (in case it is overrideing the default</param>
        /// <returns></returns>
        public ITraitsPipeline LoadTraitsPipeline(Type traitsPipelineType)
        {
            if (string.IsNullOrEmpty(ConnectionString)) throw new ArgumentNullException("ERROR: connectionString is required and should be the full path to the file containing the pipeline");

            var absolutePath = Path.GetFullPath(ConnectionString);
            if (File.Exists(absolutePath) == false) throw new FileNotFoundException("ERROR: file referenced in connectionString does not exist. " + absolutePath);
            if (traitsPipelineType == null) throw new ArgumentNullException("ERROR: traitsPipelineType is required");

            var pipelineJson = File.ReadAllText(absolutePath);
            var jsonSerializationHelper = new JsonSerializationHelper();
            return jsonSerializationHelper.DeserializeJsonObject(pipelineJson, traitsPipelineType) as ITraitsPipeline;
        }

        #endregion

        #region ITraitsPipelineWriter Related

        /// <summary>
        /// Saves the traits pipeline to file 
        /// </summary>
        /// <param name="traitsPipelineType">The type of object to write pipeline into</param>
        /// <returns></returns>
        public bool SaveTraitsPipeline(ITraitsPipeline traitsPipeline)
        {
            if (string.IsNullOrEmpty(ConnectionString)) throw new ArgumentNullException("ERROR: connectionString is required and should be the full path to the file containing the pipeline");
            var absolutePath = Path.GetFullPath(ConnectionString);

            if (TraitsPipelineType == null) TraitsPipelineType = typeof(TraitsPipeline);
            if (traitsPipeline == null) throw new ArgumentNullException("ERROR: traitsPipeline is required");

            var jsonSerializationHelper = new JsonSerializationHelper();
            File.WriteAllText(absolutePath, jsonSerializationHelper.SerializeJsonObject(traitsPipeline, TraitsPipelineType));
            return true;
        }

        #endregion
    }
}
