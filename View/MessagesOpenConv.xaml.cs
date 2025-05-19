using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;


namespace PolMedUMG.View
{
    public partial class MessagesOpenConv : UserControl
    {
        public List<ConvMessages> Messages { get; set; }
        public string date { get;  }
        public string doctorName { get; }
        public string doctorImage { get; }
        public Conversation conversation { get; set; }
        public MessagesOpenConv(string date, string doctorName, string doctorImage, Conversation conversation)
        {

            InitializeComponent();

            var repo = new MessageRepository();
            Debug.WriteLine($"Liczba wiadomości: {SessionManager.CurrentUsername}");
            Messages = repo.GetMessagesFrom(doctorName, SessionManager.CurrentUsername);

            Debug.WriteLine($"Liczba wiadomości: {Messages.Count}");
            foreach (var msg in Messages)
            {
                Debug.WriteLine($"From: {msg.Sender} To: {msg.Receiver} Content: {msg.Content}");
            }

            this.date = date;
            this.doctorName = doctorName;
            this.doctorImage = doctorImage;
            this.conversation = conversation;

            this.DataContext = this;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //poki co crashuje trzeba tu dodac kod!
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
