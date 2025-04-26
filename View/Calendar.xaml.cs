using System.Windows.Controls;
using PolMedUMG.ViewModel;

namespace PolMedUMG.View
{
    /// <summary>
-   /// Logika interakcji dla klasy Calendar.xaml
-   /// </summary>
    public partial class Calendar : UserControl
    {
        public Calendar()
        {
            InitializeComponent();
            DataContext = new CalendarModel();

        }

        private void Calendar_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var calendar = (System.Windows.Controls.Calendar)sender;
            calendar.DisplayMode = CalendarMode.Month;
        }

        private void Calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            ((System.Windows.Controls.Calendar)sender).DisplayMode = CalendarMode.Month;
            e.Handled = true;
        }
    }
}