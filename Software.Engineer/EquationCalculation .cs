using System;
using System.Collections.Generic;
using System.Text;

namespace Software.Engineer
{
    public class EquationCalc
    {
        private readonly float[] _array;
        private readonly int _length;

        public EquationCalc(float[] array) : this(array, array.Length) { }

        public EquationCalc(float[] array, int length)
        {
            _array = array;
            _length = length;
        }

        public double GetResult(int v1, int v2)
        {
            throw new NotImplementedException();
        }
    }
}
