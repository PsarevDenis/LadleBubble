using System.Windows;
using DevExpress.Mvvm.DataAnnotations;

namespace LadleBubble
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DevExpress.Xpf.Core.DXGridDataController.DisableThreadingProblemsDetection = true;
        }
    }
}
