namespace Proplogover
{
    /// <summary>
    /// This class represents a boolean variable which appears as a literal in a formula of propositional logic.
    /// Every literal has a name, a value and a sign (indicating if the value is to be modified by a NOT operator)
    /// </summary>
    public class Literal
    {
        #region Properties

        public string Name { get; private set; }
        public bool Value { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of a Literal
        /// </summary>
        /// <param name="name">The (immutable) name associated with this literal.</param>
        /// <param name="sign">The (immutable) sign associated with this literal.</param>
        public Literal(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Convenience constructor, assigns an initial value to the literal
        /// </summary>
        /// <param name="name">The (immutable) name associated with this literal.</param>
        /// <param name="sign">The (immutable) sign associated with this literal.</param>
        /// <param name="value">The initial value of this literal</param>
        public Literal(string name, bool value) : this (name)
        {
            Value = value;
        }

        #endregion

        #region Public instance methods

        public override bool Equals(object otherObject)
        {
            if(otherObject == null || GetType() != otherObject.GetType())
            {
                return false;
            }

            Literal otherLiteral = otherObject as Literal;
            return this.Name == otherLiteral.Name;
        }


        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}