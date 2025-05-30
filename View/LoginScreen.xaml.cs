using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using PolMedUMG.ViewModel;

namespace PolMedUMG.View
{
    /// <summary>
    /// Logika interakcji dla klasy LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            SessionManager.connStrSQL = "server=bb97fob4mmaybcvttjjk-mysql.services.clever-cloud.com;uid=uirqsom4re7q6gwn;pwd=ODh2O0u6eNj3uUkXsLYO;database=bb97fob4mmaybcvttjjk";
            //      nowy                "server=bb97fob4mmaybcvttjjk-mysql.services.clever-cloud.com;uid=uirqsom4re7q6gwn;pwd=ODh2O0u6eNj3uUkXsLYO;database=bb97fob4mmaybcvttjjk"
            //      stary               "server=bwpd1lnfwwmd8zooiosa-mysql.services.clever-cloud.com;uid=uf9nqf7gizjdvxmm;pwd=mV5lVFodqkbncFJJnxqQ;database=bwpd1lnfwwmd8zooiosa"

            InitializeComponent();
            DataContext = new LoginViewModel();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMinimize_Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_FullScreen(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized) WindowState = WindowState.Maximized;
            else WindowState = WindowState.Normal;
        }
        public void LoadContent(UserControl control)
        {
            if (Content != null)
            {
                Content.Children.Clear();
                Content.Children.Add(control);
            }
        }
    }
}
