using SIMS_2021.DTOs;
using SIMS_2021.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_2021.View.Patient
{
    /// <summary>
    /// Interaction logic for ListBills.xaml
    /// </summary>
    public partial class ListBills : UserControl
    {
        public ListBills()
        {
            InitializeComponent();
        }

        public void BindGrid(List<BillDTO> bills)
        {
            this.BillsDataGrid.ItemsSource = bills;

        }

        private void Lekovi_Click(object sender, RoutedEventArgs e)
        {
            BillDTO clicked = (BillDTO)((Button)sender).DataContext;
            List<DrugAndQuantityDTO> drugAndQuantityDTOs = new List<DrugAndQuantityDTO>();
            foreach (KeyValuePair<Drug, int> item in clicked.DrugsAndQuantity)
            {
                drugAndQuantityDTOs.Add(new DrugAndQuantityDTO { Code = item.Key.Code, Name = item.Key.Name, Producer = item.Key.Producer, Price = item.Key.Price, Quantity = item.Value });
            }
            ShowDrugsAndQuantity show = new ShowDrugsAndQuantity();
            show.Show(drugAndQuantityDTOs);
        }
    }
}
