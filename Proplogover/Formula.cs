using Proplogover.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proplogover
{
    /// <summary>
    /// This class represents a CNF formula of propositional logic as a conjunction of clauses. Note that it is not thread-safe
    /// </summary>
    public class Formula
    {
        #region Private fields

        List<Clause> _formula;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor that initializes an object with an empty formula
        /// </summary>
        public Formula()
        {
            _formula = new List<Clause>();
        }

        /// <summary>
        /// Constructor that would set the formula on instantiation
        /// </summary>
        /// <param name="formula">The formula to be represented by this object, given as a conjunction of clauses</param>
        public Formula(IEnumerable<Clause> formula)
        {
            _formula = formula.ToList();
        }

        #endregion

        #region Public instance methods

        /// <summary>
        /// Adds a clause to the formula that this object currently represents
        /// </summary>
        /// <param name="clause">The clause to be added to the CNF formula</param>
        public void AddClause(Clause clause)
        {
            _formula.Add(clause);
        }

        /// <summary>
        /// This function evaluates the current assignment of values in this conjunction of clauses
        /// </summary>
        /// <returns>True if the conjunction of clauses in this formula under the current assignment evaluates to true</returns>
        public bool Evaluate()
        {
            IEnumerator<Clause> it = _formula.GetEnumerator();

            bool result;
            if (it.MoveNext())
            {
                // initial assignment to result variable
                result = it.Current.Evaluate();
                while (it.MoveNext())
                {
                    result &= it.Current.Evaluate();
                }
            }
            else
            {
                // we are dealing with an empty formula
                result = true;
            }

            return result;
        }

        public IEnumerable<Literal> GetAllSignedLiterals()
        {
            return _formula.SelectMany(c => c.GetAllSignedLiterals()).Distinct();
        }

        public UnsignedLiteralsCollection[] GetAllLiterals()
        {
            List<UnsignedLiteralsCollection> result = new List<UnsignedLiteralsCollection>();

            IEnumerable<IGrouping<string, Literal>> litGroups = GetAllSignedLiterals().GroupBy(l => l.Name);

            foreach(var litGroup in litGroups)
            {
                UnsignedLiteralsCollection collection = new UnsignedLiteralsCollection();
                collection.LiteralName = litGroup.Key;
                collection.AddLiteralRange(litGroup.AsEnumerable());
                result.Add(collection);
            }

            return result.ToArray();
        }

        /// <summary>
        /// This method checks, if the formula is satisfiable, i.e. if any assignment exists that evaluates to "true"
        /// Note that the complexity of of this method is O(2^n), where n is the number of different literals in the formula.
        /// So the method may take a long time for large values of n.
        /// The method takes a List<string> models parameter, which may be null. If it is null, the method call returns true
        /// right after it finds the first satisfiable assignment. If the models parameter is not null, the method
        /// will check through all assignments and store each assignment that is a model of the formula in the list.
        /// </summary>
        /// <param name="models">The list for storing the models, or null, if the method should return right after the first models is found.</param>
        /// <returns>true, if the formula is satisfiable, false otherwise</returns>
        public bool IsSatisfiable(List<string> models)
        {
            bool result = false;

            UnsignedLiteralsCollection[] allLiterals = GetAllLiterals();

            if (allLiterals.Count() > 32)
            {
                throw new ArgumentException("IsSatisfiable cannot work on formulas with more than 32 variables.");
            }

            // the truth table has 2^n rows
            int numberOfRows = (1 << (allLiterals.Count()));
            int numberOfLiterals = allLiterals.Count();

            // iterate through the truth table and test all assignments
            for (int i = 0; i < numberOfRows; i++)
            {
                // (re-)set the values of the literals
                for (int j = 0; j < numberOfLiterals; j++)
                {
                    // this will alternate values depending on their position in the literal list.
                    // if there are literals a, b, c, the following truth table would be built:
                    // i   j   (1 << j)   a   b   c 
                    // =============================
                    // 0   0       1      f   *   *
                    // 0   1       2      f   f   *
                    // 0   2       4      f   f   f
                    // 1   0       1      t   *   *
                    // 1   1       2      t   f   *
                    // 1   2       4      t   f   f
                    // 2   0       1      f   *   *
                    // 2   1       2      f   t   *
                    // 2   2       4      f   t   f
                    // 3   0       1      t   *   *
                    // 3   1       2      t   t   *
                    // 3   2       4      t   t   f
                    // etc....
                    allLiterals[j].SetValue((i & (1 << j)) != 0);
                }

                // check if the current assignment is a model of the formula
                result = this.Evaluate();
                if (result && null == models)
                {
                    return true;
                }
                else if (result)
                {
                    models.Add(PrintCurrentFormulaAssignment(allLiterals));
                }
            }

            return result;
        }

        public string PrintCurrentFormulaAssignment(UnsignedLiteralsCollection[] allLiterals = null)
        {
            if(null == allLiterals)
            {
                allLiterals = GetAllLiterals();
            }

            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach(UnsignedLiteralsCollection lit in allLiterals)
            {
                sb.Append(lit.LiteralName + " = " + lit.GetValue());
                if(i < allLiterals.Count())
                {
                    sb.Append(", ");
                }
                i++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Removes the first occurrance of a clause within the CNF formular that this object currently represents
        /// </summary>
        /// <param name="clause">The clause to be removed</param>
        /// <returns>True, if object was successfully removed; false if clause could not be removed or was not contained in the formula</returns>
        public bool RemoveClause(Clause clause)
        {
            return _formula.Remove(clause);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            IEnumerator<Clause> it = _formula.GetEnumerator();
            int i = 1;
            while (it.MoveNext())
            {
                sb.Append(it.Current.ToString());
                if (i < _formula.Count)
                {
                    sb.Append(" ∧ ");
                }
                i++;
            }
            return sb.ToString();
        }

        #endregion
    }
}
