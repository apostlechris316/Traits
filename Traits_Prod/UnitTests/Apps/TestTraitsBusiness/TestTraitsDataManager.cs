namespace TestTraitsBusiness
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StandardDataStructureQuickStart.Data;
    using System;
    using TraitsQuickStart.Business;
    using TraitsQuickStart.Data;

    /// <summary>
    /// Unit Tests the Data Manager
    /// </summary>
    [TestClass]
    public class TestTraitsDataManager
    {
        [TestMethod]
        public void TestDataManagerApplyTraitSuccessNoStatusObject()
        {
            var dataManager = new TraitsDataManager
            {
                Traits = new BaseTraits(),
                ConnectionString = string.Empty,
                Status = null
            };

            dataManager.ApplyTrait(new CustomField("TestFieldName","TestFieldValue"));
        }

        [TestMethod]
        public void TestDataManagerApplyTraitWithNullTraitsCollectionAndTraitPair()
        {
            var dataManager = new TraitsDataManager
            {
                Traits = null,
                ConnectionString = string.Empty,
                Status = null
            };

            Assert.ThrowsException<ArgumentNullException>(() => 
                dataManager.ApplyTrait(new CustomField("TestFieldName", "TestFieldValue")));
        }
        [TestMethod]
        public void TestDataManagerApplyTraitWithNullTraitPair()
        {
            var dataManager = new TraitsDataManager
            {
                Traits = new BaseTraits(),
                ConnectionString = string.Empty,
                Status = null
            };

            Assert.ThrowsException<ArgumentNullException>(() => dataManager.ApplyTrait(null));
        }
    }
}
