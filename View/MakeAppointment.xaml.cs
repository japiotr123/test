using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PolMedUMG.View
{
    public partial class MakeAppointment : UserControl
    {
        private TimeSpan currentTime = new TimeSpan(18, 0, 0); // Domyślna godzina 18:00
        private DateTime currentMonth = DateTime.Today;
        private Button selectedDayButton = null; // Dla podświetlenia wybranej daty

        public MakeAppointment()
        {
            InitializeComponent();
            InitializeForm();
            LoadCalendar();
            UpdateTimeDisplay();
        }

        private void InitializeForm()
        {
            DoctorComboBox.ItemsSource = new List<string>
            {
                "Dr Anna Kowalska – Kardiolog",
                "Dr Jan Nowak – Internista",
                "Dr Marta Wiśniewska – Pediatra",
                "Dr Piotr Zieliński – Dermatolog"
            };
            DoctorComboBox.Text = "Wybierz specjalistę";

            ServiceComboBox.ItemsSource = new List<string>
            {
                "Konsultacja specjalistyczna",
                "Badania kontrolne",
                "Szczepienie",
                "Wizyta domowa"
            };
            ServiceComboBox.Text = "Wybierz usługę";
        }

        // KALENDARZ
        private void LoadCalendar()
        {
            MonthLabel.Text = currentMonth.ToString("MMMM yyyy");
            DaysGrid.Children.Clear();

            string[] days = { "Pn", "Wt", "Śr", "Cz", "Pt", "Sb", "Nd" };
            foreach (var day in days)
            {
                DaysGrid.Children.Add(new TextBlock
                {
                    Text = day,
                    FontWeight = FontWeights.Bold,
                    FontFamily = new FontFamily("Chakra Petch"),
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Center
                });
            }

            DateTime firstDay = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            int startOffset = ((int)firstDay.DayOfWeek + 6) % 7;

            for (int i = 0; i < startOffset; i++)
                DaysGrid.Children.Add(new TextBlock());

            int daysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                Button dayButton = new Button
                {
                    Content = day.ToString(),
                    Width = 30,
                    Height = 30,
                    Margin = new Thickness(2),
                    Tag = day,
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new Thickness(1),
                    FontFamily = new FontFamily("Chakra Petch")
                };

                dayButton.Click += DayButton_Click;
                DaysGrid.Children.Add(dayButton);
            }
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (selectedDayButton != null)
                {
                    selectedDayButton.Background = Brushes.Transparent;
                    selectedDayButton.Foreground = Brushes.Black;
                }

                selectedDayButton = btn;
                selectedDayButton.Background = new SolidColorBrush(Color.FromRgb(74, 108, 247)); // Niebieskie tło
                selectedDayButton.Foreground = Brushes.White;
            }
        }

        private void PrevMonth_Click(object sender, RoutedEventArgs e)
        {
            currentMonth = currentMonth.AddMonths(-1);
            LoadCalendar();
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            currentMonth = currentMonth.AddMonths(1);
            LoadCalendar();
        }

        // GODZINA
        private void UpdateTimeDisplay()
        {
            TimeDisplay.Text = $"{currentTime.Hours:D2}:{currentTime.Minutes:D2}";
        }

        private void IncreaseTime_Click(object sender, RoutedEventArgs e)
        {
            currentTime = currentTime.Add(new TimeSpan(0, 30, 0));
            if (currentTime.TotalHours >= 24)
                currentTime = TimeSpan.Zero;
            UpdateTimeDisplay();
        }

        private void DecreaseTime_Click(object sender, RoutedEventArgs e)
        {
            currentTime = currentTime.Subtract(new TimeSpan(0, 30, 0));
            if (currentTime.TotalMinutes < 0)
                currentTime = new TimeSpan(23, 30, 0);
            UpdateTimeDisplay();
        }

        // PLACEHOLDER HANDLING
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && (tb.Text == "Wpisz powód wizyty" || tb.Text == "Wpisz dodatkowe uwagi" || tb.Text == "Wpisz swój numer telefonu"))
            {
                tb.Text = string.Empty;
                tb.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                switch (tb.Name)
                {
                    case "PurposeTextBox":
                        tb.Text = "Wpisz powód wizyty";
                        break;
                    case "NotesTextBox":
                        tb.Text = "Wpisz dodatkowe uwagi";
                        break;
                    case "PhoneTextBox":
                        tb.Text = "Wpisz swój numer telefonu";
                        break;
                }
                tb.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        // WALIDACJA
        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorComboBox.Text == "Wybierz specjalistę" ||
                ServiceComboBox.Text == "Wybierz usługę" ||
                string.IsNullOrWhiteSpace(PurposeTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneTextBox.Text) ||
                PurposeTextBox.Text == "Wpisz powód wizyty" ||
                PhoneTextBox.Text == "Wpisz swój numer telefonu")
            {
                MessageBox.Show("Proszę uzupełnić wszystkie wymagane pola.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Twoje zgłoszenie zostało wysłane!");
        }
    }
}
