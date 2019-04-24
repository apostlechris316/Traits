namespace TestPipelineStorageProviders
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using TraitsQuickStart.Features.Pipelines.Data;
    using TraitsQuickStart.Features.Pipelines.StorageProviders;

    [TestClass]
    public class TestFileTraitsPipelineProvider
    {
        [TestMethod]
        public void TestLoadFileTraitsPipelineNoFilename()
        {
            var reader = new FileTraitsPipelineProvider();
            Assert.ThrowsException<ArgumentNullException>(() => reader.LoadTraitsPipeline());
        }

        [TestMethod]
        public void TestLoadFileTraitsPipelineTestFile()
        {
            var testFileName = "TestPipeline.json";
            var reader = new FileTraitsPipelineProvider()
            {
                ConnectionString = testFileName
            };
            var traitsPipeline = reader.LoadTraitsPipeline();

            Assert.IsNotNull(traitsPipeline);
        }

        [TestMethod]
        public void TestSaveFileTraitsPipelineNoFilename()
        {
            var traitsPipeline = new TraitsPipeline()
            {
                ErrorMode = "STOP_ON_ERROR",
                PipelineExecutionMode = "ALL_MATCHING",
            };
            traitsPipeline.PipelineSteps.Add(new TraitsPipelineStep()
            {
                ProviderName = "TEST_PROVIDER_NAME"
            });

            var writer = new FileTraitsPipelineProvider();

            Assert.ThrowsException<ArgumentNullException>(() => writer.SaveTraitsPipeline(traitsPipeline));
        }

        [TestMethod]
        public void TestSaveFileTraitsPipelineTestFile()
        {
            var testFileName = "TestPipelineSave.json";
            var traitsPipeline = new TraitsPipeline()
            {
                ErrorMode = "STOP_ON_ERROR",
                PipelineExecutionMode = "ALL_MATCHING",
            };
            traitsPipeline.PipelineSteps.Add(new TraitsPipelineStep()
            {
                ProviderName = "TEST_PROVIDER_NAME"
            });

            var writer = new FileTraitsPipelineProvider()
            {
                ConnectionString = testFileName
            };

            Assert.IsTrue(writer.SaveTraitsPipeline(traitsPipeline));
        }
    }
}
