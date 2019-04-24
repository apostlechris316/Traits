/********************************************************************************
 * Traits Enabled Pipeline Library
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Pipelines
{
    /// <summary>
    /// Interface implemented by all Traits Pipelines
    /// </summary>
    public interface IReflectionDefinitionTraitsPipelineManager : ITraitsPipelineManager
    {
        /// <summary>
        /// Full path (or relative path to current directory) to folder containing the reflection definitions
        /// </summary>
        string ReflectionDefinitionFolder { get; set; }
    }
}
