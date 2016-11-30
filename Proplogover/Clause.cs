using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proplogover
{
    /// <summary>
    /// A clause is a disjunction of literals, as it can appear in a propositional logic formula in CNF
    /// </summary>
    public class Clause
    {
        #region Private fields

        private HashSet<Literal> _clause;

        #endregion

        #region Constructor

        public Clause(IEnumerable<Literal> clauseLiterals)
        {
            _clause = new HashSet<Literal>(clauseLiterals);
        }

        #endregion

        #region Public instance methods

        /// <summary>
        /// This function evaluates the current assignment of values in this disjunction of literals
        /// </summary>
        /// <returns>True if the disjunction of literals in this clause under the current assignment evaluates to true</returns>
        public bool Evaluate()
        {
            IEnumerator<Literal> it = _clause.GetEnumerator();

            bool result;
            if(it.MoveNext())
            {
                // initial assignment to result variable
                result = it.Current.Evaluate();
                while(it.MoveNext())
                {
                    result |= it.Current.Evaluate();
                }
            }
            else
            {
                // we are dealing with an empty clause
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Returns this clause as a disjunction of literals
        /// </summary>
        /// <returns>A string of the literals in alphabetical order with corresponding negation and disjunction operators</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");

            int i = 1;
            foreach (Literal lit in _clause.Select(c => c).OrderBy(c => c.Name))
            {
                sb.Append(lit.ToString());
                if (i < _clause.Count)
                {
                    sb.Append(" ∨ ");
                }
                i++;
            }
            sb.Append(")");
            return sb.ToString();
        }

        #endregion
    }
}
