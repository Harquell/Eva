using EVA.Client.WPF.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EVA.Client.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow _instance;
        public static MainWindow Instance => _instance ?? (_instance = new MainWindow());

        private List<UserControl> _pages = new List<UserControl>();
        private UserControl _currentControl => _pages.LastOrDefault();

        public MainWindow()
        {
            InitializeComponent();
            SetPage(new LoginPage());
        }

        public void SetPage(UserControl page)
        {
            _pages.Add(page);
            Container.Children.Add(page);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(_pages.Count > 1)
            {
                _pages.Remove(_currentControl);

                Container.Children.Add(_currentControl);
            }
        }
    }
}
