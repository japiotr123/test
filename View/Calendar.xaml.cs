using MySql.Data.MySqlClient;
using PolMedUMG.Model;
using PolMedUMG.ViewModel;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
namespace PolMedUMG.View
{
    /// <summary>
   /// Logika interakcji dla klasy Calendar.xaml
   /// </summary>
    public partial class Calendar : UserControl
    {
        private int Year;
        public ObservableCollection<DateTime> Months { get; set; }
        public ObservableCollection<Model.Visit> PlannedVisits { get; set; }

        public int year
        {
            get
            {
                return Year;
            }

            set
            {
                Year = value;
                rok.Text = Year.ToString();
                Calendar_setMonths(Year);
            }
        }
        public Calendar()
        {
            InitializeComponent();
            year = DateTime.Now.Year;
            getVisits();


        }
        public void getVisits()
        {
            using (MySqlConnection conn = new MySqlConnection(SessionManager.connStrSQL))
            {
                try
                {
                    conn.Open();
                    PlannedVisits = new ObservableCollection<Model.Visit>();
                    string sql = "SELECT `causeOfVisit`, `additionalInfo`, `phoneNumber`, `dateOfVisit`, `serviceName`  FROM `Visits`";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlannedVisits.Add(new Model.Visit
                            {
                                causeOfVisit = reader["causeOfVisit"].ToString() ?? "",
                                additionalInfo = Convert.ToString(reader["additionalInfo"]) ?? "",
                                PhoneNumber = Convert.ToString(reader["phoneNumber"]) ?? "",
                                DateOfVisit = Convert.ToDateTime(reader["dateOfVisit"]),
                                serviceName = Convert.ToString(reader["serviceName"]) ?? ""
                             });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Obsługa błędów
                    MessageBox.Show("Błąd podczas pobierania danych: " + ex.Message);
                }
            }
        }
        // wczytanie kalendarzy do grida
        public void Calendar_setMonths(int year)
        {
            monthGrid.ItemsSource = new ObservableCollection<DateTime>
            {
                new DateTime(year, 1, 1),
                new DateTime(year, 2, 1),
                new DateTime(year, 3, 1),
                new DateTime(year, 4, 1),
                new DateTime(year, 5, 1),
                new DateTime(year, 6, 1),
                new DateTime(year, 7, 1),
                new DateTime(year, 8, 1),
                new DateTime(year, 9, 1),
                new DateTime(year, 10, 1),
                new DateTime(year, 11, 1),
                new DateTime(year, 12, 1)
            };
        }
        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            // pozbycie się dni spoza danego miesiąca
            var calendar = (System.Windows.Controls.Calendar)sender;
            calendar.DisplayMode = CalendarMode.Month;
            DateTime begin = (DateTime)calendar.DisplayDateStart;
            calendar.DisplayDateEnd = begin.AddDays(DateTime.DaysInMonth(begin.Year, begin.Month) - 1);

            // zaznaczenie dni wizyt w kalendarzu
            if (PlannedVisits != null)
            {
                foreach (var d in PlannedVisits.Select(v => v.DateOfVisit).Distinct())
                {
                    if (calendar.DisplayDate.Month == d.Month)
                    {
                        foreach (var btn in FindVisualChildren<CalendarDayButton>(calendar))
                            if (btn.IsInactive == false && btn.DataContext is DateTime c && d.Date == c)
                            {
                                btn.Background = new RadialGradientBrush(
                                    Colors.Yellow,
                                    Color.FromArgb(0, 230, 230, 0))
                                {
                                    GradientOrigin = new Point(0.5, 0.5),
                                    Center = new Point(0.5, 0.5),
                                    RadiusX = 0.5,
                                    RadiusY = 0.5
                                };
                            }
                    }
                }
            }

        }
        static IEnumerable<T> FindVisualChildren<T>(DependencyObject o) where T : DependencyObject
        {
            if (o == null) yield break;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
            {
                var c = VisualTreeHelper.GetChild(o, i);
                if (c is T t) yield return t;
                foreach (var i2 in FindVisualChildren<T>(c)) yield return i2;
            }
        }
        //funkcje od przełączania roku
        public void CalendarPrevious(object sender, RoutedEventArgs e)
        {
            year--;
        }
        public void CalendarNext(object sender, RoutedEventArgs e) {
            year++;
        }

        private void Calendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cal = (System.Windows.Controls.Calendar)sender;
            foreach (var d in PlannedVisits)
            {
                var calendar = (System.Windows.Controls.Calendar)sender;
                if (d.DateOfVisit.Date == cal.SelectedDate)
                {
                    MessageBox.Show("Wizyta: " + d.DateOfVisit.ToString() + "\n" +
                                    "Przyczyna: " + d.causeOfVisit + "\n" +
                                    "Dodatkowe informacje: " + d.additionalInfo + "\n" +
                                    "Numer telefonu: " + d.PhoneNumber + "\n" +
                                    "Nazwa usługi: " + d.serviceName);
                }
            }
            cal.SelectedDates.Clear();
        }
    }
}