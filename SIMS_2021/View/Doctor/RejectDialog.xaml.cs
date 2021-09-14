using SIMS_2021.Controller;
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
using System.Windows.Shapes;

namespace SIMS_2021.View.Doctor
{
    /// <summary>
    /// Interaction logic for RejectDialog.xaml
    /// </summary>
    public partial class RejectDialog : Window
    {
        private RejectedController _rejectedController;
        private long _drugId;
        private RejectedDrug rejected;
        public ListDrugs ListDrugs { get; set; }
        public RejectDialog()
        {
            InitializeComponent();
            this.DataContext = new RejectedDrug();
            rejected = new RejectedDrug();
            _rejectedController = new RejectedController();
        }

        public void Show(long drugId)
        {
            _drugId = drugId;
            this.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            rejected = (RejectedDrug)this.DataContext; 

            _rejectedController.Save(_drugId , rejected.Description);
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
