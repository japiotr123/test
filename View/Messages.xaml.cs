using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using FontAwesome.WPF;
using System.Windows.Data;

namespace PolMedUMG.View
{
    public class Conversation
    {
        public string DoctorName { get; set; }
        public string Date { get; set; }
        public string StatusText { get; set; }
        public string DoctorImage { get; set; }
    }

    public partial class Messages : UserControl
    {
        public ObservableCollection<Conversation> Conversations { get; set; }

        private List<Conversation> AllConversations;
        private int currentPage = 1;
        private int pageSize = 6;
        private int totalPages => (int)Math.Ceiling((double)AllConversations.Count / pageSize);

        public Messages()
        {
            InitializeComponent();

            this.DataContext = this; 

            AllConversations = new List<Conversation>
            {
                new Conversation{DoctorName = "dr. Anna Maria Wesołowska",Date = "19.04.2024",StatusText = "nowa wiadomość",DoctorImage = "dummy.png"},
                new Conversation{DoctorName = "dr. Witt",Date = "15.04.2024",StatusText = "odczytane",DoctorImage = "dummy.png"},
                new Conversation{DoctorName = "dr. Andrzej Pędrak", Date = "28.03.2024", StatusText = "odczytane", DoctorImage = "dummy.png"},
                new Conversation{DoctorName = "dr. Smutas Kutas", Date = "10.02.2024", StatusText = "nowa wiadomość", DoctorImage = "dummy.png"},
                new Conversation{DoctorName = "dr. Marek Towarek", Date = "12.01.2024", StatusText = "odczytane", DoctorImage = "dummy.png"},
                new Conversation{DoctorName = "dr. Tomasz Kowalski", Date = "07.11.2024", StatusText = "odczytane", DoctorImage = "dummy.png"},
                new Conversation{DoctorName = "dr. Eryk Fryderyk", Date = "02.11.2024", StatusText = "odczytane", DoctorImage = "dummy.png"},
                new Conversation{DoctorName = "dr. Dobry Ziom", Date = "23.07.2023", StatusText = "odczytane", DoctorImage = "dummy.png"},

            };

            Conversations = new ObservableCollection<Conversation>();
            LoadCurrentPage();
        }
        private void LoadCurrentPage()
        {
            var pageVisits = AllConversations
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            Conversations.Clear();
            foreach (var item in pageVisits)
                Conversations.Add(item);

            PageCounterText.Text = $"{currentPage}/{totalPages}";
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadCurrentPage();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadCurrentPage();
            }
        }
    }
    public class StatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value as string;
            if (status == null)
                return FontAwesomeIcon.CheckCircle; // domyślna ikona

            switch (status.ToLower())
            {
                case "nowa wiadomość":
                    return FontAwesomeIcon.Envelope; // ikona dla nowej wiadomości
                case "odczytane":
                    return FontAwesomeIcon.CheckCircle; // ikona dla odczytane
                default:
                    return FontAwesomeIcon.CheckCircle;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

