using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PriceSetAPI.Utility
{
    public class Convert2NumTool<T>
    {
        public static T ConvertVal(object val)
        {
            T conval = default(T);
            try
            {
                conval=(T)Convert.ChangeType(val,typeof(T));
             
            }
            catch
            { }

            return conval;
        }

        public static T ConvertNumber(object val, int decimalPlace = 4)
        {
            T conval = default(T);

            if (val == DBNull.Value) return conval;
            if (val == null) return conval;
            //conval = (T)Convert.ChangeType(val, typeof(T));
            if (typeof(T) == typeof(double))
            {
                double num = Convert.ToDouble(val);
                num = Math.Round(num, decimalPlace);
                conval = (T)Convert.ChangeType(num, typeof(T));
            }
            else if (typeof(T) == typeof(decimal))
            {
                decimal num = Convert.ToDecimal(val);
                num = Math.Round(num, decimalPlace);
                conval = (T)Convert.ChangeType(num, typeof(T));
            }
            else
            {
                throw new Exception("This method only support double and decimal type only.");
            }


            return conval;
        }
    }
}