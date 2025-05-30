using System.Windows;
using System.Windows.Controls;
using PolMedUMG.ViewModel;

namespace PolMedUMG.View
{
    public partial class PasswordRecoverySuccess : UserControl
    {
        private string _email;

        public PasswordRecoverySuccess()
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
        private void resend_password(object sender, RoutedEventArgs e)
        {
            //tu pewnie będzie odwołanie do metody która wysyła hasło
        }
    }
}
