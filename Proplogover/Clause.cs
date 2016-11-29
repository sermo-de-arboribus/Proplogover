using System.Collections.Generic;

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
            _clause.UnionWith(clauseLiterals);
        }

        #endregion

        #region Public instance methods

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

        #endregion
    }
}
