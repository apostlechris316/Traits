namespace TestTraitsData
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StandardDataStructureQuickStart.Data;
    using TraitsQuickStart.Data;

    [TestClass]
    public class TestBaseTraits
    {
        [TestMethod]
        public void TestAddingTraitPairs()
        {
            var baseTraits = new BaseTraits();
            var testCustomField = new CustomField("TestFieldName", "TestFieldValue");
            baseTraits.TraitPairs.Add(testCustomField);

            Assert.AreEqual(testCustomField, baseTraits.TraitPairs[0]);
        }
    }
}
