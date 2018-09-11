namespace TestTraitsData
{
    using CSHARPStandard.Data.Common;
    using CSHARPStandard.Traits.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestBaseTraits
    {
        [TestMethod]
        public void TestAddingTraitPairs()
        {
            BaseTraits baseTraits = new BaseTraits();
            var testCustomField = new CustomField()
            {
                FieldName = "TestFieldName",
                FieldValue = "TestFieldValue"
            };

            baseTraits.TraitPairs.Add(testCustomField);

            Assert.AreEqual(testCustomField, baseTraits.TraitPairs[0]);
        }
    }
}
