using System.Collections.Generic;
using MyToolkit.Mvvm;

namespace LadleBubble.MVVM.Model
{
    public class Koltco : ViewModelBase
    {
        private double _coordinatZFrom;
        private double _coordinatZTo;
        private double _coordinatXFrom;
        private double _coordinatXTo;
        private List<ResultSet> _listBubble;
        private double _W;
        private double _Fc;
        private double _Vg;
        private double _Vm0;
        private double _Vm;
        private int _i;
        private int _j;
        private double _h_k;

        public double CoordinatZFrom
        {
            get { return _coordinatZFrom; }
            set { Set(ref _coordinatZFrom, value); }
        }

        public double CoordinatZTo
        {
            get { return _coordinatZTo; }
            set { Set(ref _coordinatZTo, value); }
        }

        public double CoordinatXFrom
        {
            get { return _coordinatXFrom; }
            set { Set(ref _coordinatXFrom, value); }
        }

        public double CoordinatXTo
        {
            get { return _coordinatXTo; }
            set { Set(ref _coordinatXTo, value); }
        }

        public List<ResultSet> ListBubble
        {
            get { return _listBubble; }
            set { Set(ref _listBubble, value); }
        }

        public double W
        {
            get { return _W; }
            set { Set(ref _W, value); }
        }

        public double Fc
        {
            get { return _Fc; }
            set { Set(ref _Fc, value); }
        }

        public double Vg
        {
            get { return _Vg; }
            set { Set(ref _Vg, value); }
        }

        public double Vm0
        {
            get { return _Vm0; }
            set { Set(ref _Vm0, value); }
        }

        public double Vm
        {
            get { return _Vm; }
            set { Set(ref _Vm, value); }
        }

        public int Index_gor
        {
            get { return _i; }
            set { Set(ref _i, value); }
        }

        public int Index_kol
        {
            get { return _j; }
            set { Set(ref _j, value); }
        }

        public double HK
        {
            get { return _h_k; }
            set { _h_k = value; }
        }

        public Koltco(double coordinatXFrom, double coordinatXTo, double coordinatZFrom, double coordinatZTo, double vm0,int i, int j)
        {
            CoordinatXFrom = coordinatXFrom;
            CoordinatXTo = coordinatXTo;
            CoordinatZFrom = coordinatZFrom;
            CoordinatZTo = coordinatZTo;

            Vg = 0;
            Fc = 0;
            W = 0;

            Vm0 = vm0;
            Vm = 0;

            Index_kol = j;
            Index_gor = i;

            ListBubble = new List<ResultSet>();
        }
    }
}
