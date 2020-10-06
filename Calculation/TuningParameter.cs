using System.Collections.Generic;

namespace BarometersBRS1M
{
    /// <summary>
    /// Настроечные параметры
    /// </summary>
    /// <remarks></remarks>
    public class TuningParameter
    {
        //public const string conA = "A";
        ////Public Property Tuning1 As Dual = New Dual ' With {.ЛогикаИлиЧисло = False, .ЦифровоеЗначение = 0, .ЛогическоеЗначение = False}
        //private Dual mA = new Dual();
        //public Dual A
        //{
        //    get { return mA; }
        //    set
        //    {
        //        mA = value;
        //        TuningDictionary[conA] = value;
        //    }
        //}

        //private const string conTuning2 = "Tuning2";
        //private Dual mTuning2 = new Dual();
        //public Dual Tuning2
        //{
        //    get { return mTuning2; }
        //    set
        //    {
        //        mTuning2 = value;
        //        TuningDictionary[conTuning2] = value;
        //    }
        //}

        //private const string conTuning3 = "Tuning3";
        //private Dual mTuning3 = new Dual();
        //public Dual Tuning3
        //{
        //    get { return mTuning3; }
        //    set
        //    {
        //        mTuning3 = value;
        //        TuningDictionary[conTuning3] = value;
        //    }
        //}

        //private const string conTuning4 = "Tuning4";
        //private Dual mTuning4 = new Dual();
        //public Dual Tuning4
        //{
        //    get { return mTuning4; }
        //    set
        //    {
        //        mTuning4 = value;
        //        TuningDictionary[conTuning4] = value;
        //    }
        //}

        //private const string conTuning5 = "Tuning5";
        //private Dual mTuning5 = new Dual();
        //public Dual Tuning5
        //{
        //    get { return mTuning5; }
        //    set
        //    {
        //        mTuning5 = value;
        //        TuningDictionary[conTuning5] = value;
        //    }
        //}

        //private const string conTuning6 = "Tuning6";
        //private Dual mTuning6 = new Dual();
        //public Dual Tuning6
        //{
        //    get { return mTuning6; }
        //    set
        //    {
        //        mTuning6 = value;
        //        TuningDictionary[conTuning6] = value;
        //    }
        //}

        //private const string conTuning7 = "Tuning7";
        //private Dual mTuning7 = new Dual();
        //public Dual Tuning7
        //{
        //    get { return mTuning7; }
        //    set
        //    {
        //        mTuning7 = value;
        //        TuningDictionary[conTuning7] = value;
        //    }
        //}


        //private const string conTuning8 = "Tuning8";
        //private Dual mTuning8 = new Dual();
        //public Dual Tuning8
        //{
        //    get { return mTuning8; }
        //    set
        //    {
        //        mTuning8 = value;
        //        TuningDictionary[conTuning8] = value;
        //    }
        //}


        //private const string conTuning9 = "Tuning9";
        //private Dual mTuning9 = new Dual();
        //public Dual Tuning9
        //{
        //    get { return mTuning9; }
        //    set
        //    {
        //        mTuning9 = value;
        //        TuningDictionary[conTuning9] = value;
        //    }
        //}

        //private const string conTuning10 = "Tuning10";
        //private Dual mTuning10 = new Dual();
        //public Dual Tuning10
        //{
        //    get { return mTuning10; }
        //    set
        //    {
        //        mTuning10 = value;
        //        TuningDictionary[conTuning10] = value;
        //    }
        //}

        public Dictionary<string, Dual> TuningDictionary { get; set; }

        public TuningParameter()
        {
            TuningDictionary = new Dictionary<string, Dual>
            {
                //{ conA, A },
                //{ conTuning2, Tuning2 },
                //{ conTuning3, Tuning3 },
                //{ conTuning4, Tuning4 },
                //{ conTuning5, Tuning5 },
                //{ conTuning6, Tuning6 },
                //{ conTuning7, Tuning7 },
                //{ conTuning8, Tuning8 },
                //{ conTuning9, Tuning9 },
                //{ conTuning10, Tuning10 }
            };
        }
    }

    public class Dual
    {
        public bool ЛогикаИлиЧисло { get; set; }
        public double ЦифровоеЗначение { get; set; }
        public bool ЛогическоеЗначение { get; set; }
    }
}
