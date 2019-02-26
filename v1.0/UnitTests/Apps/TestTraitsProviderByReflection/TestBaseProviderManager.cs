
namespace TestTraitsProviderByReflection
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using TraitsQuickStart.Business;
    using TraitsQuickStart.ProvidersByReflection;

    [TestClass]
    public class TestBaseProviderManager
    {
        [TestMethod]
        public void TestBaseProviderManagerLoadProviderNoTraitsEmptyStringProviderName()
        {
            var providerReflectionManager = new ProviderReflectionManager();

            Assert.ThrowsException<ArgumentNullException>(() => providerReflectionManager.LoadProvider(string.Empty, string.Empty));
        }

        [TestMethod]
        public void TestBaseProviderManagerLoadProviderEmptyStringProviderName()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = "c:\\",
                TraitFileName = "TestTraits"
            };
            var traits = traitsHelper.GetTraits();

            var providerReflectionManager = new ProviderReflectionManager
            {
                Traits = traits
            };
            Assert.ThrowsException<ArgumentNullException>(() => providerReflectionManager.LoadProvider(string.Empty, string.Empty));
        }

        [TestMethod]
        public void TestBaseProviderManagerGetProviderDataSourceNoTraitsEmptyStringProviderName()
        {
            var providerReflectionManager = new ProviderReflectionManager();
            Assert.ThrowsException<ArgumentNullException>(() => providerReflectionManager.GetProviderDataSource(string.Empty, string.Empty));
        }

        [TestMethod]
        public void TestBaseProviderManagerGetProviderDataSourceEmptyStringProviderName()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = "c:\\",
                TraitFileName = "TestTraits"
            };
            var traits = traitsHelper.GetTraits();

            var providerReflectionManager = new ProviderReflectionManager
            {
                Traits = traits
            };
            Assert.ThrowsException<ArgumentNullException>(() => providerReflectionManager.GetProviderDataSource(string.Empty, string.Empty));
        }
    }
}
