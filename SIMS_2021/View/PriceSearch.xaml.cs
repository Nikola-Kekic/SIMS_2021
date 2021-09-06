using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS_2021.View
{
    /// <summary>
    /// Interaction logic for PriceSearch.xaml
    /// </summary>
    public partial class PriceSearch : Window
    {
        public string LowestPrice { get; set; }
        public string HighestPrice { get; set; }
        public ListDrugs ListDrugs { get; set;}

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        public PriceSearch()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Pretrazi_Click(object sender, RoutedEventArgs e)
        {
            if (!IsTextAllowed(HighestPrice) || !IsTextAllowed(LowestPrice))
            {
                MessageBox.Show("Forma nije pravilno popunjena.");
                return;
            }
           
            ListDrugs.PriceSearch(HighestPrice, LowestPrice);
            this.Close();
        }

        public void Show(ListDrugs parent)
        {
            this.Show();
            ListDrugs = parent;   
        }
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
