using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraitsQuickStart.Foundation.TraitStorageProvider.String;

namespace TestStringTraitStorageProvider
{
    [TestClass]
    public class TestStringTraitsProvider
    {
        /// <summary>
        /// Test string type source valid traits JSON in connection string
        /// </summary>
        [TestMethod]
        public void TestGetTraitsStringProviderValid()
        {
            //V2 
            var traitsV2Json = "{\"TraitPairs\":[{\"Key\":\"Test_Trait_Key_1\",\"Value\":\"Test_Trait_Value_1\"},{\"Key\":\"Test_Trait_Key_2\",\"Value\":\"Test_Trait_Value_2\"},{\"Key\":\"Test_Trait_Key_3\",\"Value\":\"Test_Trait_Value_3\"},{\"Key\":\"Test_Trait_Key_4\",\"Value\":\"Test_Trait_Value_4\"},{\"Key\":\"Test_Trait_Key_5\",\"Value\":\"Test_Trait_Value_5\"},{\"Key\":\"Test_Trait_Key_6\",\"Value\":\"Test_Trait_Value_6\"},{\"Key\":\"Test_Trait_Key_7\",\"Value\":\"Test_Trait_Value_6\"},{\"Key\":\"Test_Trait_Key_8\",\"Value\":\"Test_Trait_Value_7\"},{\"Key\":\"Test_Trait_Key_9\",\"Value\":\"Test_Trait_Value_8\"},{\"Key\":\"Test_Trait_Key_10\",\"Value\":\"Test_Trait_Value_10\"}],\"Version\":\"2.0\"}";

            var traitsV2Provider = new StringTraitsProvider()
            {
                ConnectionString = traitsV2Json
            };
            Assert.IsTrue(traitsV2Provider.LoadTraits());
            Assert.IsNotNull(traitsV2Provider.Traits);
            Assert.IsNotNull(traitsV2Provider.Traits.TraitPairs);
            Assert.IsTrue(traitsV2Provider.Traits.Version == "2.0");
            Assert.IsTrue(traitsV2Provider.Traits.TraitPairs.ContainsKey("Test_Trait_Key_1"));

            //V3 
            var traitsV3Json = "{\"TraitPairs\":[{\"Key\":\"PROVIDER_PIPELINE_STORAGE_PROVIDER_FILE\",\"Value\":\"TraitsQuickStart.Fpundation.Pipelines.StorageProviders.dll,TraitsQuickStart.Foundation.Pipelines.StorageProviders.FileTraitsPipelineProvider,\"},{\"Key\":\"PROVIDER_PIPELINE_STORAGE_PROVIDER_STRING\",\"Value\":\"TraitsQuickStart.Features.Pipelines.StorageProviders.dll,TraitsQuickStart.Features.Pipelines.StorageProviders.StringTraitsPipelineProvider,\"}],\"Version\":\"3.0\"}";

            var traitsV3Provider = new StringTraitsProvider()
            {
                ConnectionString = traitsV3Json
            };
            Assert.IsTrue(traitsV3Provider.LoadTraits());
            Assert.IsNotNull(traitsV3Provider.Traits);
            Assert.IsNotNull(traitsV3Provider.Traits.TraitPairs);
            Assert.IsTrue(traitsV3Provider.Traits.Version == "3.0");
            Assert.IsTrue(traitsV3Provider.Traits.TraitPairs.ContainsKey("PROVIDER_PIPELINE_STORAGE_PROVIDER_FILE"));
        }
    }
}
