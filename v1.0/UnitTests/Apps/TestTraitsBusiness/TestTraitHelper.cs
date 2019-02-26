namespace TestTraitsBusiness
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraitsQuickStart.Business;
    using System;
    using StandardDataStructureQuickStart.Data;

    [TestClass]
    public class TestTraitHelper
    {
        public const string TestTraitsRootPath = "c:\\traits";

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
                TraitPath = TestTraitsRootPath,
                 TraitFileName = "TestTraits"
            };

            var traits = traitsHelper.GetTraits();
        }

        [TestMethod]
        public void TestTraitHelperGetVariableTraits()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = TestTraitsRootPath,
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
                TraitPath = TestTraitsRootPath,
                TraitFileName = "TestTraits"
            };

            Assert.ThrowsException<ArgumentNullException>(() => traitsHelper.ApplyTrait(traitsHelper.GetTraits(), null));
        }

        [TestMethod]
        public void TestTraitHelperSaveTraitNullTraits()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = TestTraitsRootPath,
                TraitFileName = "TestTraits"
            };

            Assert.ThrowsException<ArgumentNullException>(() => traitsHelper.SaveTraits(null));
        }

        [TestMethod]
        public void TestTraitHelperSaveTrait()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = TestTraitsRootPath,
                TraitFileName = "TestTraits"
            };

            var traits = traitsHelper.GetTraits();

            traits.TraitPairs.Add(new CustomField("TestTrait_" + DateTime.Now.Year + "_" + DateTime.Now.DayOfYear.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString(), DateTime.Now.Year.ToString()));
            traits.TraitPairs.Add(new CustomField("TestTrait2_" + DateTime.Now.Year + "_" + DateTime.Now.DayOfYear.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString(), DateTime.Now.Year.ToString()));
            traitsHelper.SaveTraits(traits);
        }

        [TestMethod]
        public void TestTraitHelperGetTraitTemplateNoTemplatePathSupplied()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = TestTraitsRootPath,
                TraitFileName = "TestTraits"
            };

            Assert.ThrowsException<ArgumentException>(() => traitsHelper.GetTraitTemplate(string.Empty));
        }

        [TestMethod]
        public void TestTraitHelperGetTraitTemplate()
        {
            var traitsHelper = new TraitHelper()
            {
                TraitPath = TestTraitsRootPath,
                TraitFileName = "TestTraits"
            };

            var traitsTemplateFilePath = TestTraitsRootPath + (!TestTraitsRootPath.EndsWith('\\') ? "\\" : "") + "TestTraitsTemplate.json";
            var traits = traitsHelper.GetTraitTemplate(traitsTemplateFilePath);
            if (traits == null) throw new Exception("ERROR: Cannot GetTraitTemplate");
        }
    }
}
