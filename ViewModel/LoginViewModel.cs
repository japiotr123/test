using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PolMedUMG.Model;
using PolMedUMG.View;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using MySqlX.XDevAPI;
using System.Data.Common;
using System.Reflection.PortableExecutable;

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
            SessionManager.CurrentUsername = _username;
            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(SessionManager.connStrSQL);
                conn.Open();

                // Zapytanie do bazy o wybranego uzytkownika
                MySqlCommand query = new MySqlCommand();
                query.Connection = conn;
                query.CommandText = @"SELECT COUNT(*) FROM users WHERE uid = @uid;";
                query.Parameters.AddWithValue("@uid", _username);
                int userCount = (int)(long)query.ExecuteScalar();
                conn.Close();
                if (userCount > 0)
                {
                    // Istnieje uzytkownik z takimi danymi
                    try
                    {
                        conn.Open();
                        
                        // Sprawdzamy jakiego typu jest uzytkownik
                        MySqlCommand query2 = new MySqlCommand();
                        query2.Connection = conn;
                        query2.CommandText = @"SELECT acc_type FROM users WHERE uid = @uid;";
                        query2.Parameters.AddWithValue("@uid", _username);
                        String acctype = query2.ExecuteScalar().ToString();
                        SessionManager.accType = acctype;
                        
                        // pobieranie hasla
                        MySqlCommand passwd = new MySqlCommand();
                        passwd.Connection = conn;
                        passwd.CommandText = @"SELECT pwd FROM users WHERE uid = @uid;";
                        passwd.Parameters.AddWithValue("@uid", _username);
                        String password = passwd.ExecuteScalar().ToString();
                        
                        //pobieranie recovery hasla, o ile istnieje
                        MySqlCommand recpass = new MySqlCommand();
                        recpass.Connection = conn;
                        recpass.CommandText = @"SELECT newPass, dateOfGeneration FROM PassRecovery WHERE username = @username ORDER BY dateOfGeneration DESC LIMIT 1;";
                        recpass.Parameters.AddWithValue("@username", _username);
                        string recoveryPassword = null;
                        DateTime dateOfGeneration = DateTime.MinValue;

                        using (var reader = recpass.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                recoveryPassword = reader["newPass"].ToString();
                                dateOfGeneration = Convert.ToDateTime(reader["dateOfGeneration"]);
                                
                            }
                            else
                            {
                                recoveryPassword = null;
                                dateOfGeneration = DateTime.MinValue;
                            }
                        }
                        TimeSpan timeSinceGeneration = DateTime.Now - dateOfGeneration;

                        if (password == _password || (_password == recoveryPassword && timeSinceGeneration.TotalMinutes <= 15)) 
                        { // dodac stosowna informacje ze haslo wygaslo, usunac z bazy niepotrzebne hasla
                            MySqlCommand updateLoginTime = new MySqlCommand();
                            updateLoginTime.Connection = conn;
                            updateLoginTime.CommandText = @"UPDATE users SET last_login = @loginTime WHERE uid = @uid;";
                            updateLoginTime.Parameters.AddWithValue("@loginTime", DateTime.Now);
                            updateLoginTime.Parameters.AddWithValue("@uid", _username);
                            updateLoginTime.ExecuteNonQuery();

                            
                            // W zaleznosci od typu uzytkownika otwieramy odpowiednie okno
                            if (acctype.Equals("2"))
                            {
                                MessageBox.Show("Zalogowano jako admin");
                            }
                            else if (acctype.Equals("1"))
                            {
                                DoctorScreen doctorWindow = new DoctorScreen();
                                doctorWindow.Show();
                                Application.Current.MainWindow.Close();
                            }
                            else
                            {
                                PatientScreen patientWindow = new PatientScreen();
                                patientWindow.Show();
                                Application.Current.MainWindow.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cos poszlo nie tak");
                        }
                            conn.Close();
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Niepoprawny login lub haslo");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}