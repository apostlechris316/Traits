namespace TestTraitsBusiness
{
    using CSHARPStandard.Data.Common;
    using Rwc.Traits.Business;
    using Rwc.Traits.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

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

            dataManager.ApplyTrait(new CustomField()
            {
                FieldName = "TestFieldName",
                FieldValue = "TestFieldValue"
            });
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
                dataManager.ApplyTrait(new CustomField()
                {
                     FieldName = "TestFieldName",
                     FieldValue = "TestFieldValue"
                })
            );
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
