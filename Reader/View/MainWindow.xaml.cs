using System.Windows;

namespace Reader
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel(); // Устанавливаем ViewModel как контекст данных

        }
    }
}



