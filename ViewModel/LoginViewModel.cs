using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PolMedUMG.Model;
using PolMedUMG.View;

namespace PolMedUMG.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            var correctPatient = new UserModel
            {
                Username = "patient",
                Password = "patient"
            };
            var correctDoctor = new UserModel
            {
                Username = "doctor",
                Password = "doctor"
            };

            if (Username == correctPatient.Username && Password == correctPatient.Password )
            {
                MessageBox.Show("Zalogowano pomyœlnie do strony pacjenta!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                PatientScreen patientWindow = new PatientScreen();
                patientWindow.Show();
                Application.Current.MainWindow.Close();
                
            }
            else if (Username == correctDoctor.Username && Password == correctDoctor.Password)
            {
                MessageBox.Show("Zalogowano pomyœlnie do strony doktora!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                DoctorScreen doctorWindow = new DoctorScreen();
                doctorWindow.Show();
                Application.Current.MainWindow.Close();

            }
            else
            {
                MessageBox.Show("B³êdny login lub has³o.", "B³¹d", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}