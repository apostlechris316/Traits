namespace TestTraitsBusiness
{
    using CSHARPStandard.Traits.Business;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class TestTraitHelper
    {
        [TestMethod]
        public void TestTraitHelperGetTraitsEmptyTraitsPathAndFilename()
        {
            var traitsHelper = new TraitHelper();
            var traits = traitsHelper.GetTraits();
        }

        [TestMethod]
        public void TestTraitHelperGetTraitsEmptyTraitsPath()
        {
            var traitsHelper = new TraitHelper()
            {
                 TraitFileName = "TestTraits"
            };
            var traits = traitsHelper.GetTraits();
        }

        [TestMethod]
        public void TestTraitHelperGetTraits()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = "c:\\",
                 TraitFileName = "TestTraits"
            };

            var traits = traitsHelper.GetTraits();
        }

        [TestMethod]
        public void TestTraitHelperGetVariableTraits()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = "c:\\",
                TraitFileName = "TestTraits"
            };

            var traits = traitsHelper.GetVariableTraits(traitsHelper.GetTraits());
        }

        [TestMethod]
        public void TestTraitHelperApplyTraitNullTraitsAndNullPair()
        {
            var traitsHelper = new TraitHelper();

            Assert.ThrowsException<ArgumentNullException>(() => traitsHelper.ApplyTrait(null, null));
        }

        [TestMethod]
        public void TestTraitHelperApplyTraitNullTraitPair()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = "c:\\",
                TraitFileName = "TestTraits"
            };

            Assert.ThrowsException<ArgumentNullException>(() => traitsHelper.ApplyTrait(traitsHelper.GetTraits(), null));
        }

        [TestMethod]
        public void TestTraitHelperSaveTraitNullTraits()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = "c:\\",
                TraitFileName = "TestTraits"
            };

            Assert.ThrowsException<ArgumentNullException>(() => traitsHelper.SaveTraits(null));
        }

        [TestMethod]
        public void TestTraitHelperSaveTrait()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = "c:\\",
                TraitFileName = "TestTraits"
            };

            traitsHelper.SaveTraits(traitsHelper.GetTraits());
        }

        [TestMethod]
        public void TestTraitHelperGetTraitTemplateNoTemplatePathSupplied()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = "c:\\",
                TraitFileName = "TestTraits"
            };

            Assert.ThrowsException<ArgumentException>(() => traitsHelper.GetTraitTemplate(string.Empty));
        }

        [TestMethod]
        public void TestTraitHelperGetTraitTemplate()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = "c:\\",
                TraitFileName = "TestTraits"
            };

            traitsHelper.GetTraitTemplate("c:\\TestTraitsTemplate.json");
        }
    }
}
