using System;

namespace Software.Engineer
{
    public abstract class BaseEquationCalculation : IEquationCalculation
    {
        #region Fields
        protected readonly float[] _array;
        protected readonly int _length;
        protected readonly int _c;
        #endregion Fields

        #region Constructors
        public BaseEquationCalculation(int c, float[] array) : this(c, array, array.Length) { }

        public BaseEquationCalculation(int c, float[] array, int length)
        {
            if(array == null)
                throw new ArgumentNullException("array cannot be null");

            if (c < 1 && c >= array.Length)
                throw new ArgumentException("c is out of bounds");

            if(length != array.Length)
                throw new ArgumentException($"in valid array length, expected {array.Length} got {length}");

            _array = array;
            _length = length;
            _c = c;
        }
        #endregion Constructors

        #region Methods
        public abstract float GetResult(int k, int j);
        #endregion Methods
    }
}
