namespace Software.Engineer
{
    public class EquationCalculation : BaseEquationCalculation
    {
        #region Constructors
        public EquationCalculation(int c, float[] array) : base(c, array)
        {
        }

        public EquationCalculation(int c, float[] array, int length) : base(c, array, length)
        {
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Calculate float Equation
        /// </summary>
        /// <param name="k">k co-ordinate within float array</param>
        /// <param name="j">j co-ordinate within float array</param>
        /// <returns>returns float result</returns>
        public override float GetResult(int k, int j)
        {
            float sumOfArray = 0;

            for (int i = _c; i < _length; i++)
            {
                sumOfArray += (_array[i - k] * _array[i - j]);
            }

            return (float)System.Math.Round(sumOfArray, 6);
        }
        #endregion Methods
    }
}
