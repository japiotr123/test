using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PolMedUMG.View
{
    public class Result
    {
        public string Date { get; set; }
        public string TestType { get; set; } // np. Morfologia krwi
        public string Status { get; set; } // np. odczytane / nieodczytane
        public string OrderNumber { get; set; }       // numer zlecenia
        public string SampleDate { get; set; }        // data pobrania
        public string ResultDate { get; set; }        // data wykonania



        public bool IsRead => Status == "odczytane";

    }

    public partial class Results : UserControl
    {
        private List<Result> allResults;
        private int currentPage = 1;
        private int pageSize = 8;
        private int totalPages => (int)Math.Ceiling((double)allResults.Count / pageSize);

        public Results()
        {
            InitializeComponent();

            allResults = new List<Result>
            {
                new Result { Date = "12.07.2020", SampleDate ="12.07.2020", ResultDate = "14.07.2020" ,TestType = "Morfologia krwi", Status = "odczytane", OrderNumber = "ZL20200712-01", },
                new Result { Date = "08.02.2019", SampleDate ="08.02.2019", ResultDate = "11.02.2019" ,TestType = "Lipidogram", Status = "odczytane",OrderNumber = "AK471228-47" },
                new Result { Date = "22.09.2017", SampleDate ="22.09.2017", ResultDate = "23.09.2017" ,TestType = "Mocz – badania ogólne", Status = "nieodczytane",OrderNumber = "93293219DA12-31" },
                new Result { Date = "20.01.2015", SampleDate ="20.01.2015", ResultDate = "20.01.2015" ,TestType = "Morfologia krwi", Status = "odczytane", OrderNumber = "ZE3981232-93", },
                new Result { Date = "03.07.2012", SampleDate ="03.07.2012", ResultDate = "04.07.2012" ,TestType = "Przeciwciała", Status = "nieodczytane",OrderNumber = "DSKDSJ213-38" },
            };

            LoadCurrentPage();
        }

        private void LoadCurrentPage()
        {
            var pageResults = allResults
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ResultsListBox.ItemsSource = null;
            ResultsListBox.Items.Clear();
            ResultsListBox.ItemsSource = pageResults;

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

        private void ResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResultsListBox.SelectedItem is Result selectedResult)
            {
                var detailsWindow = new ResultsWindow(selectedResult);
                detailsWindow.ShowDialog();
                ResultsListBox.SelectedItem = null;
            }
        }
    }
}
