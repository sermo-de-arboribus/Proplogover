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
            Formula formula = TestFormula.GetTestFormula();

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
            SignedLiteral a = new SignedLiteral("A", false, true); // evaluates to true
            SignedLiteral b = new SignedLiteral("B", false, false); // evaluates to false
            SignedLiteral c = new SignedLiteral("C", true, true); // evaluates to false
            Clause clause1 = new Clause(new List<SignedLiteral>() { a, b }); // evaluates to true
            Clause clause2 = new Clause(new List<SignedLiteral>() { a, c }); // evaluates to true
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

        #region Test satisfiability checks

        [TestMethod]
        public void Should_classify_formula_as_satisfiable()
        {
            Formula formula = TestFormula.GetTestFormula();

            Assert.IsTrue(formula.IsSatisfiable(null));
        }

        [TestMethod]
        public void Should_return_all_models_for_satisfiable_formula()
        {
            Formula formula = TestFormula.GetTestFormula();
            List<string> models = new List<string>();

            bool isSatisfiable = formula.IsSatisfiable(models);

            Assert.IsTrue(isSatisfiable);
            Assert.IsTrue(models.Contains("A = False, B = True, C = False"));
            Assert.IsTrue(models.Contains("A = True, B = False, C = False"));
            Assert.IsTrue(models.Contains("A = True, B = False, C = True"));
            Assert.IsTrue(models.Contains("A = True, B = True, C = False"));
            Assert.IsTrue(models.Contains("A = True, B = True, C = True"));
            Assert.IsFalse(models.Contains("A = False, B = False, C = False"));
            Assert.IsFalse(models.Contains("A = False, B = False, C = True"));
            Assert.IsFalse(models.Contains("A = False, B = True, C = True"));
        }

        [TestMethod]
        public void Should_return_all_models_for_satisfiable_formula2()
        {
            Formula formula = TestFormula.GetTestFormula2();
            List<string> models = new List<string>();

            bool isSatisfiable = formula.IsSatisfiable(models);

            Assert.IsTrue(isSatisfiable);
            Assert.IsTrue(models.Contains("A = False, B = True, C = False"));
            Assert.IsTrue(models.Contains("A = True, B = False, C = True"));
            Assert.IsTrue(models.Contains("A = True, B = True, C = False"));
            Assert.IsTrue(models.Contains("A = True, B = True, C = True"));
            Assert.IsFalse(models.Contains("A = False, B = False, C = False"));
            Assert.IsFalse(models.Contains("A = False, B = False, C = True"));
            Assert.IsFalse(models.Contains("A = False, B = True, C = True"));
            Assert.IsFalse(models.Contains("A = True, B = False, C = False"));
        }

        #endregion

        static class TestFormula
        {
            // Returns the following formula: (A ∨ B) ∧ (A ∨ ¬C)
            static internal Formula GetTestFormula()
            {
                SignedLiteral a = new SignedLiteral("A", false);
                SignedLiteral b = new SignedLiteral("B", false);
                SignedLiteral c = new SignedLiteral("C", true);
                Clause clause1 = new Clause(new List<SignedLiteral>() { a, b });
                Clause clause2 = new Clause(new List<SignedLiteral>() { a, c });
                return new Formula(new List<Clause>() { clause1, clause2 });
            }

            // Returns the following formula: (A ∨ B) ∧ (A ∨ ¬C) ∧ (B ∨ C)
            static internal Formula GetTestFormula2()
            {
                SignedLiteral a = new SignedLiteral("A", false);
                SignedLiteral b = new SignedLiteral("B", false);
                SignedLiteral c1 = new SignedLiteral("C", true);
                SignedLiteral c2 = new SignedLiteral("C", false);
                Clause clause1 = new Clause(new List<SignedLiteral>() { a, b });
                Clause clause2 = new Clause(new List<SignedLiteral>() { a, c1 });
                Clause clause3 = new Clause(new List<SignedLiteral>() { b, c2 });
                return new Formula(new List<Clause>() { clause1, clause2, clause3 });
            }
        }
    }
}
