using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TraitsQuickStart.Foundation.Traits;

namespace TestTraits
{
    [TestClass]
    public class TestTraitsManager
    {
        [TestMethod]
        public void TestLoadTraitsEmptySourceType()
        {
            var traitsManager = new TraitsManager();
            Assert.ThrowsException<ArgumentNullException>(() => traitsManager.GetTraits(string.Empty, string.Empty));
        }

        [TestMethod]
        public void TestLoadTraitsStringSourceTypeNoStorageProviderPath()
        {
            var traitsJson = "{\"TraitPairs\":[{\"Key\":\"Test_Trait_Key_1\",\"Value\":\"Test_Trait_Value_1\"},{\"Key\":\"Test_Trait_Key_2\",\"Value\":\"Test_Trait_Value_2\"},{\"Key\":\"Test_Trait_Key_3\",\"Value\":\"Test_Trait_Value_3\"},{\"Key\":\"Test_Trait_Key_4\",\"Value\":\"Test_Trait_Value_4\"},{\"Key\":\"Test_Trait_Key_5\",\"Value\":\"Test_Trait_Value_5\"},{\"Key\":\"Test_Trait_Key_6\",\"Value\":\"Test_Trait_Value_6\"},{\"Key\":\"Test_Trait_Key_7\",\"Value\":\"Test_Trait_Value_6\"},{\"Key\":\"Test_Trait_Key_8\",\"Value\":\"Test_Trait_Value_7\"},{\"Key\":\"Test_Trait_Key_9\",\"Value\":\"Test_Trait_Value_8\"},{\"Key\":\"Test_Trait_Key_10\",\"Value\":\"Test_Trait_Value_10\"}],\"Version\":\"2.0\"}";

            var traitsManager = new TraitsManager();
            Assert.ThrowsException<ArgumentNullException>(() => traitsManager.GetTraits("String", traitsJson));
        }

        [TestMethod]
        public void TestLoadTraitsStringSourceType()
        {
            var traitsJson = "{\"TraitPairs\":[{\"Key\":\"Test_Trait_Key_1\",\"Value\":\"Test_Trait_Value_1\"},{\"Key\":\"Test_Trait_Key_2\",\"Value\":\"Test_Trait_Value_2\"},{\"Key\":\"Test_Trait_Key_3\",\"Value\":\"Test_Trait_Value_3\"},{\"Key\":\"Test_Trait_Key_4\",\"Value\":\"Test_Trait_Value_4\"},{\"Key\":\"Test_Trait_Key_5\",\"Value\":\"Test_Trait_Value_5\"},{\"Key\":\"Test_Trait_Key_6\",\"Value\":\"Test_Trait_Value_6\"},{\"Key\":\"Test_Trait_Key_7\",\"Value\":\"Test_Trait_Value_6\"},{\"Key\":\"Test_Trait_Key_8\",\"Value\":\"Test_Trait_Value_7\"},{\"Key\":\"Test_Trait_Key_9\",\"Value\":\"Test_Trait_Value_8\"},{\"Key\":\"Test_Trait_Key_10\",\"Value\":\"Test_Trait_Value_10\"}],\"Version\":\"2.0\"}";
            var traitsStorageProviderPath = Path.GetFullPath("..\\..\\..\\..\\..\\..\\..\\builds\\CurrentBuild\\Bin\\");

            var traitsManager = new TraitsManager()
            {
                 TraitsStorageProviderPath = traitsStorageProviderPath
            };
            var traits = traitsManager.GetTraits("String", traitsJson);

            Assert.IsNotNull(traits);
            Assert.IsNotNull(traits.TraitPairs);
            Assert.IsTrue(traits.Version == "2.0");
            Assert.IsTrue(traits.TraitPairs.ContainsKey("Test_Trait_Key_1"));
        }

        [TestMethod]
        public void TestLoadTraitsFileSourceTypeNoStorageProviderPath()
        {
            var traitsFilePath = "..\\..\\..\\..\\..\\..\\..\\Data\\sampletraitsv2.json";

            var traitsManager = new TraitsManager();
            Assert.ThrowsException<ArgumentNullException>(() => traitsManager.GetTraits("File", traitsFilePath));
        }

        [TestMethod]
        public void TestLoadTraitsFileSourceType()
        {
            var traitsFilePath = "..\\..\\..\\..\\..\\..\\..\\Data\\sampletraitsv2.json";
            var traitsStorageProviderPath = Path.GetFullPath("..\\..\\..\\..\\..\\..\\..\\builds\\CurrentBuild\\Bin\\");

            var traitsManager = new TraitsManager()
            {
                TraitsStorageProviderPath = traitsStorageProviderPath
            };
            var traits = traitsManager.GetTraits("File", traitsFilePath);

            Assert.IsNotNull(traits);
            Assert.IsNotNull(traits.TraitPairs);
            Assert.IsTrue(traits.Version == "2.0");
            Assert.IsTrue(traits.TraitPairs.ContainsKey("Test_Trait_Key_1"));
        }
    }
}
