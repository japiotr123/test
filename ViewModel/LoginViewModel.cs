using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PolMedUMG.Model;
using PolMedUMG.View;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

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
            // CONNECTION STRING
            string dbPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "database.mdf");
            string connectionString = $@"Server=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True;";

            using (var conn = new SqlConnection(connectionString)) {
                conn.Open();
                Console.WriteLine("Connection to the database was successful.");


                string loginQuery = "SELECT * FROM users WHERE username=@username AND password=@password;";

                using (SqlCommand cmd = new SqlCommand(loginQuery, conn)) {
                    if(_username.IsNullOrEmpty() || _password.IsNullOrEmpty())
                    {
                        MessageBox.Show("Nie podano danych!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                     
                    cmd.Parameters.AddWithValue("@username", _username);
                    cmd.Parameters.AddWithValue("@password", _password);
                    int numOfRows = 0;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            numOfRows++;
                            string accType = reader["accType"].ToString();

                            if (accType == "doktor")
                            {
                                DoctorScreen doctorWindow = new DoctorScreen();
                                doctorWindow.Show();
                                Application.Current.MainWindow.Close();
                            }

                            if (accType == "pacjent")
                            {
                                PatientScreen patientWindow = new PatientScreen();
                                patientWindow.Show();
                                Application.Current.MainWindow.Close();
                            }

                        }

                        if(numOfRows == 0)
                        {
                            MessageBox.Show("B³êdny login lub has³o.", "B³¹d", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            
            
            
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}