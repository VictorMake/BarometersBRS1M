using System.Collections.Generic;

namespace BarometersBRS1M
{
    /// <summary>
    /// Входные аргументы
    /// </summary>
    /// <remarks></remarks>
    public class InputParameter
    {
        //private const string conБарометр = "Барометр";
        //private double mБарометр;
        //public double Барометр
        //{
        //    get { return mБарометр; }
        //    set
        //    {
        //        mБарометр = value;
        //        InputArgDictionary[conБарометр] = value;
        //    }
        //}

        //private const string conРстатРМК1 = "РстатРМК1";
        //private double mРстатРМК1;
        //public double РстатРМК1
        //{
        //    get { return mРстатРМК1; }
        //    set
        //    {
        //        mРстатРМК1 = value;
        //        InputArgDictionary[conРстатРМК1] = value;
        //    }
        //}

        //private const string conРстатРМК2 = "РстатРМК2";
        //private double mРстатРМК2;
        //public double РстатРМК2
        //{
        //    get { return mРстатРМК2; }
        //    set
        //    {
        //        mРстатРМК2 = value;
        //        InputArgDictionary[conРстатРМК2] = value;
        //    }
        //}

        //private const string conРполнРМК1 = "РполнРМК1";
        //private double mРполнРМК1;
        //public double РполнРМК1
        //{
        //    get { return mРполнРМК1; }
        //    set
        //    {
        //        mРполнРМК1 = value;
        //        InputArgDictionary[conРполнРМК1] = value;
        //    }
        //}

        //private const string conРполнРМК2 = "РполнРМК2";
        //private double mРполнРМК2;
        //public double РполнРМК2
        //{
        //    get { return mРполнРМК2; }
        //    set
        //    {
        //        mРполнРМК2 = value;
        //        InputArgDictionary[conРполнРМК2] = value;
        //    }
        //}

        //private const string conРизмНаВходе1 = "РизмНаВходе1";
        //private double mРизмНаВходе1;
        //public double РизмНаВходе1
        //{
        //    get { return mРизмНаВходе1; }
        //    set
        //    {
        //        mРизмНаВходе1 = value;
        //        InputArgDictionary[conРизмНаВходе1] = value;
        //    }
        //}

        //private const string conРизмНаВходе2 = "РизмНаВходе2";
        //private double mРизмНаВходе2;
        //public double РизмНаВходе2
        //{
        //    get { return mРизмНаВходе2; }
        //    set
        //    {
        //        mРизмНаВходе2 = value;
        //        InputArgDictionary[conРизмНаВходе2] = value;
        //    }
        //}

        //private const string conРизмПолнЗаКомпр1 = "РизмПолнЗаКомпр1";
        //private double mРизмПолнЗаКомпр1;
        //public double РизмПолнЗаКомпр1
        //{
        //    get { return mРизмПолнЗаКомпр1; }
        //    set
        //    {
        //        mРизмПолнЗаКомпр1 = value;
        //        InputArgDictionary[conРизмПолнЗаКомпр1] = value;
        //    }
        //}

        //private const string conРизмПолнЗаКомпр2 = "РизмПолнЗаКомпр2";
        //private double mРизмПолнЗаКомпр2;
        //public double РизмПолнЗаКомпр2
        //{
        //    get { return mРизмПолнЗаКомпр2; }
        //    set
        //    {
        //        mРизмПолнЗаКомпр2 = value;
        //        InputArgDictionary[conРизмПолнЗаКомпр2] = value;
        //    }
        //}

        public Dictionary<string, double> InputArgDictionary { get; set; }

        public InputParameter()
        {
            InputArgDictionary = new Dictionary<string, double>
            {
                //{ conБарометр, Барометр },
                //{ conРстатРМК1, РстатРМК1 },
                //{ conРстатРМК2, РстатРМК2 },
                //{ conРполнРМК1, РполнРМК1 },
                //{ conРполнРМК2, РполнРМК2 },
                //{ conРизмНаВходе1, РизмНаВходе1 },
                //{ conРизмНаВходе2, РизмНаВходе2 },
                //{ conРизмПолнЗаКомпр1, РизмПолнЗаКомпр1 },
                //{ conРизмПолнЗаКомпр2, РизмПолнЗаКомпр2 }
            };
            //InputArgDictionary.Add(conARG10, ARG10)
        }
    }
}
