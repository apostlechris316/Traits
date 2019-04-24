/********************************************************************************
 * Traits Enabled Pipeline Library
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Pipelines
{
    /// <summary>
    /// Manages loading and saving pipelines to the proper location by provider loaded by Reflection Definition
    /// </summary>
    public class ReflectionDefinitionEnabledTraitsPipelineManager : TraitsPipelineManager, IReflectionDefinitionTraitsPipelineManager
    {
        /// <summary>
        /// Full path (or relative path to current directory) to folder containing the reflection definitions
        /// </summary>
        public string ReflectionDefinitionFolder { get; set; }
    }
}
