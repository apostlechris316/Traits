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
    /// <summary>
    /// Interface implemented by all Reflection Definitions
    /// </summary>
    public interface IReflectionDefinition
    {
        /// <summary>
        /// Full path to directory containing assembly and its related assemblies
        /// </summary>
        string AssemblyPath { get; set; }

        /// <summary>
        /// Name of the assembly (including full namespace)
        /// </summary>
        string AssemblyName { get; set; }

        /// <summary>
        /// Name of type to load from the assembly
        /// </summary>
        string TypeName { get; set; }

        /// <summary>
        /// Additional information regarding data related to the object loaded
        /// </summary>
        string DataSource { get; set; }

        /// <summary>
        /// If true, uses Activator when loading object via reflection
        /// </summary>
        bool UseActivator { get; set; }
    }
}
