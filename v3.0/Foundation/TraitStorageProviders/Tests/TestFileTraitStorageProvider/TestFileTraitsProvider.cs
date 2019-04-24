namespace TestFileTraitStorageProvider
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraitsQuickStart.Foundation.TraitStorageProvider.File;

    [TestClass]
    public class TestFileTraitsProvider
    {
        /// <summary>
        /// Test file type source valid traits JSON in connection string
        /// </summary>
        [TestMethod]
        public void TestGetTraitsFileProviderValid()
        {
            var traitsFilePath = "..\\..\\..\\..\\..\\..\\..\\Data\\sampletraitsv2.json";
            var traitsProvider = new FileTraitsProvider()
            {
                ConnectionString = traitsFilePath
            };
            Assert.IsTrue(traitsProvider.LoadTraits());
            Assert.IsNotNull(traitsProvider.Traits);
            Assert.IsNotNull(traitsProvider.Traits.TraitPairs);
            Assert.IsTrue(traitsProvider.Traits.Version == "2.0");
            Assert.IsTrue(traitsProvider.Traits.TraitPairs.ContainsKey("Test_Trait_Key_1"));

            traitsFilePath = "..\\..\\..\\..\\..\\..\\..\\Data\\sampletraitsv3.json";
            traitsProvider.ConnectionString = traitsFilePath;
            Assert.IsTrue(traitsProvider.LoadTraits());
            Assert.IsNotNull(traitsProvider.Traits);
            Assert.IsNotNull(traitsProvider.Traits.TraitPairs);
            Assert.IsTrue(traitsProvider.Traits.Version == "3.0");
            Assert.IsTrue(traitsProvider.Traits.TraitPairs.ContainsKey("PROVIDER_PIPELINE_STORAGE_PROVIDER_FILE"));
        }
    }
}
