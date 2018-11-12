    namespace TestTraitsProviderByReflection
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics;
    using System;
    using TraitsQuickStart.Business;
    using TraitsQuickStart.ProvidersByReflection;

    [TestClass]
    public class TestProviderReflectionManager
    {
        #region Provider Related

        [TestMethod]
        public void TestGetAllProviderNamesOfObjectTypeNullTraits()
        {
            var providerReflectionManager = new ProviderReflectionManager();
            Assert.ThrowsException<ArgumentNullException>(() => providerReflectionManager.GetAllProviderNamesOfObjectType(string.Empty));
        }

        [TestMethod]
        public void TestGetAllProviderNamesOfObjectTypeEmptyObjectType()
        {
            var defaultRootPath = (Debugger.IsAttached ? "c:\\traits" : AppDomain.CurrentDomain.BaseDirectory);
            defaultRootPath = defaultRootPath + (defaultRootPath.EndsWith("\\") ? string.Empty : "\\");

            var traitsHelper = new TraitHelper
            {
                TraitPath = defaultRootPath,
                TraitFileName = "test_traits"
            };
            var traits = traitsHelper.GetTraits();

            var providerReflectionManager = new ProviderReflectionManager()
            {
                Traits = traits,
                ProviderRootPath = defaultRootPath + "Providers"
            };

            Assert.ThrowsException<ArgumentNullException>(() => providerReflectionManager.GetAllProviderNamesOfObjectType(string.Empty));
        }

        [TestMethod]
        public void TestGetAllProviderNamesOfObjectTypeTestObjectType()
        {
            var defaultRootPath = (Debugger.IsAttached ? "c:\\traits" : AppDomain.CurrentDomain.BaseDirectory);
            defaultRootPath = defaultRootPath + (defaultRootPath.EndsWith("\\") ? string.Empty : "\\");

            var traitsHelper = new TraitHelper
            {
                TraitPath = defaultRootPath,
                TraitFileName = "test_traits"
            };
            var traits = traitsHelper.GetTraits();

            var providerReflectionManager = new ProviderReflectionManager()
            {
                Traits = traits,
                ProviderRootPath = defaultRootPath + "Providers"
            };

            var providers = providerReflectionManager.GetAllProviderNamesOfObjectType("Test");
        }

        [TestMethod]
        public void TestGetProviderDataSourceNullTraits()
        {
        }
        [TestMethod]
        public void TestGetProviderDataSourceEmptyObjectType()
        {
        }

        [TestMethod]
        public void TestGetProviderDataSourceTestObjectTypeEmptyProviderName()
        {
        }

        [TestMethod]
        public void TestGetProviderDataSourceTestObjectTypeTestProviderName()
        {
        }

        [TestMethod]
        public void TestLoadProviderNullTraits()
        {
        }

        [TestMethod]
        public void TestLoadProviderEmptyObjectType()
        {
        }

        [TestMethod]
        public void TestLoadProviderTestObjectTypeEmptyProviderName()
        {
        }

        [TestMethod]
        public void TestLoadProviderTestObjectTypeTestProviderName()
        {
            var defaultRootPath = (Debugger.IsAttached ? "c:\\traits" : AppDomain.CurrentDomain.BaseDirectory);
            defaultRootPath = defaultRootPath + (defaultRootPath.EndsWith("\\") ? string.Empty : "\\");

            var traitsHelper = new TraitHelper
            {
                TraitPath = defaultRootPath,
                TraitFileName = "test_traits"
            };
            var traits = traitsHelper.GetTraits();
            if (traits == null) throw new Exception("EREROR: Traits could not be loaded");

            var providerReflectionManager = new ProviderReflectionManager
            {
                Traits = traits,
                ProviderRootPath = defaultRootPath
            };
            var provider = providerReflectionManager.LoadProvider("DEFAULT", "TEST");
            if (provider == null) throw new Exception("ERROR: provider could not be loaded");
        }

        #endregion
    }
}
