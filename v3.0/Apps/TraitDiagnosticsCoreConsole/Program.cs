/********************************************************************************
 * Traits Diagnostics Console 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitDiagnosticsCoreConsole
{
    using DiagnosticsQuickStart.Business;
    using System;
    using System.Collections.Generic;
    using TraitsQuickStart.Foundation.Pipelines;
    using TraitsQuickStart.Foundation.Reflection.ReflectionByDefinition;
    using TraitsQuickStart.Foundation.Traits;

    class Program
    {
        const string PROVIDER_ROOT_PATH = "PROVIDER_ROOT_PATH";
        const string REFLECTION_DEFINITION_PATH = "REFLECTION_DEFINITION_PATH";
        const string TRAITS_PROVIDER_ROOT_PATH = "TRAITS_PROVIDER_ROOT_PATH";
        const string DEFAULT_TRAITS_FILE_PATH = "DEFAULT_TRAITS_FILE_PATH";
        const string DEFAULT_TRAITS_PIPELINE_FILE_FULL_PATH = "DEFAULT_TRAITS_PIPELINE_FULL_PATH";
        const string DEFAULT_REFLECTION_DEFINITION_ENABLED_PIPELINE_FILE_PATH = "DEFAULT_REFLECTION_DEFINITION_ENABLED_PIPELINE_FILE_PATH";
        const string DEFAULT_REFLECTION_DEFINITION_NAME = "DEFAULT_REFLECTION_DEFINITION_NAME";

        static void Main(string[] args)
        {
            // Make it look like a main frame (Thanks Martina Welander for inspiration)

            Console.WindowWidth = 160;
            Console.WriteLine("Traits Diagnostics .NET Core Console");

            // These are the defaults
            Dictionary<string, string> providerSettings = new Dictionary<string, string>();
            providerSettings.Add(PROVIDER_ROOT_PATH, "..\\..\\..\\..\\..\\builds\\CurrentBuild\\Providers");
            providerSettings.Add(REFLECTION_DEFINITION_PATH, "..\\..\\..\\..\\..\\builds\\CurrentBuild\\Reflections");
            providerSettings.Add(TRAITS_PROVIDER_ROOT_PATH,  "..\\..\\..\\..\\..\\builds\\CurrentBuild\\Bin\\");

            providerSettings.Add(DEFAULT_TRAITS_FILE_PATH, "..\\..\\..\\..\\..\\builds\\CurrentBuild\\data\\defaulttraits.json");
            providerSettings.Add(DEFAULT_TRAITS_PIPELINE_FILE_FULL_PATH, "..\\..\\..\\..\\..\\builds\\CurrentBuild\\data\\pipelines\\defaulttraitspipeline.json");
            providerSettings.Add(DEFAULT_REFLECTION_DEFINITION_ENABLED_PIPELINE_FILE_PATH, "..\\..\\..\\..\\..\\builds\\CurrentBuild\\data\\pipelines\\defaultreflectiondefinitionenabledpipeline.json");
            providerSettings.Add(DEFAULT_REFLECTION_DEFINITION_NAME, "PROVIDER_PIPELINE_STORAGE_PROVIDER_FILE");

            if(args.Length > 0)
            {
                foreach(var arg in args)
                {
                    var argParts = arg.Split('|');

                    if(providerSettings.ContainsKey(argParts[0].ToUpperInvariant()))
                    {
                        providerSettings[argParts[0]] = argParts[1];
                    }
                }
            }

            var status = new ConsoleEventLog();

            var traitsManager = new TraitsManager()
            {
                Status = status
            };

            var reflectionManager = new ReflectionManager()
            {
                ProviderRootFolder = providerSettings[PROVIDER_ROOT_PATH],
                ReflectionDefinitionFolder = providerSettings[REFLECTION_DEFINITION_PATH]
            };

            int choice = -1;
            while (choice != -99)
            {
                Console.WriteLine("\r\n\r\n");
                Console.WriteLine("Choose a task by typing the number (or type -99 to QUIT) and pressing enter:");
                Console.WriteLine("1 - Create a traits file using BaseTraits");
                Console.WriteLine("2 - Validate a traits file");
                Console.WriteLine("3 - Create reflection definitions");
                Console.WriteLine("4 - Validate a reflection definition");
                Console.WriteLine("5 - Create a traits pipeline");
                Console.WriteLine("6 - Validate a traits pipeline");
                Console.WriteLine("7 - Execute a traits pipeline");
                Console.WriteLine("8 - Create a reflection definition enabled pipeline");
                Console.WriteLine("9 - Validate a reflection definition enabled pipeline");
                Console.WriteLine("10 - Execute a reflection definition enabled pipeline");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case -99: // Exit value
                        break;
                    case 1:     // 1 - Create a language detection traits file

                        DoCreateTraitsFile(providerSettings, status);
                        break;

                    case 2:

                        #region 2 - Validate a language detection traits file

                        #endregion

                        break;

                    case 3:     // 3 - Create reflection definitions

                        DoCreateReflectionDefinitions(providerSettings);

                        break;

                    case 4:

                        #region 4 - Validate a reflection definition

                        Console.WriteLine("Please enter the reflection definition name: (Press ENTER for PROVIDER_PIPELINE_STORAGE_PROVIDER_FILE)");
                        var reflectionDefinitionName = Console.ReadLine();
                        if (string.IsNullOrEmpty(reflectionDefinitionName)) reflectionDefinitionName = providerSettings[DEFAULT_REFLECTION_DEFINITION_NAME];

                        var loadedReflectionDefinition = reflectionManager.LoadReflectionDefinition(reflectionDefinitionName);
                        if (loadedReflectionDefinition == null) throw new NullReferenceException("ERROR: cannot load " + reflectionDefinitionName);

                        Console.WriteLine("Loaded assembly " + reflectionDefinitionName + " successfully.");
                        Console.WriteLine(string.Format("     - AssemblyName: {0}\r\n     - AssemblyPath: {1}\r\n     - TypeName: {2}", loadedReflectionDefinition.AssemblyName, loadedReflectionDefinition.AssemblyPath, loadedReflectionDefinition.TypeName));

                        #endregion

                        break;

                    case 5:     // 5- Create a traits pipeline

                        DoCreateTraitsPipeline(providerSettings, status);

                        break;

                    case 6:
                        #region 6 - Validate a traits pipeline
                        #endregion
                        break;

                    case 7:     // 7 - Execute a traits pipeline

                        DoExecuteTraitsPipeline(providerSettings, status);
                        break;

                    case 8:     // 8 - Create a reflection definition enabled pipeline

                        DoCreateReflectionDefinitionEnabledPipeline(providerSettings, status);

                        break;

                    case 9:

                        #region 9 - Validate a reflection definition enabled pipeline

                        #endregion

                        Console.WriteLine("Invalid Choice - 9 !!!");
                        break;

                    case 10:    // 10 - Execute a reflection definition enabled language detection pipeline

                        DoExecuteReflectionDefinitionEnabledPipeline(providerSettings, status);
                        break;

                    default:
                        Console.WriteLine("Invalid Choice - " + choice + " !!!");
                        break;
                }
            }
        }

        #region Traits Related 

        static bool DoCreateTraitsFile(Dictionary<string, string> providerSettings, IEventLog status)
        {
            var traitsManager = new TraitsManager()
            {
                Status = status,
                TraitsStorageProviderPath = System.IO.Path.GetFullPath(providerSettings[TRAITS_PROVIDER_ROOT_PATH])
            };

            Console.WriteLine("Please enter the full path (including filename) to the traits file you wish to create: (Press ENTER for default)");
            var newTraitsFileFullPath = Console.ReadLine();
            if (string.IsNullOrEmpty(newTraitsFileFullPath)) newTraitsFileFullPath = providerSettings[DEFAULT_TRAITS_FILE_PATH];

            var traits = new BaseTraits
            {
                TraitPairs = new Dictionary<string, string>
                {
                    { "PROVIDER_PIPELINE_STORAGE_PROVIDER_FILE", "TraitsQuickStart.Foundation.PipelineStorageProviders.dll,TraitsQuickStart.Foundation.PipelineStorageProviders.FileTraitsPipelineProvider," },
                    { "PROVIDER_PIPELINE_STORAGE_PROVIDER_STRING", "TraitsQuickStart.Foundation.PipelineStorageProviders.dll,TraitsQuickStart.Foundation.PipelineStorageProviders.StringTraitsPipelineProvider," }
                }
            };

            if (traitsManager.SaveTraits("File", newTraitsFileFullPath, traits) == false)
            {
                Console.WriteLine("ERROR: Creating traits file failed for: " + newTraitsFileFullPath);
                return false;
            }
            else
            {
                Console.WriteLine("SUCCESS: Traits file created at: " + newTraitsFileFullPath);
                return true;
            }
        }

        #endregion

        #region Traits Pipeline Related

        static bool DoCreateTraitsPipeline(Dictionary<string, string> providerSettings, IEventLog status)
        {
            var traitsManager = new TraitsManager()
            {
                Status = status,
                TraitsStorageProviderPath = providerSettings[TRAITS_PROVIDER_ROOT_PATH]
            };

            Console.WriteLine("Please enter the full path (including filename) to the traits file: (Press ENTER for default)");
            var traitsFileFullPath = Console.ReadLine();
            if (string.IsNullOrEmpty(traitsFileFullPath)) traitsFileFullPath = providerSettings[DEFAULT_TRAITS_FILE_PATH];

            Console.WriteLine("Please enter the full path (including filename) to the traits pipeline file you wish to create: (Press ENTER for for default location)");
            var newTraitsPipelineFileFullPath = Console.ReadLine();
            if (string.IsNullOrEmpty(newTraitsPipelineFileFullPath)) newTraitsPipelineFileFullPath = providerSettings[DEFAULT_TRAITS_PIPELINE_FILE_FULL_PATH];

            var traits = traitsManager.GetTraits("File", traitsFileFullPath);

            var traitsPipeline = new TraitsPipeline()
            {
                ErrorMode = "STOP_ON_ERROR",
                PipelineExecutionMode = "ALL_MATCHING"
            };
            traitsPipeline.PipelineSteps.Add(new TraitsPipelineStep()
            {
                ProviderName = "TEST"
            });

            var traitsPipelineManager = new TraitsPipelineManager()
            {
                Status = status,
                Pipeline = traitsPipeline,
                ProviderRootPath = providerSettings[PROVIDER_ROOT_PATH],
                Traits = traits
            };

            if (traitsPipelineManager.SaveTraitsPipeline("SAMPLE", "File", newTraitsPipelineFileFullPath, null) == false)
            {
                Console.WriteLine("ERROR: Creating traits file failed for: " + newTraitsPipelineFileFullPath);
                return false;
            }
            else
            {
                Console.WriteLine("SUCCESS: Traits file created at: " + newTraitsPipelineFileFullPath);
                return true;
            }
        }

        static bool DoExecuteTraitsPipeline(Dictionary<string, string> providerSettings, IEventLog status)
        {
            Console.WriteLine("Please enter the full path (including filename) to the traits file: (Press ENTER for default)");
            var traitsFileFullPath = Console.ReadLine();
            if (string.IsNullOrEmpty(traitsFileFullPath)) traitsFileFullPath = providerSettings[DEFAULT_TRAITS_FILE_PATH];

            Console.WriteLine("Please enter the full path (including filename) to the traits pipeline file you wish to create: (Press ENTER for for default location)");
            var loadTraitsPipelineFileFullPath = Console.ReadLine();
            if (string.IsNullOrEmpty(loadTraitsPipelineFileFullPath)) loadTraitsPipelineFileFullPath = providerSettings[DEFAULT_TRAITS_PIPELINE_FILE_FULL_PATH];

            var traitsManager = new TraitsManager()
            {
                Status = status,
                TraitsStorageProviderPath = providerSettings[TRAITS_PROVIDER_ROOT_PATH]
            };

            var traits = traitsManager.GetTraits("File", traitsFileFullPath);


            var traitsPipelineManager = new TraitsPipelineManager()
            {
                Status = status,                 
                ProviderRootPath = providerSettings[PROVIDER_ROOT_PATH],
                Traits = traits
            };

            // TO DO: Derived class will load the specific pipeline type
            if (traitsPipelineManager.LoadTraitsPipeline(string.Empty, "File", loadTraitsPipelineFileFullPath, null) == false)
                return false;

            Console.WriteLine("Please enter the text to process: (Press ENTER for for default text)");
            var content = Console.ReadLine();

            // TO DO: Derived will execute the specific pipeline type
            //traitsPipelineManager.ExecuteTraitsPipeline(content, true);

            Console.WriteLine("WARNING: The base application does not know how to Execute the pipeline.");
            Console.ReadLine();

            return false;
        }

        #endregion

        #region Reflection Definition Related

        static bool DoCreateReflectionDefinitions(Dictionary<string, string> providerSettings)
        {
            var reflectionManager = new ReflectionManager()
            {
                ProviderRootFolder = providerSettings[PROVIDER_ROOT_PATH],
                ReflectionDefinitionFolder = providerSettings[REFLECTION_DEFINITION_PATH]
            };

            #region Traits Related

            var reflectionDefinition = new ReflectionDefinition()
            {
                AssemblyName = "TraitsQuickStart.Features.Pipelines.StorageProviders",
                AssemblyPath = "{PROVIDER_ROOT_PATH}",
                TypeName = "TraitsQuickStart.Features.Pipelines.StorageProviders.FileTraitsPipelineProvider",
                UseActivator = false
            };
            if (reflectionManager.CreateReflectionDefinition("PROVIDER_PIPELINE_STORAGE_PROVIDER_FILE", reflectionDefinition) == false) return false;

            reflectionDefinition = new ReflectionDefinition()
            {
                AssemblyName = "TraitsQuickStart.Features.Pipelines.StorageProviders",
                AssemblyPath = "{PROVIDER_ROOT_PATH}",
                TypeName = "TraitsQuickStart.Features.Pipelines.StorageProviders.StringTraitsPipelineProvider",
                UseActivator = false
            };
            if (reflectionManager.CreateReflectionDefinition("PROVIDER_PIPELINE_STORAGE_PROVIDER_STRING", reflectionDefinition) == false) return false;

            #endregion

            return true;
        }

        static bool DoValidateReflectionDefinition(Dictionary<string, string> providerSettings, string defaultReflectionDefinitionName)
        {
            var reflectionManager = new ReflectionManager()
            {
                ProviderRootFolder = providerSettings[PROVIDER_ROOT_PATH],
                ReflectionDefinitionFolder = providerSettings[REFLECTION_DEFINITION_PATH]
            };

            Console.WriteLine("Please enter the reflection definition name: (Press ENTER for PROVIDER_PIPELINE_STORAGE_PROVIDER_FILE)");
            var reflectionDefinitionName = Console.ReadLine();
            if (string.IsNullOrEmpty(reflectionDefinitionName)) reflectionDefinitionName = defaultReflectionDefinitionName;

            var loadedReflectionDefinition = reflectionManager.LoadReflectionDefinition(reflectionDefinitionName);
            if (loadedReflectionDefinition == null) throw new NullReferenceException("ERROR: cannot load " + reflectionDefinitionName);

            Console.WriteLine("Loaded assembly " + reflectionDefinitionName + " successfully.");
            Console.WriteLine(string.Format("     - AssemblyName: {0}\r\n     - AssemblyPath: {1}\r\n     - TypeName: {2}", loadedReflectionDefinition.AssemblyName, loadedReflectionDefinition.AssemblyPath, loadedReflectionDefinition.TypeName));

            return true;
        }

        #endregion

        #region Reflection Definition Enabled Pipeline Related

        static bool DoCreateReflectionDefinitionEnabledPipeline(Dictionary<string, string> providerSettings, IEventLog status)
        {
            Console.WriteLine("Please enter the full path (including filename) to the reflection definition pipeline file you wish to create: (Press ENTER for for default location)");
            var newReflectionDefinitionEnabledPipelineFileFullPath = Console.ReadLine();
            if (string.IsNullOrEmpty(newReflectionDefinitionEnabledPipelineFileFullPath)) newReflectionDefinitionEnabledPipelineFileFullPath = providerSettings[DEFAULT_REFLECTION_DEFINITION_ENABLED_PIPELINE_FILE_PATH];

            var traitsManager = new TraitsManager()
            {
                Status = status,
                TraitsStorageProviderPath = providerSettings[TRAITS_PROVIDER_ROOT_PATH]
            };

            var traits = traitsManager.GetTraits("FILE", providerSettings[DEFAULT_TRAITS_FILE_PATH]);

            var reflectionDefinitionEnabledTraitsPipeline = new TraitsPipeline()
            {
                ErrorMode = "STOP_ON_ERROR",
                PipelineExecutionMode = "ALL_MATCHING"
            };
            reflectionDefinitionEnabledTraitsPipeline.PipelineSteps.Add(new TraitsPipelineStep()
            {
                ProviderName = "TEST"
            });

            var reflectionDefinitionTraitsPipelineManager = new ReflectionDefinitionEnabledTraitsPipelineManager()
            {
                Status = status,
                Pipeline = reflectionDefinitionEnabledTraitsPipeline,
                ReflectionDefinitionFolder = providerSettings[REFLECTION_DEFINITION_PATH],
                ProviderRootPath = providerSettings[PROVIDER_ROOT_PATH],
                Traits = traits
            };

            if (reflectionDefinitionTraitsPipelineManager.SaveTraitsPipeline("SAMPLE", "FILE", newReflectionDefinitionEnabledPipelineFileFullPath, null) == false)
            {
                Console.WriteLine("ERROR: Creating pipeline file failed for: " + newReflectionDefinitionEnabledPipelineFileFullPath);
                return false;
            }
            else
            {
                Console.WriteLine("SUCCESS: Pipeline file created at: " + newReflectionDefinitionEnabledPipelineFileFullPath);
                return true;
            }
        }

        static bool DoExecuteReflectionDefinitionEnabledPipeline(Dictionary<string, string> providerSettings, IEventLog status)
        {
            Console.WriteLine("Please enter the full path (including filename) to the reflection definition pipeline file you wish to create: (Press ENTER for for default location)");
            var loadReflectionDefinitionEnabledPipelineFileFullPath = Console.ReadLine();
            if (string.IsNullOrEmpty(loadReflectionDefinitionEnabledPipelineFileFullPath)) loadReflectionDefinitionEnabledPipelineFileFullPath = providerSettings[DEFAULT_REFLECTION_DEFINITION_ENABLED_PIPELINE_FILE_PATH];

            var traitsManager = new TraitsManager()
            {
                Status = status,
                TraitsStorageProviderPath = providerSettings[TRAITS_PROVIDER_ROOT_PATH]
            };

            var traits = traitsManager.GetTraits("FILE", providerSettings[DEFAULT_TRAITS_FILE_PATH]);

            var reflectionDefinitionTraitsPipelineManager = new ReflectionDefinitionEnabledTraitsPipelineManager()
            {
                Status = status,
                ReflectionDefinitionFolder = providerSettings[REFLECTION_DEFINITION_PATH],
                ProviderRootPath = providerSettings[PROVIDER_ROOT_PATH],
                Traits = traits
            };

            // TO DO: The specific implementation would do its own loading of traits pipeline
            //var reflectionDefinitionEnabledTraitsPipeline = reflectionDefinitionTraitsPipelineManager.LoadTraitsPipeline(string.Empty, "FILE", defaultReflectionDefinitionEnabledPipelineFileFullPath, null);

            // TO DO: The specific implementation would do its own execution of traits pipeline
            // reflectionDefinitionTraitsPipelineManager.ExecuteTraitsPipeline();

            return false;
        }

        #endregion
    }
}
