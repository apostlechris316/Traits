/********************************************************************************
 * Traits Reflection By Definition Library 
 * 
 * This library stores each reflection definition (assembly name and type name) in seperate definitions.
 * This makes it easier to swap them in and out as needed. However if you would rather manage them all 
 * in one file then you may wish to use Providers by Reflection instead.

 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitsQuickStart.Foundation.Reflection.ReflectionByDefinition
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Data Transfer Object containing settings necessary to load an object via reflection
    /// </summary>
    /// <remarks>NEW in v2.0.0.0<br/>
    /// v3.0.0.0 - Implements IReflectionDefinition
    /// </remarks>
    [DataContract]
    public class ReflectionDefinition : IReflectionDefinition
    {
        /// <summary>
        /// Full path to directory containing assembly and its related assemblies
        /// </summary>
        [DataMember]
        public string AssemblyPath { get; set; }

        /// <summary>
        /// Name of the assembly (including full namespace)
        /// </summary>
        [DataMember]
        public string AssemblyName { get; set; }

        /// <summary>
        /// Name of type to load from the assembly
        /// </summary>
        [DataMember]
        public string TypeName { get; set; }

        /// <summary>
        /// Additional information regarding data related to the object loaded
        /// </summary>
        [DataMember]
        public string DataSource { get; set; }

        /// <summary>
        /// If true, uses Activator when loading object via reflection
        /// </summary>
        [DataMember]
        public bool UseActivator { get; set; }
    }
}
