using System;
using MyToolkit.Mvvm;

namespace LadleBubble.MVVM.Model
{
    public class Parametrs : ViewModelBase
    {
        private string _parametrName;
        private string _parametrNameRus;

        public string ParametrName
        {
            get { return _parametrName; }
            set { Set(ref _parametrName, value); }
        }

        public string ParametrNameRus
        {
            get { return _parametrNameRus; }
            set { Set(ref _parametrNameRus, value); }
        }

        public Parametrs()
        {
        }

        public Parametrs(string parametrName, string parametrNameRus)
        {
            if (parametrName == null) throw new ArgumentNullException(nameof(parametrName));
            if (parametrNameRus == null) throw new ArgumentNullException(nameof(parametrNameRus));
            _parametrName = parametrName;
            _parametrNameRus = parametrNameRus;
        }
    }
}
