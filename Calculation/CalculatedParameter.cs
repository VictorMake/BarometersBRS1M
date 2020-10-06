using System.Collections.Generic;

namespace BarometersBRS1M
{
    /// <summary>
    /// Расчетные параметры
    /// </summary>
    /// <remarks></remarks>
    public class CalculatedParameter
    {
        //private const string conGвпр = "Gвпр";
        //private double mGвпр;
        //public double Gвпр
        //{
        //    get { return mGвпр; }
        //    set
        //    {
        //        mGвпр = value;
        //        CalcDictionary[conGвпр] = value;
        //    }
        //}

        //private const string conPiK = "PiK";
        //private double mPiK;
        //public double PiK
        //{
        //    get { return mPiK; }
        //    set
        //    {
        //        mPiK = value;
        //        CalcDictionary[conPiK] = value;
        //    }
        //}

        public Dictionary<string, double> CalcDictionary { get; set; }

        public CalculatedParameter()
        {
            CalcDictionary = new Dictionary<string, double>
            {
                //{ conGвпр, Gвпр },
                //{ conPiK, PiK }
            };
        }
    }
}
