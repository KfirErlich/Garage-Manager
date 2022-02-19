using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue, string i_Message):
            base(string.Format("{0} \nThe input should be between {1} to {2}", i_Message, i_MinValue, i_MaxValue))
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }

        public float MaxValue
        {
            get { return r_MaxValue; }
        }
        public float MinValue
        {
            get { return r_MinValue; }
        }

    }
}
