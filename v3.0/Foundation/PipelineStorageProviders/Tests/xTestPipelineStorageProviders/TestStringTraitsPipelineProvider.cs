namespace TestPipelineStorageProviders
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using TraitsQuickStart.Features.Pipelines;
    using TraitsQuickStart.Foundation.PipelineStorageProviders;

    [TestClass]
    public class TestStringTraitsPipelineProvider
    {
        [TestMethod]
        public void TestLoadStringTraitsPipelineEmptyString()
        {
            var reader = new StringTraitsPipelineProvider();
            ITraitsPipeline traitsPipeline = null;
            Assert.ThrowsException<ArgumentNullException>(() => traitsPipeline = reader.LoadTraitsPipeline());
        }

        [TestMethod]
        public void TestLoadStringTraitsPipelineTestString()
        {
            var connectionString = "{\"ErrorMode\":\"TEST_ERROR_MODE\",\"PipelineExecutionMode\":\"TEST_PIPELINE_EXECUTION_MODE\",\"PipelineSteps\":[{\"__type\":\"TraitsPipelineStep:#TraitsQuickStart.Features.Pipelines.Data\",\"Provider\":null,\"ProviderName\":\"PipelineStep1\"},{\"__type\":\"TraitsPipelineStep:#TraitsQuickStart.Features.Pipelines.Data\",\"Provider\":null,\"ProviderName\":\"PipelineStep2\"}]}";

            var reader = new StringTraitsPipelineProvider()
            {
                ConnectionString = connectionString
            };
            var traitsPipeline = reader.LoadTraitsPipeline();

            Assert.IsNotNull(traitsPipeline);
        }

        [TestMethod]
        public void TestSaveStringTraitsPipelineEmptyString()
        {
            ITraitsPipelineWriter writer = new StringTraitsPipelineProvider();
            Assert.ThrowsException<ArgumentNullException>(() => writer.SaveTraitsPipeline(null));
        }

        [TestMethod]
        public void TestSaveStringTraitsPipelineTestString()
        {
            ITraitsPipeline traitsPipeline = new TraitsPipeline()
            {
                 ErrorMode = "TEST_ERROR_MODE",
                PipelineExecutionMode = "TEST_PIPELINE_EXECUTION_MODE"
            };
            traitsPipeline.PipelineSteps.Add(
                new TraitsPipelineStep()
                {
                    ProviderName = "PipelineStep1"
                });
            traitsPipeline.PipelineSteps.Add(
                new TraitsPipelineStep()
                {
                    ProviderName = "PipelineStep2"
                }
            );

            ITraitsPipelineWriter writer = new StringTraitsPipelineProvider();
            writer.SaveTraitsPipeline(traitsPipeline);

            Assert.IsFalse(string.IsNullOrEmpty(writer.ConnectionString));
        }
    }
}
