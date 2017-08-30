using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Xpf.Core;
using LadleBubble.MVVM.Model;
using MyToolkit.Command;
using MyToolkit.Mvvm;

namespace LadleBubble.MVVM.ViewModel
{
    public class BubbleViewModel : ViewModelBase
    {
        private double _lshl;
        private double _hkSp;
        private double _processTime;
        private double _ugol;
        private ObservableCollection<Point<double>> _point;

        private Bubble _initialSettings;
        private ObservableCollection<ResultSet> _oneBubbleResultSets;
        private ObservableCollection<ResultSet> _manyBubbleResultSets;
        private ObservableCollection<Koltco> _listKoltco;

        private ObservableCollection<Parametrs> _listParametr;
        private Parametrs _parametrX;
        private Parametrs _parametrY;

        public ObservableCollection<Parametrs> ListParametr
        {
            get { return _listParametr; }
            set { Set(ref _listParametr, value); }
        }

        public Parametrs ParametrX
        {
            get { return _parametrX; }
            set { Set(ref _parametrX, value); }
        }

        public Parametrs ParametrY
        {
            get { return _parametrY; }
            set { Set(ref _parametrY, value); }
        }

        public ObservableCollection<Point<double>> Point
        {
            get { return _point; }
            set { Set(ref _point, value); }
        }

        public Bubble InitialSettings
        {
            get { return _initialSettings; }
            set { Set(ref _initialSettings, value); }
        }

        public ObservableCollection<ResultSet> OneBubbleResultSets
        {
            get { return _oneBubbleResultSets; }
            set { Set(ref _oneBubbleResultSets, value); }
        }

        public ObservableCollection<ResultSet> ManyBubbleResultSets
        {
            get { return _manyBubbleResultSets; }
            set { Set(ref _manyBubbleResultSets, value); }
        }

        public ObservableCollection<Koltco> ListKoltco
        {
            get { return _listKoltco; }
            set { Set(ref _listKoltco, value); }
        }

        public double Lshl
        {
            get { return _lshl; }
            set { Set(ref _lshl, value); }
        }

        public double hkSp
        {
            get { return _hkSp; }
            set { Set(ref _hkSp, value); }
        }

        public double processTime
        {
            get { return _processTime; }
            set { Set(ref _processTime, value); }
        }

        public double Ugol
        {
            get { return _ugol; }
            set { Set(ref _ugol, value); }
        }

        public bool FirstStep { get; set; }
        public bool IsWork { get; set; }

        public const double G = 9.18f;

        #region Один пузырь

        public double GetWebber()
        {
            return InitialSettings.Σ / (Math.Pow(InitialSettings.R * 2, 2) * InitialSettings.Pm * G);
        }

        public double GetBeginRadiusStr()
        {
            return ((1.82 -
                    200 * Math.Pow(InitialSettings.Pg / (InitialSettings.Pm - InitialSettings.Pg), 0.96) *
                    Math.Pow(GetWebber(), 0.36)) * InitialSettings.R * 2) / 2;
        }

        public double GetMassBubble(double nachalnaiRadius)
        {
            return 4 * Math.PI * Math.Pow(nachalnaiRadius, 3) * InitialSettings.Pg * (InitialSettings.Pa + InitialSettings.Hk * InitialSettings.Pm * G)
                / (3 * InitialSettings.Pa);
        }

        public double GetReNumber(double nachalnaiSkorost, double nachalnaiRadius)
        {
            return nachalnaiSkorost * 2 * nachalnaiRadius / InitialSettings.Y;
        }

        public double GetKoefRe(double reNumber)
        {
            if (Math.Abs(reNumber) == 0)
            {
                return 0.2f;
            }
            if (reNumber > 0 && reNumber < 2)
            {
                return 24 / reNumber;
            }
            if (reNumber > 2 && reNumber < 500)
            {
                return (double) (18.5 / Math.Pow(reNumber, 0.6));
            }
            return 0.44f;
        }

        public double GetGz()
        {
            return (G * (InitialSettings.Pm - InitialSettings.Pg)) / InitialSettings.Pg;
        }

        public double GetRz(double kf, double beginRadius)
        {
            return (kf * Math.PI * Math.Pow(beginRadius, 2) * InitialSettings.Pm) / 2;
        }

        public double GetK(double gz, double massa, double rz)
        {
            return Math.Sqrt(gz * massa / rz);
        }
        public double GetK1(double gz, double massa, double rz)
        {
            return 2 * (rz / massa) * Math.Sqrt(gz * rz / massa);
        }

        public double GetOurSpeed(double k, double k1, double nachalnaiSkorost)
        {
            return k * ((1 - (k - nachalnaiSkorost) / (k + nachalnaiSkorost) * Math.Exp(-k1 * InitialSettings.Dt))
                / (1 + (k - nachalnaiSkorost) / (k + nachalnaiSkorost) * Math.Exp(-k1 * InitialSettings.Dt)));
        }

        public double GetR2(double obem)
        {
            return Math.Pow(3 * obem / Math.PI * 4, 1.0 / 3);
        }

        public double GetV2(double davlenie, double nachalnaiDavlenie, double nachalniaObem)
        {
            return nachalniaObem * nachalnaiDavlenie / davlenie;
        }

        public double GetP1()
        {
            return InitialSettings.Pa + InitialSettings.Pm * InitialSettings.Hk;
        }

        public double GetV1(double nachalnaiRadius)
        {
            return (4 * Math.PI * Math.Pow(nachalnaiRadius, 3)) / 3;
        }

        public double GetP2(double put, double nachalnaiDavlenie)
        {
            return InitialSettings.Pa + InitialSettings.Pm * (InitialSettings.Hk - put);
        }

        public double GetS2(double nachalnaiPut,double nachalnaiSkorost)
        {
            return nachalnaiPut + nachalnaiSkorost * InitialSettings.Dt;
        }

        public void OneBubbleScave()
        {
            double time = 0;

            double beginRadius = GetBeginRadiusStr();
            double beginObem = GetV1(beginRadius);
            double massa = GetMassBubble(beginRadius);
            double beginDavlenie = GetP1();
            double beginSkorost = 0;

            double beginPut = 0;

            while (beginPut < 2)
            {

                var chisloReinoldsa = GetReNumber(beginSkorost, beginRadius);
                var kf = GetKoefRe(chisloReinoldsa);
                var gz = GetGz();
                var rz = GetRz(kf, beginRadius);
                var k = GetK(gz, massa, rz);
                var k1 = GetK1(gz, massa, rz);
                var skorost = GetOurSpeed(k, k1, beginSkorost);
                var put = GetS2(beginPut, skorost);
                var davlenie = GetP2(put, beginDavlenie);
                var obem = GetV2(davlenie, beginDavlenie, beginObem);
                var radius = GetR2(obem);

                OneBubbleResultSets.Add(new ResultSet()
                {
                    KRe = chisloReinoldsa,
                    Kf = kf,
                    GStar = gz,
                    RStar = rz,
                    K = k,
                    K1 = k1,
                    W = skorost,
                    S = beginPut,
                    PaBubble = beginDavlenie,
                    V = beginObem,
                    R = beginRadius,
                    T = time,
                    MBubble = massa
                });
                
                beginSkorost = skorost;
                beginRadius = radius;
                beginPut = put;
                beginDavlenie = davlenie;
                beginObem = obem;

                time = time + InitialSettings.Dt;
            }
        }

        #endregion

        #region Много пузырей

        public double GetRandomE()
        {
            Random rand = new Random();
            return Convert.ToDouble(rand.Next(100)) / 100;
        }

        public double GetSkorostU(double a, double e, double wi)
        {
            return a * Math.Cos(2 * Math.PI * e) * wi;
        }

        public double GetSkorostV(double a, double e, double wi)
        {
            return a * Math.Sin(2 * Math.PI * e) * wi;
        }

        public double GetNewCoordinatX(double x, double uiPlus1)
        {
            return x + InitialSettings.Dt * uiPlus1;
        }

        public double GetNewCoordinatY(double y, double viPlus1)
        {
            return y + InitialSettings.Dt * viPlus1;
        }

        public double GetNewCoordinatZ(double z, double wiPlus1)
        {
            return z + InitialSettings.Dt * wiPlus1;
        }

        public double GetCountBubble(double V0)
        {
            return (InitialSettings.Intensiti * InitialSettings.Dt) / V0;
        }

        public double GetPi(double z)
        {
            return InitialSettings.Pa + InitialSettings.Pm * G * G * (InitialSettings.Hk - z);
        }

        public double GetPi1(double zi)
        {
            return InitialSettings.Pa + InitialSettings.Pm * G * G * (InitialSettings.Hk - zi);
        }

        public double GetRiPlus1(double r0, double pi, double piPlus1)
        {
            return r0 * Math.Pow((pi / piPlus1), 1.0 / 3);
        }

        public void ManyBubbleScave()
        {
            double time = 0;

            double x = 0;
            double y = 0;
            double z = 0;

            double beginRadius = GetBeginRadiusStr();
            double beginObem = GetV1(beginRadius);
            double r0 = GetR2(beginObem);
            double massa = GetMassBubble(beginRadius);
            double beginDavlenie = GetPi(z);
            double beginSkorost = 0;
            
            while (InitialSettings.Hk > z)
            {
                var chisloReinoldsa = GetReNumber(beginSkorost, beginRadius);
                var kf = GetKoefRe(chisloReinoldsa);
                var gz = GetGz();
                var rz = GetRz(kf, beginRadius);
                var k = GetK(gz, massa, rz);
                var k1 = GetK1(gz, massa, rz);
                var skorost = GetOurSpeed(k, k1, beginSkorost);
                var e = GetRandomE();
                var u = GetSkorostU(0.125, e, skorost);
                var v = GetSkorostV(0.125, e, skorost);

                ManyBubbleResultSets.Add(new ResultSet()
                {
                    KRe = chisloReinoldsa,
                    Kf = kf,
                    GStar = gz,
                    RStar = rz,
                    K = k,
                    K1 = k1,
                    W = skorost,
                    PaBubble = beginDavlenie,
                    V = beginObem,
                    R = r0,
                    T = time,
                    X = x,
                    Y = y,
                    Z = z,
                    MBubble = massa,
                    U = u,
                    Vs = v,
                    E = e
                });

                var xi = GetNewCoordinatX(x, u);
                var yi = GetNewCoordinatY(y, v);
                var zi = GetNewCoordinatZ(z, skorost);

                var pi = GetPi1(zi);
                var ri = GetRiPlus1(r0, beginDavlenie, pi);

                beginSkorost = skorost;
                beginDavlenie = pi;
                r0 = ri;

                x = xi;
                y = yi;
                z = zi;

                time = time + InitialSettings.Dt;
            }
        }

        #endregion

        #region Подъем металла

        public void GetKoltca()
        {
            FirstStep = true;
            double delenieGrizont = InitialSettings.Hk / InitialSettings.HorizontCount;
            double delenieKoletc = InitialSettings.Rk / InitialSettings.RingCount;

            double coordinatZFrom = 0;

            double vm0 = Math.PI * Math.Pow(InitialSettings.R, 2) * InitialSettings.Hk;

            for (int i = 0; i < InitialSettings.HorizontCount; i++)
            {
                var coordinatZTo = coordinatZFrom + delenieGrizont;
                double coordinatXFrom = 0;
                for (int j = 0; j < InitialSettings.RingCount; j++)
                {
                    var coordinatXTo = coordinatXFrom + delenieKoletc;
                    ListKoltco.Add(new Koltco(coordinatXFrom, coordinatXTo, coordinatZFrom, coordinatZTo, vm0, i, j));
                    coordinatXFrom = coordinatXTo;
                }
                coordinatZFrom = coordinatZTo;
            }
        }

        public double GetWKoltca(Koltco koltco)
        {
            double wKoltca = 0;

            foreach (var bubble in koltco.ListBubble)
            {
                double ch = 2 * koltco.Fc * (InitialSettings.Hk / InitialSettings.HorizontCount) / (InitialSettings.Pm * koltco.Vm);
                wKoltca = wKoltca + ch;
            }

            return Math.Sqrt(wKoltca);
        }

        public double GethKKoltca(Koltco koltco)
        {
            double hkoltca = 0;

            foreach (var bubble in koltco.ListBubble)
            {
                double ch = 2 * koltco.Fc / koltco.Vm;
                hkoltca = hkoltca + ch;
            }

            return hkoltca * (InitialSettings.Hk / InitialSettings.HorizontCount) / (G * InitialSettings.Pm);
        }

        public double GetUgolRascr(ObservableCollection<Koltco> listKoltco)
        {
            int indexKol = 0;

            List<double> listCoordinatX = new List<double>();

            foreach (var item in listKoltco.Where(x => x.Index_gor == InitialSettings.HorizontCount - 1))
            {
                if (item.Vg > 0)
                {
                    indexKol = item.Index_kol;
                }
            }

            if (indexKol > 0)
            {
                foreach (var item in listKoltco.Single(x => x.Index_gor == InitialSettings.HorizontCount - 1 && x.Index_kol == indexKol).ListBubble)
                {
                    listCoordinatX.Add(Math.Abs(item.X));
                }

                return Math.Atan2(listCoordinatX.Max(), InitialSettings.Hk) * (180 / Math.PI);
            }
            return 0;
        }

        public void GetBurun(ObservableCollection<Koltco> listKoltco)
        {
            double sum_l = 0, diam = 0, v_obsh = 0;
            foreach (var item in listKoltco.Where(x => x.Index_gor == InitialSettings.HorizontCount - 1))
            {
                if (item.Vg > 0)
                {
                    sum_l = sum_l + item.HK * Math.PI * (Math.Pow(item.CoordinatXTo, 2) - Math.Pow(diam, 2));
                    diam = item.CoordinatXTo;
                }
            }

            _hkSp = listKoltco.Single(x => x.Index_gor == InitialSettings.HorizontCount - 1 && x.Index_kol == 0).HK;
            var ind = Math.Round(2 * InitialSettings.RingCount * diam / InitialSettings.R) - 1;
            v_obsh = (InitialSettings.Hs * Math.PI * Math.Pow(InitialSettings.R, 2)) / 4 + sum_l;
            var newHk = 4 * (InitialSettings.Hk * Math.PI * Math.Pow(InitialSettings.R, 2) / 4 + 4 * sum_l / (Math.PI * Math.Pow(InitialSettings.R, 2))) / (Math.PI * Math.Pow(InitialSettings.R, 2));
            _lshl = (4 * v_obsh) / (Math.PI * Math.Pow(InitialSettings.R, 2));
        }

        public ResultSet GetResultBubble()
        {
            double x = 0;
            double y = 0;
            double z = 0;

            double beginRadius = GetBeginRadiusStr();
            double beginObem = GetV1(beginRadius);
            double r0 = GetR2(beginObem);
            double massa = GetMassBubble(r0);
            double beginDavlenie = GetPi(z);
            double beginSkorost = 0;
            
            var chisloReinoldsa = GetReNumber(beginSkorost, r0);
            var kf = GetKoefRe(chisloReinoldsa);
            var gz = GetGz();
            var rz = GetRz(kf, r0);
            var k = GetK(gz, massa, rz);
            var k1 = GetK1(gz, massa, rz);
            var skorost = GetOurSpeed(k, k1, beginSkorost);
            var e = GetRandomE();
            var u = GetSkorostU(0.1, e, skorost);
            var v = GetSkorostV(0.1, e, skorost);
            
            var xi = GetNewCoordinatX(x, u);
            var yi = GetNewCoordinatY(y, v);
            var zi = GetNewCoordinatZ(z, skorost);

            var pi = GetPi1(zi);
            var ri = GetRiPlus1(r0, beginDavlenie, pi);

            var result = new ResultSet()
            {
                KRe = chisloReinoldsa,
                Kf = kf,
                GStar = gz,
                RStar = rz,
                K = k,
                K1 = k1,
                W = skorost,
                PaBubble = pi,
                V = beginObem,
                R = ri,
                X = xi,
                Y = yi,
                Z = zi,
                MBubble = massa,
                U = u,
                Vs = v,
                E = e
            };

            return result;
        }

        public ResultSet GetNewBubbleResult(ResultSet rs)
        {
            double x = rs.X;
            double y = rs.Y;
            double z = rs.Z;

            double beginObem = rs.V;
            double r0 = rs.R;
            double massa = rs.MBubble;
            double beginDavlenie = rs.PaBubble;
            double beginSkorost = rs.W;

            var chisloReinoldsa = GetReNumber(beginSkorost, r0);
            var kf = GetKoefRe(chisloReinoldsa);
            var gz = GetGz();
            var rz = GetRz(kf, r0);
            var k = GetK(gz, massa, rz);
            var k1 = GetK1(gz, massa, rz);
            var skorost = GetOurSpeed(k, k1, beginSkorost);
            var e = GetRandomE();
            var u = GetSkorostU(0.1, e, skorost);
            var v = GetSkorostV(0.1, e, skorost);

            var xi = GetNewCoordinatX(x, u);
            var yi = GetNewCoordinatY(y, v);
            var zi = GetNewCoordinatZ(z, skorost);

            var pi = GetPi1(zi);
            var ri = GetRiPlus1(r0, beginDavlenie, pi);

            var result = new ResultSet()
            {
                KRe = chisloReinoldsa,
                Kf = kf,
                GStar = gz,
                RStar = rz,
                K = k,
                K1 = k1,
                W = skorost,
                PaBubble = pi,
                V = beginObem,
                R = ri,
                X = xi,
                Y = yi,
                Z = zi,
                MBubble = massa,
                U = u,
                Vs = v,
                E = e
            };

            return result;
        }

        public void MetalMovement()
        {
            processTime = 0;
            GetKoltca();
            var r0 = GetBeginRadiusStr();
            var v0 = GetV1(r0);
            var bubbleCount = GetCountBubble(v0);

            Koltco Koltco;
            ResultSet Bubble;
            
            while (IsWork)
            {
                if (FirstStep)
                {
                    for (int i = 0; i < bubbleCount; i++)
                    {
                        Bubble = GetResultBubble();

                        Koltco =
                            ListKoltco.Single(
                                x =>
                                    (x.CoordinatXFrom <= Math.Abs(Bubble.X) && Math.Abs(Bubble.X) <= x.CoordinatXTo) &&
                                    (x.CoordinatZFrom <= Bubble.Z && Bubble.Z <= x.CoordinatZTo));

                        Koltco.ListBubble.Add(Bubble);
                        Koltco.Vg = Koltco.Vg + Bubble.V;
                        Koltco.Vm = Koltco.Vm0 - Koltco.Vg;
                        Koltco.Fc = Koltco.Fc + Bubble.RStar * Bubble.W / G;
                    }

                    FirstStep = false;
                }
                else
                {
                    for (int i = 0; i < ListKoltco.Count; i++)
                    {
                        if (ListKoltco[i].ListBubble.Count != 0)
                        {
                            for (int j = 0; j < ListKoltco[i].ListBubble.Count; j++)
                            {
                                //TODO: восстановить прежнюю рабочую версию
                                Bubble = ListKoltco[i].ListBubble[j];
                                Bubble = GetNewBubbleResult(Bubble);

                                if (Bubble.Z < InitialSettings.Hk)
                                {
                                    ListKoltco[i].ListBubble.Remove(Bubble);
                                    ListKoltco[i].Vg = ListKoltco[i].Vg - Bubble.V;
                                    ListKoltco[i].Vm = ListKoltco[i].Vm0 - ListKoltco[i].Vg;
                                    ListKoltco[i].Fc = ListKoltco[i].Fc - Bubble.RStar * Bubble.W / G;


                                    Koltco =
                                        ListKoltco.First(
                                            x =>
                                                (x.CoordinatXFrom <= Math.Abs(Bubble.X) &&
                                                    Math.Abs(Bubble.X) <= x.CoordinatXTo) &&
                                                (x.CoordinatZFrom <= Bubble.Z && Bubble.Z <= x.CoordinatZTo));

                                    Koltco.ListBubble.Add(Bubble);
                                    Koltco.Vg = Koltco.Vg + Bubble.V;
                                    Koltco.Vm = Koltco.Vm0 - Koltco.Vg;
                                    Koltco.Fc = Koltco.Fc + Bubble.RStar * Bubble.W / G;
                                }
                                else
                                {
                                    ListKoltco[i].ListBubble.Remove(Bubble);
                                    ListKoltco[i].Vg = ListKoltco[i].Vg - Bubble.V;
                                    ListKoltco[i].Vm = ListKoltco[i].Vm0 - ListKoltco[i].Vg;
                                    ListKoltco[i].Fc = ListKoltco[i].Fc - Bubble.RStar * Bubble.W / G;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < bubbleCount; i++)
                    {
                        Bubble = GetResultBubble();

                        Koltco =
                            ListKoltco.Single(
                                x =>
                                    (x.CoordinatXFrom <= Math.Abs(Bubble.X) && Math.Abs(Bubble.X) <= x.CoordinatXTo) &&
                                    (x.CoordinatZFrom <= Bubble.Z && Bubble.Z <= x.CoordinatZTo));

                        Koltco.ListBubble.Add(Bubble);
                        Koltco.Vg = Koltco.Vg + Bubble.V;
                        Koltco.Vm = Koltco.Vm0 - Koltco.Vg;
                        Koltco.Fc = Koltco.Fc + Bubble.RStar * Bubble.W / G;
                    }
                }

                foreach (var item in ListKoltco)
                {
                    item.W = GetWKoltca(item);
                    item.HK = GethKKoltca(item);
                }

                _processTime = processTime + InitialSettings.Dt;
                _ugol = GetUgolRascr(ListKoltco);
                GetBurun(ListKoltco);
            }
                
        }

        public async Task MetalMovemenTask(
    CancellationToken token = new CancellationToken())
        {
            await Task.Delay(TimeSpan.FromSeconds(3), token).ConfigureAwait(false);
            MetalMovement();
        }

        public void Stop()
        {
            IsWork = false;
        }

        #endregion

        public void BuildGraph()
        {
            _point.Clear();

            var x = ParametrX.ParametrName;
            var y = ParametrY.ParametrName;

            foreach (var oneBubble in OneBubbleResultSets)
            {
                _point.Add(new Point<double>((double) oneBubble.GetType().GetProperty(y).GetValue(oneBubble), (double)oneBubble.GetType().GetProperty(x).GetValue(oneBubble)));
            }
        }

        public BubbleViewModel()
        {
            InitialSettings = new Bubble(7000,1.73,0.096,2,1.8,0.005,0.0000005,10000,0.0001,20,20,0.005,0.69);
            OneBubbleResultSets = new ObservableCollection<ResultSet>();
            ManyBubbleResultSets = new ObservableCollection<ResultSet>();
            ListKoltco = new ObservableCollection<Koltco>();
            Point = new ObservableCollection<Point<double>>();
            Point.Add(new Point<double>(0, 0));

            OneBubbleScaveCommand = new RelayCommand(OneBubbleScave);
            ManyBubbleScaveCommand = new RelayCommand(ManyBubbleScave);
            MetalMovementCommand = new AsyncRelayCommand(async () =>
            {
                await MetalMovemenTask();
            });
            StopCommand = new RelayCommand(Stop);
            BuildGraphCommand = new RelayCommand(BuildGraph);

            IsWork = true;
            FirstStep = true;
            
            _listParametr = new ObservableCollection<Parametrs>();
            _listParametr.Add(new Parametrs("T", "Время"));
            _listParametr.Add(new Parametrs("S", "Путь"));
            _listParametr.Add(new Parametrs("PaBubble", "Давление в пузырьке"));
            _listParametr.Add(new Parametrs("KRe", "Коэффициент Рейнольдса"));
            _listParametr.Add(new Parametrs("Kf", "Коэффициент сопртивления"));
            _listParametr.Add(new Parametrs("Vs", "Скорость V"));
            _listParametr.Add(new Parametrs("V", "Объем"));
            _listParametr.Add(new Parametrs("U", "Скорость U"));
            _listParametr.Add(new Parametrs("W", "Скорость"));
            _listParametr.Add(new Parametrs("R", "Радиус"));
            _listParametr.Add(new Parametrs("MBubble", "Масса пузырька"));
            _listParametr.Add(new Parametrs("RStar", "r*"));
            _listParametr.Add(new Parametrs("GStar", "g*"));
            _listParametr.Add(new Parametrs("K1", "K1"));
            _listParametr.Add(new Parametrs("K", "K"));
            _listParametr.Add(new Parametrs("E", "e"));
            _listParametr.Add(new Parametrs("X", "Координата x"));
            _listParametr.Add(new Parametrs("Y", "Координата y"));
            _listParametr.Add(new Parametrs("Z", "Координата z"));
            _listParametr.Add(new Parametrs("Fc", "Сила кольца"));
            _listParametr.Add(new Parametrs("Vc", "Объем кольца"));
            _listParametr.Add(new Parametrs("Wc", "Скорость кольца"));
            _listParametr.Add(new Parametrs("Hc", "Высота подъема кольца"));
            _listParametr.Add(new Parametrs("L", ""));
            _listParametr.Add(new Parametrs("HkSp", ""));
            _listParametr.Add(new Parametrs("Ind", ""));
            _listParametr.Add(new Parametrs("VObsh", "Общий объем"));
            _listParametr.Add(new Parametrs("NewHk", ""));
            _listParametr.Add(new Parametrs("LShl", ""));
            _listParametr.Add(new Parametrs("UgolRask", "Угол расктрытия"));
        }

        public CommandBase OneBubbleScaveCommand { get; private set; }
        public CommandBase ManyBubbleScaveCommand { get; private set; }
        public CommandBase MetalMovementCommand { get; private set; }
        public CommandBase StopCommand { get; private set; }
        public CommandBase BuildGraphCommand { get; private set; }

    }
}
