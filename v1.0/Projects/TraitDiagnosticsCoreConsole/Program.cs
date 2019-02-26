/********************************************************************************
 * Traits Diagnostics Console 
 * 
 * LICENSE: Free to use provided details on fixes and/or extensions emailed to 
 *          chris.williams@readwatchcreate.com
 ********************************************************************************/

namespace TraitDiagnosticsCoreConsole
{
    using CSHARPStandard.Diagnostics;
    using System;
    using TraitsQuickStart.Business;
    using TraitsQuickStart.Data;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Traits Diagnostics .NET Core Console");

            // trait source should be assigned
            string traitRootPath = (args.Length > 0)
                ? args[0] : "c:\\traits\\";

            // trait source should be assigned
            string traitDataSource = (args.Length > 1)
                ? args[1] : "traits";

            // Get all the feeds to process
            var eventLog = new ConsoleEventLog();

            var traitsHelper = new TraitHelper
            {
                TraitPath = traitRootPath,
                TraitFileName = traitDataSource
            };

            var traits = traitsHelper.GetTraits();

            // TO DO: Write some basic traits file diagnostics tests
        }
    }
}
