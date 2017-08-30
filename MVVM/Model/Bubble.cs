using System.Collections.ObjectModel;
using MyToolkit.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace LadleBubble.MVVM.Model
{
    public class Bubble : ViewModelBase
    {
        const string InitialSettings = "Начальные настройки";

        private double _pm;
        private double _pg;
        private double _σ;
        private double _hk;
        private double _rk;
        private double _r;
        private double _y;
        private double _pa;
        private double _dt;
        private int _ringCount;
        private int _horizontCount;
        private double _hs;
        private double _intensiti;

        [Display(Name = "Удельная плотность металла", GroupName = InitialSettings)]
        public double Pm
        {
            get { return _pm; }
            set { Set(ref _pm, value); }
        }

        [Display(Name = "Удельная плотность газа", GroupName = LadleBubble.MVVM.Model.Bubble.InitialSettings)]
        public double Pg
        {
            get { return _pg; }
            set { Set(ref _pg, value); }
        }

        [Display(Name = "Поверхностное натяжение металла", GroupName = InitialSettings)]
        public double Σ
        {
            get { return _σ; }
            set { Set(ref _σ, value); }
        }

        [Display(Name = "Высота ковша", GroupName = InitialSettings)]
        public double Hk
        {
            get { return _hk; }
            set { Set(ref _hk, value); }
        }

        [Display(Name = "Радиус ковша", GroupName = InitialSettings)]
        public double Rk
        {
            get { return _rk; }
            set { Set(ref _rk, value); }
        }

        [Display(Name = "Радиус капиляра", GroupName = InitialSettings)]
        public double R
        {
            get { return _r; }
            set { Set(ref _r, value); }
        }

        [Display(Name = "Кинематическая вязкость жидкости", GroupName = InitialSettings)]
        public double Y
        {
            get { return _y; }
            set { Set(ref _y, value); }
        }

        [Display(Name = "Атмосферное давление", GroupName = InitialSettings)]
        public double Pa
        {
            get { return _pa; }
            set { Set(ref _pa, value); }
        }

        [Display(Name = "Интервал времени", GroupName = InitialSettings)]
        public double Dt
        {
            get { return _dt; }
            set { Set(ref _dt, value); }
        }

        [Display(Name = "Количество колец", GroupName = InitialSettings)]
        public int RingCount
        {
            get { return _ringCount; }
            set { Set(ref _ringCount, value); }
        }

        [Display(Name = "Количество горизонтов", GroupName = InitialSettings)]
        public int HorizontCount
        {
            get { return _horizontCount; }
            set { Set(ref _horizontCount, value); }
        }

        [Display(Name = "Высота шлака", GroupName = InitialSettings)]
        public double Hs
        {
            get { return _hs; }
            set { Set(ref _hs, value); }
        }

        [Display(Name = "Интенсивность", GroupName = InitialSettings)]
        public double Intensiti
        {
            get { return _intensiti; }
            set { Set(ref _intensiti, value); }
        }

        public Bubble(double pm, double pg, double σ, double hk,double rk, double r, double y, double pa, double dt, int ringCount, int horizontCount, double hs, double intensiti)
        {
            _pm = pm;
            _pg = pg;
            _σ = σ;
            _hk = hk;
            _rk = rk;
            _r = r;
            _y = y;
            _pa = pa;
            _dt = dt;
            _ringCount = ringCount;
            _horizontCount = horizontCount;
            _hs = hs;
            _intensiti = intensiti;
        }

        public Bubble()
        {
        }
    }
}
