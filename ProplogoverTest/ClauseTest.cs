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
            Literal a = new Literal("A", false);
            Literal b = new Literal("B", false);
            Clause clause = new Clause(new List<Literal>() { a, b });

            // Act
            string clauseAsString = clause.ToString();

            // Assert
            Assert.AreEqual(clauseAsString, "(A ∨ B)");
        }

        [TestMethod]
        public void Should_display_clauses_with_single_literal_correctly()
        {
            // Arrange
            Literal a = new Literal("A", false);
            Clause clause = new Clause(new List<Literal>() { a });

            // Act
            string clauseAsString = clause.ToString();

            // Assert
            Assert.AreEqual(clauseAsString, "(A)");
        }

        [TestMethod]
        public void Should_swallow_duplicate_literals()
        {
            // Arrange
            Literal a = new Literal("A", false);
            Literal b = new Literal("B", true);
            Clause clause = new Clause(new List<Literal>() { a, a, b, b });

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
            Literal a = new Literal("A", false);
            Literal b = new Literal("B", false);
            Literal c = new Literal("C", false);
            a.Value = true;
            b.Value = true;
            c.Value = true;
            Clause clause = new Clause(new List<Literal>() { a, b, c });

            // Act
            bool clauseEvaluation = clause.Evaluate();

            // Assert
            Assert.IsTrue(clauseEvaluation);
        }

        [TestMethod]
        public void Should_evaluate_clause_with_mixed_valued_literals_to_true()
        {
            // Arrange
            Literal a = new Literal("A", false, false);
            Literal b = new Literal("B", false, true);
            Literal c = new Literal("C", false, false);
            Clause clause = new Clause(new List<Literal>() { a, b, c });

            // Act
            bool clauseEvaluation = clause.Evaluate();

            // Assert
            Assert.IsTrue(clauseEvaluation);
        }

        [TestMethod]
        public void Should_evaluate_clause_with_all_false_literals_to_false()
        {
            // Arrange
            Literal a = new Literal("A", false, false);
            Literal b = new Literal("B", false, false);
            Literal c = new Literal("C", false, false);
            Clause clause = new Clause(new List<Literal>() { a, b, c });

            // Act
            bool clauseEvaluation = clause.Evaluate();

            // Assert
            Assert.IsFalse(clauseEvaluation);
        }

        [TestMethod]
        public void Should_evaluate_clauses_with_negated_values_correctly()
        {
            // Arrange
            Literal a = new Literal("A", true, false); // evaluates to true
            Literal b = new Literal("B", false, false); // evaluates to false
            Literal c = new Literal("C", false, true); // evaluates to true
            Literal d = new Literal("D", true, true); // evaluates to false
            Clause clause1 = new Clause(new List<Literal>() { a, b, c });
            Clause clause2 = new Clause(new List<Literal>() { a, c });
            Clause clause3 = new Clause(new List<Literal>() { a, b });
            Clause clause4 = new Clause(new List<Literal>() { b, d });

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
            Clause clause = new Clause(new List<Literal>() {});

            // Act
            bool clauseEvaluation = clause.Evaluate();

            // Assert
            Assert.IsTrue(clauseEvaluation);
        }

        #endregion
    }
}
