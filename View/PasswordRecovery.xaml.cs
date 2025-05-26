using System.Windows;
using System.Windows.Controls;
using PolMedUMG.ViewModel;

namespace PolMedUMG.View
{
    public partial class PasswordRecovery : UserControl
    {
        public PasswordRecovery()
        {
            InitializeComponent();
        }
        private void go_Back(object sender, RoutedEventArgs e)
        {
            var Conv = new LoginPrompt();

            var parentWindow = Window.GetWindow(this) as LoginScreen;

            if (parentWindow != null)
            {
                parentWindow.LoadContent(Conv);
            }
        }
    }
}
