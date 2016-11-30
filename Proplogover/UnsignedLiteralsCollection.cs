using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proplogover
{
    /// <summary>
    /// The UnsignedLiteralsCollection class groups SignedLiterals of the same name together. 
    /// Within a clause, a literal of the same name, but with different signs (¬ or no ¬) are different objects,
    /// but when assigning a value to a literal variable in a formula, we need to ensure that all literals of
    /// the same name are assigned the same value. This is what the UnsignedLiteralsCollection takes care of.
    /// </summary>
    public class UnsignedLiteralsCollection
    {
        #region Properties

        /// <summary>
        /// The name of the literal represented by this object
        /// </summary>
        public string LiteralName { get; set; }
        /// <summary>
        /// All the literals that are grouped together under the same name
        /// </summary>
        public List<Literal> Literals { get; private set; }

        #endregion

        #region Constructor

        public UnsignedLiteralsCollection()
        {
            Literals = new List<Literal>();
        }

        #endregion

        #region Public instance methods

        public void AddLiteral(Literal lit)
        {
            Literals.Add(lit);
        }

        public void AddLiteralRange(IEnumerable<Literal> literals)
        {
            Literals.AddRange(literals);
        }

        public bool GetValue()
        {
            if(Literals.Any())
            {
                return Literals.First().Value;
            }
            else
            {
                return false;
            }
        }

        public void SetValue(bool value)
        {
            foreach (Literal lit in Literals)
            {
                lit.Value = value;
            }
        }

        #endregion
    }
}
