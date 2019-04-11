namespace Software.Engineer
{
    public interface IEquationCalculation
    {
        /// <summary>
        /// Calculate float Equation
        /// </summary>
        /// <param name="k">k co-ordinate within float array</param>
        /// <param name="j">j co-ordinate within float array</param>
        /// <returns>returns float result</returns>
        float GetResult(int k, int j);
    }
}