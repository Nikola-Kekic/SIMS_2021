using SIMS_2021.DTOs;
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

namespace SIMS_2021.View.Patient
{
    /// <summary>
    /// Interaction logic for ShowDrugsAndQuantity.xaml
    /// </summary>
    public partial class ShowDrugsAndQuantity : Window
    {
        public ShowDrugsAndQuantity()
        {
            InitializeComponent();
        }

        public void Show(List<DrugAndQuantityDTO> drugAndQuantityDTOs)
        {
            DataContext = drugAndQuantityDTOs;
            BindGrid();
            this.Show();
        }

        public void BindGrid()
        {
            this.BillsDataGrid.ItemsSource = (List<DrugAndQuantityDTO>)DataContext;
        }
    }
}
