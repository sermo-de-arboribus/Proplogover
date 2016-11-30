using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
