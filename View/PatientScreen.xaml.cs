using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PolMedUMG.View
{
    public partial class PatientScreen : Window
    {
        public PatientScreen()
        {
            InitializeComponent();
            LoadContent(new NewsView());
        }

        public void LoadContent(UserControl control)
        {

            if (RightContentPanel != null)
            {
                RightContentPanel.Children.Clear();
                RightContentPanel.Children.Add(control);
            }
        }

        private void MyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavList.SelectedItem is ListBoxItem selectedItem)
            {
                string selectedText = selectedItem.Content.ToString();

                switch (selectedText)
                {
                    case "Strona główna":
                        LoadContent(new NewsView());
                        break;
                    case "Umów wizytę":
                        LoadContent(new MakeAppointment());
                        break;
                    case "Kalendarz":
                        LoadContent(new Calendar());
                        break;
                    case "Historia wizyt":
                        LoadContent(new Visits());
                        break;
                    case "Wyniki badań":
                        LoadContent(new Results());
                        break;
                    case "Cennik":
                        LoadContent(new Pricing());
                        break;
                    case "Specjaliści":
                        LoadContent(new Specialists());
                        break;
                    case "Wiadomości":
                        LoadContent(new Messages());
                        break;
                    case "Kontakt":
                        LoadContent(new Contact());
                        break;
                    case "Ustawienia konta":
                        LoadContent(new Settings());
                        break;
                    default:
                        break;
                }
            }
        }

        private void NewsView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
