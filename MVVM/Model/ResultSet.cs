using MyToolkit.Mvvm;

namespace LadleBubble.MVVM.Model
{
    public class ResultSet : ViewModelBase
    {
        private double _t;
        private double _s;
        private double _paBubble;
        private double _kRe;
        private double _kf;
        private double _v;
        private double _vs;
        private double _u;
        private double _w;
        private double _r;
        private double _mBubble;
        private double _rStar;
        private double _gStar;
        private double _k1;
        private double _k;
        private double _e;
        private double _x;
        private double _y;
        private double _z;
        private double _Fc;
        private double _Vc;
        private double _Wc;
        private double _Hc;
        private double _l;
        private double _hkSp;
        private double _ind;
        private double _v_obsh;
        private double _newHk;
        private double _lShl;
        private double _ugolRask;

        public double T
        {
            get { return _t; }
            set { Set(ref _t, value); }
        }

        public double S
        {
            get { return _s; }
            set { Set(ref _s, value); }
        }

        public double PaBubble
        {
            get { return _paBubble; }
            set { Set(ref _paBubble, value); }
        }

        public double KRe
        {
            get { return _kRe; }
            set { Set(ref _kRe, value); }
        }

        public double Kf
        {
            get { return _kf; }
            set { Set(ref _kf, value); }
        }

        public double Vs
        {
            get { return _vs; }
            set { Set(ref _vs, value); }
        }

        public double V
        {
            get { return _v; }
            set { Set(ref _v, value); }
        }

        public double U
        {
            get { return _u; }
            set { Set(ref _u, value); }
        }

        public double W
        {
            get { return _w; }
            set { Set(ref _w, value); }
        }

        public double R
        {
            get { return _r; }
            set { Set(ref _r, value); }
        }

        public double MBubble
        {
            get { return _mBubble; }
            set { Set(ref _mBubble, value); }
        }

        public double RStar
        {
            get { return _rStar; }
            set { Set(ref _rStar, value); }
        }

        public double GStar
        {
            get { return _gStar; }
            set { Set(ref _gStar, value); }
        }

        public double K1
        {
            get { return _k1; }
            set { Set(ref _k1, value); }
        }

        public double K
        {
            get { return _k; }
            set { Set(ref _k, value); }
        }

        public double E
        {
            get { return _e; }
            set { Set(ref _e, value); }
        }

        public double X
        {
            get { return _x; }
            set { Set(ref _x, value); }
        }

        public double Y
        {
            get { return _y; }
            set { Set(ref _y, value); }
        }

        public double Z
        {
            get { return _z; }
            set { Set(ref _z, value); }
        }

        public double Fc
        {
            get { return _Fc; }
            set { Set(ref _Fc, value); }
        }

        public double Vc
        {
            get { return _Vc; }
            set { Set(ref _Vc, value); }
        }

        public double Wc
        {
            get { return _Wc; }
            set { Set(ref _Wc, value); }
        }

        public double Hc
        {
            get { return _Hc; }
            set { Set(ref _Hc, value); }
        }

        public double L
        {
            get { return _l; }
            set { Set(ref _l, value); }
        }

        public double HkSp
        {
            get { return _hkSp; }
            set { Set(ref _hkSp, value); }
        }

        public double Ind
        {
            get { return _ind; }
            set { Set(ref _ind, value); }
        }

        public double VObsh
        {
            get { return _v_obsh; }
            set { Set(ref _v_obsh, value); }
        }

        public double NewHk
        {
            get { return _newHk; }
            set { Set(ref _newHk, value); }
        }

        public double LShl
        {
            get { return _lShl; }
            set { Set(ref _lShl, value); }
        }

        public double UgolRask
        {
            get { return _ugolRask; }
            set { Set(ref _ugolRask, value); }
        }

        public ResultSet()
        {
            _t = 0;
            _s = 0;
            _paBubble = 0;
            _kRe = 0;
            _kf = 0;
            _v = 0;
            _u = 0;
            _w = 0;
            _r = 0;
            _mBubble = 0;
            _rStar = 0;
            _gStar = 0;
            _k1 = 0;
            _k = 0;
            _e = 0;
            _x = 0;
            _y = 0;
            _z = 0;
            _Fc = 0;
            _Vc = 0;
            _Wc = 0;
            _Hc = 0;
            _l = 0;
            _hkSp = 0;
            _ind = 0;
            _v_obsh = 0;
            _newHk = 0;
            _lShl = 0;
            _ugolRask = 0;
        }

        public ResultSet(double t, double s, double paBubble, double kRe, double kf, double v, double u, double w, double r, double mBubble, double rStar, double gStar, double k1, double k, double e, double x, double y, double z, double fc, double vc, double wc, double hc, double l)
        {
            _t = t;
            _s = s;
            _paBubble = paBubble;
            _kRe = kRe;
            _kf = kf;
            _v = v;
            _u = u;
            _w = w;
            _r = r;
            _mBubble = mBubble;
            _rStar = rStar;
            _gStar = gStar;
            _k1 = k1;
            _k = k;
            _e = e;
            _x = x;
            _y = y;
            _z = z;
            _Fc = fc;
            _Vc = vc;
            _Wc = wc;
            _Hc = hc;
            _l = l;
        }
    }
}
