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
    using JsonQuickStart;
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Manages loading an object of a given type from an assembly using Reflection Definition files.
    /// </summary>
    public class ReflectionManager
    {
        /// <summary>
        /// Full path (or relative path to current directory) to folder containing the reflection definitions
        /// </summary>
        public string ReflectionDefinitionFolder { get; set; }

        /// <summary>
        /// Full path to the root folder for providers
        /// </summary>
        public string ProviderRootFolder { get; set; }

        /// <summary>
        /// Creates the reflection definition in the proper folder
        /// </summary>
        /// <param name="reflectionDefinitionName">Namwe of Reflection Definition</param>
        /// <param name="reflectionDefinition">Reflection definition to save</param>
        /// <returns></returns>
        public bool CreateReflectionDefinition(string reflectionDefinitionName, ReflectionDefinition reflectionDefinition)
        {
            if (string.IsNullOrEmpty(reflectionDefinitionName)) throw new ArgumentNullException("ERROR: reflectionDefinitionName is required");

            return CreateReflectionDefinition(reflectionDefinitionName, reflectionDefinition, typeof(ReflectionDefinition));
        }

        /// <summary>
        /// Creates the reflection definition in the proper folder
        /// </summary>
        /// <param name="reflectionDefinitionName">Namwe of Reflection Definition</param>
        /// <param name="reflectionDefinition">Reflection definition to save</param>
        /// <param name="reflectionDefinitionType">Type of reflection definition object required for serializing</param>
        /// <returns></returns>
        public bool CreateReflectionDefinition(string reflectionDefinitionName, IReflectionDefinition reflectionDefinition, Type reflectionDefinitionType)
        {
            if (string.IsNullOrEmpty(reflectionDefinitionName)) throw new ArgumentNullException("ERROR: reflectionDefinitionName is required");

            var jsonSerializationHelper = new JsonSerializationHelper();
            string json = jsonSerializationHelper.SerializeJsonObject(reflectionDefinition, typeof(ReflectionDefinition));
            File.WriteAllText(ReflectionDefinitionFolder + (ReflectionDefinitionFolder.EndsWith("\\") ? "" : "\\") + reflectionDefinitionName + ".json", json);
            return true;
        }

        /// <summary>
        /// Loads the reflection definition from the proper folder
        /// </summary>
        /// <param name="reflectionDefinitionName">Name of reflection definition to load</param>
        /// <returns></returns>
        public ReflectionDefinition LoadReflectionDefinition(string reflectionDefinitionName)
        {
            return LoadReflectionDefinition(reflectionDefinitionName, typeof(ReflectionDefinition)) as ReflectionDefinition;
        }

        /// <summary>
        /// Loads the reflection definition from the proper folder
        /// </summary>
        /// <param name="reflectionDefinitionName">Name of reflection definition to load</param>
        /// <param name="reflectionDefinitionType">Type of reflection definition object required for deserializing</param>
        /// <returns></returns>
        public IReflectionDefinition LoadReflectionDefinition(string reflectionDefinitionName, Type reflectionDefinitionType)
        { 
            // Load the file
            string json = File.ReadAllText(ReflectionDefinitionFolder + (ReflectionDefinitionFolder.EndsWith("\\") ? "" : "\\") + reflectionDefinitionName + ".json");
            var jsonSerializationHelper = new JsonSerializationHelper();
            return jsonSerializationHelper.DeserializeJsonObject(json, reflectionDefinitionType) as IReflectionDefinition;
        }

        /// <summary>
        /// Loads the object from the Reflection Definition
        /// </summary>
        /// <param name="reflectionDefinitionName"></param>
        /// <returns></returns>
        public object LoadObject(string reflectionDefinitionName)
        {
            // Ensure we have all we need
            if (ReflectionDefinitionFolder.StartsWith(".")) ReflectionDefinitionFolder = Path.GetFullPath(ReflectionDefinitionFolder);
            if (Directory.Exists(ReflectionDefinitionFolder) == false) throw new DirectoryNotFoundException("ERROR: The ReflectionDirectoryFolder " + ReflectionDefinitionFolder + " does not exist");
            if (string.IsNullOrEmpty(reflectionDefinitionName)) throw new ArgumentNullException("ERROR: reflectionDefintionName must be supplied");
            var reflectionDefinitionFile = Directory.GetFiles(ReflectionDefinitionFolder, reflectionDefinitionName + ".json");
            if (reflectionDefinitionFile.Length != 1) throw new FileNotFoundException("ERROR: reflectionDefinition for " + reflectionDefinitionName + " was not found in " + ReflectionDefinitionFolder);

            var reflectionDefinition = LoadReflectionDefinition(reflectionDefinitionName);
            if (reflectionDefinition == null) throw new ArgumentNullException("ERROR: reflectionDefintion for " + reflectionDefinitionName + " could not be loaded");

            // if assembly not found report error 
            if (string.IsNullOrEmpty(reflectionDefinition.AssemblyName)) throw new NullReferenceException("Assembly Name not found in reflection definition for: " + reflectionDefinitionName);

            // if type name not found report error
            if (string.IsNullOrEmpty(reflectionDefinition.TypeName)) throw new NullReferenceException("Type Name not found in reflection definition for:" + reflectionDefinitionName);

            // Handle relative assembly paths
            var assemblyPath = reflectionDefinition.AssemblyPath;

            var providerRootFolder = string.IsNullOrEmpty(ProviderRootFolder) ? Directory.GetCurrentDirectory() : ProviderRootFolder;
            if (assemblyPath.Contains("{PROVIDER_ROOT_PATH}")) assemblyPath = assemblyPath.Replace("{PROVIDER_ROOT_PATH}", providerRootFolder);
            if (assemblyPath.StartsWith(".\\")) assemblyPath = providerRootFolder + assemblyPath.Replace(".\\", "\\");
            if (assemblyPath.StartsWith(".")) assemblyPath = Path.GetFullPath(assemblyPath);

            assemblyPath = assemblyPath + (!assemblyPath.EndsWith("\\") ? "\\" : "");

            var assemblyPathWithAssemblyName = assemblyPath + reflectionDefinition.AssemblyName + ".dll";

            // Load the provider object.
            // WARNING: Loading from local functions so any bug fixes in CSHARPStandard.Reflection will not be applied here.
            var loadedObject = (reflectionDefinition.UseActivator == true
                                    ? LoadObjectUsingActivator(assemblyPathWithAssemblyName, reflectionDefinition.TypeName)
                                    : LoadObject(assemblyPathWithAssemblyName, reflectionDefinition.TypeName)
                                );

            return loadedObject;
        }

        #region Taken From CSHARPStandard.Reflection 

        /// <summary>
        /// Loads an object from an assembly
        /// </summary>
        /// <param name="fullPathToDll"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private object LoadObject(string fullPathToDll, string typeName)
        {
            if (string.IsNullOrEmpty(fullPathToDll)) throw new ArgumentNullException("ERROR: fullPathToDll is manditory");
            if (string.IsNullOrEmpty(typeName)) throw new ArgumentNullException("ERROR: typeName is manditory");
            if (File.Exists(fullPathToDll) == false) throw new System.IO.FileNotFoundException("ERROR: dll not found at: " + fullPathToDll);

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
        private object LoadObjectUsingActivator(string fullPathToDll, string typeName)
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
