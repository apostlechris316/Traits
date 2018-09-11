namespace UnitTestTraitsCoreConsole
{
    using CSHARPStandard.Traits.Business;
    using CSHARPStandard.Traits.ProvidersByReflection;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Traits Uit Tester!");

            // Make it look like a main frame (Thanks Martina Welander for inspiration)
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WindowWidth = 160;

            string traitDataSource = (args.Length > 1)
                ? args[1] : string.Empty;

            string logType = "CONSOLE";

            // Get the default root path from arguments if available otherwise use executable path.
            string defaultRootPath = (args.Length > 2)
                    ? args[2] : AppDomain.CurrentDomain.BaseDirectory;

            var traitHelper = new TraitHelper
            {
                TraitPath = defaultRootPath,
                TraitFileName = traitDataSource
            };
            var traits = traitHelper.GetTraits();


            var providerReflectionManager = new ProviderReflectionManager
            {
                Traits = traits,
                ProviderRootPath = defaultRootPath
            };
            var provider = providerReflectionManager.LoadProvider("DEFAULT", "TEST");
            if (provider == null) Console.WriteLine("Unit Test Failed");

            Console.WriteLine("Unit Test Succeeded");
            Console.ReadKey();
        }
    }
}
