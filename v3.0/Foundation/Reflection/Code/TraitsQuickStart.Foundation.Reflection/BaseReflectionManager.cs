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

namespace TraitsQuickStart.Foundation.Reflection
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Base class for Reflection Managers. Provides functionality to loading an object of a given type from an assembly given full path to dll and type name.
    /// </summary>
    public class BaseReflectionManager
    {
        #region Taken From CSHARPStandard.Reflection 

        /// <summary>
        /// Loads an object from an assembly
        /// </summary>
        /// <param name="fullPathToDll">Full path to dll containing the Assembly</param>
        /// <param name="typeName">Type name to create</param>
        /// <returns></returns>
        public object LoadObject(string fullPathToDll, string typeName)
        {
            if (string.IsNullOrEmpty(fullPathToDll)) throw new ArgumentNullException("ERROR: fullPathToDll is manditory");
            if (string.IsNullOrEmpty(typeName)) throw new ArgumentNullException("ERROR: typeName is manditory");
            if (File.Exists(fullPathToDll) == false) throw new FileNotFoundException("ERROR: dll not found at: " + fullPathToDll);

            // Load the assembly
            var assembly = Assembly.LoadFrom(fullPathToDll);
            if (assembly == null) throw new NullReferenceException("ERROR: Assembly not loaded");

            // Get the type from name
            Type type = assembly.GetType(typeName, true);
            if (type == null) throw new NullReferenceException("ERROR: Type not found in assembly: " + typeName);

            // Create the instance
            return assembly.CreateInstance(typeName);
        }

        /// <summary>
        /// Loads an object from an assembly using Activator
        /// </summary>
        /// <param name="fullPathToDll"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        /// <remarks>This version looks up the type in the assembly first then uses activator to create the instance</remarks>
        public object LoadObjectUsingActivator(string fullPathToDll, string typeName)
        {
            if (string.IsNullOrEmpty(fullPathToDll)) throw new ArgumentNullException("ERROR: fullPathToDll is manditory");
            if (string.IsNullOrEmpty(typeName)) throw new ArgumentNullException("ERROR: typeName is manditory");
            if (File.Exists(fullPathToDll) == false) throw new FileNotFoundException("ERROR: dll not found at: " + fullPathToDll);

            // Load the assembly
            var assembly = Assembly.LoadFile(fullPathToDll);
            if (assembly == null) throw new NullReferenceException("ERROR: Assembly not loaded");

            // Get the type from name
            Type type = assembly.GetType(typeName, true);
            if (type == null) throw new NullReferenceException("ERROR: Type not found in assembly: " + typeName);

            // Create the instance
            return Activator.CreateInstance(type);
        }

        #endregion
    }
}
