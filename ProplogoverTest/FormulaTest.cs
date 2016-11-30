using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proplogover;
using System.Collections.Generic;

namespace ProplogoverTest
{
    [TestClass]
    public class FormulaTest
    {
        #region Test for string representation of formulas

        [TestMethod]
        public void Should_display_formula_correctly()
        {
            // Arrange
            Literal a = new Literal("A", false);
            Literal b = new Literal("B", false);
            Literal c = new Literal("C", true);
            Clause clause1 = new Clause(new List<Literal>() { a, b });
            Clause clause2 = new Clause(new List<Literal>() { a, c });
            Formula formula = new Formula(new List<Clause>() { clause1, clause2 });

            // Act
            string formulaAsString = formula.ToString();

            // Assert
            Assert.AreEqual(formulaAsString, "(A ∨ B) ∧ (A ∨ ¬C)");
        }

        #endregion

        #region Test evaluation of formulas

        public void Should_evaluate_formula_correctly()
        {
            // Arrange
            Literal a = new Literal("A", false, true); // evaluates to true
            Literal b = new Literal("B", false, false); // evaluates to false
            Literal c = new Literal("C", true, true); // evaluates to false
            Clause clause1 = new Clause(new List<Literal>() { a, b }); // evaluates to true
            Clause clause2 = new Clause(new List<Literal>() { a, c }); // evaluates to true
            Formula formula = new Formula(new List<Clause>() { clause1, clause2 });

            // Act
            bool formulaEvaluation = formula.Evaluate();

            // Assert
            Assert.IsTrue(formulaEvaluation);

            // Re-arrange
            a.Value = false; // both clauses now evaluate to false

            // Act again
            formulaEvaluation = formula.Evaluate();

            //Assert
            Assert.IsFalse(formulaEvaluation);
        }

        #endregion
    }
}
