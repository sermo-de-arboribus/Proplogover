using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proplogover;
using System.Collections.Generic;

namespace ProplogoverTest
{
    [TestClass]
    public class ClauseTest
    {
        #region Test string representation of clauses

        [TestMethod]
        public void Should_display_clauses_with_or_signs()
        {
            // Arrange
            SignedLiteral a = new SignedLiteral("A", false);
            SignedLiteral b = new SignedLiteral("B", false);
            Clause clause = new Clause(new List<SignedLiteral>() { a, b });

            // Act
            string clauseAsString = clause.ToString();

            // Assert
            Assert.AreEqual(clauseAsString, "(A ∨ B)");
        }

        [TestMethod]
        public void Should_display_clauses_with_single_literal_correctly()
        {
            // Arrange
            SignedLiteral a = new SignedLiteral("A", false);
            Clause clause = new Clause(new List<SignedLiteral>() { a });

            // Act
            string clauseAsString = clause.ToString();

            // Assert
            Assert.AreEqual(clauseAsString, "(A)");
        }

        [TestMethod]
        public void Should_swallow_duplicate_literals()
        {
            // Arrange
            SignedLiteral a = new SignedLiteral("A", false);
            SignedLiteral b = new SignedLiteral("B", true);
            Clause clause = new Clause(new List<SignedLiteral>() { a, a, b, b });

            // Act
            string clauseAsString = clause.ToString();

            // Assert
            Assert.AreEqual(clauseAsString, "(A ∨ ¬B)");
        }

        #endregion

        #region Test evaluation of clauses

        [TestMethod]
        public void Should_evaluate_clause_with_all_true_literals_to_true()
        {
            // Arrange
            SignedLiteral a = new SignedLiteral("A", false);
            SignedLiteral b = new SignedLiteral("B", false);
            SignedLiteral c = new SignedLiteral("C", false);
            a.Value = true;
            b.Value = true;
            c.Value = true;
            Clause clause = new Clause(new List<SignedLiteral>() { a, b, c });

            // Act
            bool clauseEvaluation = clause.Evaluate();

            // Assert
            Assert.IsTrue(clauseEvaluation);
        }

        [TestMethod]
        public void Should_evaluate_clause_with_mixed_valued_literals_to_true()
        {
            // Arrange
            SignedLiteral a = new SignedLiteral("A", false, false);
            SignedLiteral b = new SignedLiteral("B", false, true);
            SignedLiteral c = new SignedLiteral("C", false, false);
            Clause clause = new Clause(new List<SignedLiteral>() { a, b, c });

            // Act
            bool clauseEvaluation = clause.Evaluate();

            // Assert
            Assert.IsTrue(clauseEvaluation);
        }

        [TestMethod]
        public void Should_evaluate_clause_with_all_false_literals_to_false()
        {
            // Arrange
            SignedLiteral a = new SignedLiteral("A", false, false);
            SignedLiteral b = new SignedLiteral("B", false, false);
            SignedLiteral c = new SignedLiteral("C", false, false);
            Clause clause = new Clause(new List<SignedLiteral>() { a, b, c });

            // Act
            bool clauseEvaluation = clause.Evaluate();

            // Assert
            Assert.IsFalse(clauseEvaluation);
        }

        [TestMethod]
        public void Should_evaluate_clauses_with_negated_values_correctly()
        {
            // Arrange
            SignedLiteral a = new SignedLiteral("A", true, false); // evaluates to true
            SignedLiteral b = new SignedLiteral("B", false, false); // evaluates to false
            SignedLiteral c = new SignedLiteral("C", false, true); // evaluates to true
            SignedLiteral d = new SignedLiteral("D", true, true); // evaluates to false
            Clause clause1 = new Clause(new List<SignedLiteral>() { a, b, c });
            Clause clause2 = new Clause(new List<SignedLiteral>() { a, c });
            Clause clause3 = new Clause(new List<SignedLiteral>() { a, b });
            Clause clause4 = new Clause(new List<SignedLiteral>() { b, d });

            // Act
            bool clauseEvaluation1 = clause1.Evaluate();
            bool clauseEvaluation2 = clause2.Evaluate();
            bool clauseEvaluation3 = clause3.Evaluate();
            bool clauseEvaluation4 = clause4.Evaluate();

            // Assert
            Assert.IsTrue(clauseEvaluation1);
            Assert.IsTrue(clauseEvaluation2);
            Assert.IsTrue(clauseEvaluation3);
            Assert.IsFalse(clauseEvaluation4);
        }

        [TestMethod]
        public void Should_evaluate_empty_clause_to_true()
        {
            // Arrange
            Clause clause = new Clause(new List<SignedLiteral>() {});

            // Act
            bool clauseEvaluation = clause.Evaluate();

            // Assert
            Assert.IsTrue(clauseEvaluation);
        }

        #endregion
    }
}
