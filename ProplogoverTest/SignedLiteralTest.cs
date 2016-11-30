using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proplogover;

namespace ProplogoverTest
{
    [TestClass]
    public class SignedLiteralTest
    {
        #region Test string representation of literals

        [TestMethod]
        public void Should_display_negative_literal_with_not_sign()
        {
            SignedLiteral literal = new SignedLiteral("A", true);

            Assert.AreEqual(literal.ToString(), "¬A");
        }

        [TestMethod]
        public void Should_display_positive_literal_without_not_sign()
        {
            SignedLiteral literal = new SignedLiteral("A", false);

            Assert.AreEqual(literal.ToString(), "A");
        }

        #endregion

        #region Test evaluation of literals

        [TestMethod]
        public void Should_evaluate_positive_literal_with_value_true_to_true()
        {
            SignedLiteral literal = new SignedLiteral("A", false);
            literal.Value = true;

            Assert.IsTrue(literal.Evaluate());
        }

        [TestMethod]
        public void Should_evaluate_positive_literal_with_value_false_to_false()
        {
            SignedLiteral literal = new SignedLiteral("A", false);
            literal.Value = false;

            Assert.IsFalse(literal.Evaluate());
        }

        [TestMethod]
        public void Should_evaluate_negative_literal_with_value_true_to_false()
        {
            SignedLiteral literal = new SignedLiteral("A", true);
            literal.Value = true;

            Assert.IsFalse(literal.Evaluate());
        }

        [TestMethod]
        public void Should_evaluate_negative_literal_with_value_false_to_true()
        {
            SignedLiteral literal = new SignedLiteral("A", true);
            literal.Value = false;

            Assert.IsTrue(literal.Evaluate());
        }
        #endregion
    }
}