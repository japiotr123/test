using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
namespace PolMedUMG.View
{
    /// <summary>
   /// Logika interakcji dla klasy Calendar.xaml
   /// </summary>
    public partial class Calendar : UserControl
    {
        private int Year;

        public ObservableCollection<DateTime> Months { get; set; }
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
        }
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
        private void Calendar_Loaded(object sender,RoutedEventArgs e)
        {
            var calendar = (System.Windows.Controls.Calendar)sender;
            calendar.DisplayMode = CalendarMode.Month;
            DateTime begin = (DateTime)calendar.DisplayDateStart;
         
            calendar.DisplayDateEnd = begin.AddDays(DateTime.DaysInMonth(begin.Year, begin.Month) - 1);
        }
        public void CalendarPrevious(object sender, RoutedEventArgs e)
        {
            year--;
        }
        public void CalendarNext(object sender, RoutedEventArgs e) {
            year++;
        }
    }
}