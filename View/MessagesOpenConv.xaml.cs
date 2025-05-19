using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using MySql.Data.MySqlClient;


namespace PolMedUMG.View
{
    public partial class MessagesOpenConv : UserControl
    {
        public List<ConvMessages> Messages { get; set; }
        public DateTime date { get;  }
        public string doctorName { get; }
        public string doctorImage { get; }
        public ConvMessages conversation { get; set; }
        public MessagesOpenConv(DateTime date, string doctorName, string doctorImage, ConvMessages conversation)
        {

            InitializeComponent();

            var repo = new MessageRepository();

            Messages = repo.GetMessagesFrom(doctorName, SessionManager.CurrentUsername);


            this.date = date;
            this.doctorName = doctorName;
            this.doctorImage = doctorImage;
            this.conversation = conversation;

            this.DataContext = this;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (MainArea != null)
            {
                MainArea.Children.Clear();
                MainArea.Children.Add(new Messages());
            }
        }
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string messageText = MessageInput.Text;
            string senderr = SessionManager.CurrentUsername;
            string receiver = doctorName;
            DateTime data = DateTime.Now;
            string dataAsString = data.ToString();

            if (!string.IsNullOrWhiteSpace(messageText))
            {
                var newMsg = new ConvMessages(senderr, receiver, DateTime.Now, messageText,"nowa wiadomość", "dummy");

                Messages.Add(newMsg);

                MessagesList.ItemsSource = null;
                MessagesList.ItemsSource = Messages;


                using (MySqlConnection conn = new MySqlConnection(SessionManager.connStrSQL))
                {
                    try
                    {
                        conn.Open();

                        string sql = @"INSERT INTO Conversations (sender, receiver, date, content, status, doctorImage) 
                           VALUES (@sender, @receiver, @date, @content, @status, @doctorImage);";

                        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@sender", senderr);
                            cmd.Parameters.AddWithValue("@receiver", receiver);
                            cmd.Parameters.AddWithValue("@date", dataAsString);
                            cmd.Parameters.AddWithValue("@content", messageText);
                            cmd.Parameters.AddWithValue("@status", "nowa wiadomość");
                            cmd.Parameters.AddWithValue("@doctorImage", "dummy");

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd podczas dodawania wiadomości: " + ex.Message);
                    }
                }
                MessageInput.Text = "";
            }
            else
            {
                MessageBox.Show("Wiadomość jest pusta!");
            }
        }
        public static bool compare(object value)
        {
            string sender = value as string;
            string user = SessionManager.CurrentUsername;


            bool areEqual = sender != null && user != null && sender == user;
            bool equalsMethod = sender != null && user != null && string.Equals(sender, user, StringComparison.Ordinal);

            bool IsPatient = equalsMethod;

            return IsPatient;
        }
    }
    public class BoolToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsPatient = MessagesOpenConv.compare(value);

            return IsPatient ? Brushes.LightGray : (Brush)new BrushConverter().ConvertFromString("#5C84E2");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BoolToAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsPatient = MessagesOpenConv.compare(value);

            return IsPatient ? HorizontalAlignment.Right : HorizontalAlignment.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BoolToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsPatient = MessagesOpenConv.compare(value);

            return IsPatient ? Brushes.Black : Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
