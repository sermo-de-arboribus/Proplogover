using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proplogover
{
    /// <summary>
    /// The signed literal is a literal that is either negated or not negated (e.g. A or ¬A)
    /// </summary>
    public class SignedLiteral : Literal
    {
        /// <summary>
        /// The Sign represents the NOT operator, which is treated as a property of the Literal.
        /// Sign == false means that there is no NOT operator, Sign == true means a NOT operator is used
        /// </summary>
        public bool Sign { get; private set; }

        public SignedLiteral(string name, bool sign) : base (name)
        {
            Sign = sign;
        }

        public SignedLiteral(string name, bool sign, bool value) : base (name, value)
        {
            Sign = sign;
        }

        public override bool Equals(object otherObject)
        {
            if (otherObject == null || GetType() != otherObject.GetType())
            {
                return false;
            }

            SignedLiteral otherLiteral = otherObject as SignedLiteral;
            return this.Name == otherLiteral.Name && this.Sign == otherLiteral.Sign;
        }

        /// <summary>
        /// Evaluation of this Literal.
        /// </summary>
        /// <returns>This method returns the current value of this Literal, considering the NOT operator / Sign.</returns>
        public bool Evaluate()
        {
            return Sign ? !Value : Value;
        }


        public override int GetHashCode()
        {
            return 13 * Name.GetHashCode() + 17 * Sign.GetHashCode();
        }

        public override string ToString()
        {
            return (Sign ? "¬" : "") + Name;
        }
    }
}
