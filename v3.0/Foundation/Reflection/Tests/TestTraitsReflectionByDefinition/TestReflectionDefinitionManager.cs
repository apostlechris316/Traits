namespace TestTraitsReflectionByDefinition
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraitsQuickStart.Foundation.Reflection.ReflectionByDefinition;

    [TestClass]
    public class TestReflectionDefinitionManager
    {
        [TestMethod]
        public void TestCreateReflectionDefinitionEmptyPaths()
        {
            var providerRootPath = "";
            var reflectionDefinitionPath = "";

            var reflectionManager = new ReflectionManager()
            {
                ProviderRootFolder = providerRootPath,
                ReflectionDefinitionFolder = reflectionDefinitionPath
            };

            #region Traits Related

            var reflectionDefinition = new ReflectionDefinition()
            {
                AssemblyName = "TraitsQuickStart.Features.Pipelines.StorageProviders",
                AssemblyPath = "{PROVIDER_ROOT_PATH}",
                TypeName = "TraitsQuickStart.Features.Pipelines.StorageProviders.FileTraitsPipelineProvider",
                UseActivator = false
            };
            Assert.IsTrue(reflectionManager.CreateReflectionDefinition("PROVIDER_PIPELINE_STORAGE_PROVIDER_FILE", reflectionDefinition));

            reflectionDefinition = new ReflectionDefinition()
            {
                AssemblyName = "TraitsQuickStart.Features.Pipelines.StorageProviders",
                AssemblyPath = "{PROVIDER_ROOT_PATH}",
                TypeName = "TraitsQuickStart.Features.Pipelines.StorageProviders.StringTraitsPipelineProvider",
                UseActivator = false
            };
            Assert.IsTrue(reflectionManager.CreateReflectionDefinition("PROVIDER_PIPELINE_STORAGE_PROVIDER_STRING", reflectionDefinition));

            #endregion
        }
    }
}
