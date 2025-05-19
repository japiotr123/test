using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PolMedUMG.View
{
    public partial class MakeAppointment : UserControl
    {
        public MakeAppointment()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            DoctorComboBox.ItemsSource = new List<string>
            {
                "Dr Anna Kowalska – Kardiolog",
                "Dr Jan Nowak – Internista",
                "Dr Marta Wiśniewska – Pediatra"
            };
            DoctorComboBox.SelectedIndex = 0;

            PurposeComboBox.ItemsSource = new List<string>
            {
                "Konsultacja",
                "Kontrola",
                "Badanie",
                "Inne"
            };
            PurposeComboBox.SelectedIndex = 0;

            TimeComboBox.ItemsSource = GenerateTimeSlots();
            TimeComboBox.SelectedIndex = 0;

            DatePicker.SelectedDate = DateTime.Today.AddDays(1);
        }

        private List<string> GenerateTimeSlots()
        {
            var slots = new List<string>();
            var start = new TimeSpan(8, 0, 0);
            for (int i = 0; i < 20; i++)
            {
                var end = start.Add(TimeSpan.FromMinutes(30));
                slots.Add($"{start:hh\\:mm} - {end:hh\\:mm}");
                start = end;
            }
            return slots;
        }

        private void SendAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PeselTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("Proszę uzupełnić wszystkie wymagane pola.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (TermsCheckBox.IsChecked != true)
            {
                MessageBox.Show("Aby kontynuować, należy zaakceptować regulamin.", "Brak akceptacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Pobranie danych
            var name = NameTextBox.Text;
            var phone = PhoneTextBox.Text;
            var doctor = DoctorComboBox.SelectedItem as string;
            var purpose = PurposeComboBox.SelectedItem as string;
            var time = TimeComboBox.SelectedItem as string;
            var date = DatePicker.SelectedDate?.ToString("dd.MM.yyyy") ?? "nie wybrano";
            var notes = NotesTextBox.Text;

            // Ustawienie podsumowania
            SummaryName.Text = $"Pacjent: {name}";
            SummaryDoctor.Text = $"Lekarz: {doctor}";
            SummaryDate.Text = $"Data: {date}";
            SummaryTime.Text = $"Godzina: {time}";
            SummaryPurpose.Text = $"Cel wizyty: {purpose}";
            SummaryPhone.Text = $"Telefon: {phone}";
            SummaryNotes.Text = $"Uwagi: {notes}";

            MessageBox.Show("Wizyta została pomyślnie zarejestrowana!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
