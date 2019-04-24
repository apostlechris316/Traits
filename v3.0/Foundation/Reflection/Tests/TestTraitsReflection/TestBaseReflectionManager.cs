namespace TestTraitsReflection
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using TraitsQuickStart.Foundation.Reflection;

    [TestClass]
    public class TestBaseReflectionManager
    {
        private const string PROVIDER_ROOT_PATH = "PROVIDER_ROOT_PATH";

        [TestMethod]
        public void LoadTestObject()
        {
            var fullPathToDll = Path.GetFullPath("..\\..\\..\\..\\..\\..\\..\\builds\\CurrentBuild\\Bin\\TraitsQuickStart.Foundation.TraitStorageProvider.String.dll");
            var reflectionManager = new BaseReflectionManager();
            var traitStorageProvider = reflectionManager.LoadObject(fullPathToDll, "TraitsQuickStart.Foundation.TraitStorageProvider.String.StringTraitsProvider");

            Assert.IsNotNull(traitStorageProvider);
        }
    }
}
