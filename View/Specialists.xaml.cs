using PolMedUMG.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PolMedUMG.Model;            // dla Specialist
using PolMedUMG.View;             // dla MessagesOpenConv
using PolMedUMG.View;
using System.Diagnostics;             // jeżeli MessageRepository jest w tej przestrzeni

namespace PolMedUMG.View
{
    /// <summary>
    /// Logika interakcji dla klasy Specialists.xaml
    /// </summary>
    public partial class Specialists : UserControl
    {
        public Specialists()
        {
            InitializeComponent();
            DataContext = new SpecialistsViewModel();
        }

        private void Specialist_Click(object sender, MouseButtonEventArgs e)
        {
            // 1) Pobierz kliknięty kafelek i model Specialist:
            if (!(sender is FrameworkElement element && element.DataContext is Specialist spec))
                return;

            // 2) Załaduj historię rozmów z tego lekarzem:
            var repo = new MessageRepository();
            // GetMessagesFrom zwraca listę ConvMessages posortowaną od najstarszych
            var history = repo
                .GetMessagesFrom(spec.Name, SessionManager.CurrentUsername)
                .OrderBy(m => m.Date)
                .ToList();

            // 3) Przygotuj parametry do MessagesOpenConv:
            //    jeśli brak wiadomości - ustaw domyślnie
            var lastMsg = history.LastOrDefault();
            var date = lastMsg?.Date ?? DateTime.Now;
            var img = lastMsg?.DoctorImage ?? "default_doctor.png";

            // 4) Stwórz kontrolkę czatu:
            var chatControl = new MessagesOpenConv(
                date,               // data ostatniej wiadomości
                spec.Name,          // imię lekarza ("dr. XX")
                img,                // obrazek lekarza
                lastMsg             // ostatnia wiadomość (ConvMessages)
            );

            // 5) Wywołaj metodę LoadContent w głównym oknie (PatientScreen):
            var parentWindow = Window.GetWindow(this) as PatientScreen;
            if (parentWindow != null)
            {
                parentWindow.LoadContent(chatControl);
            }
            else
            {
                Debug.WriteLine("Nie znaleziono PatientScreen, nie można załadować czatu.");
            }
        }

    }
}
