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

namespace SIMS_2021.View.Pharmacist
{
    /// <summary>
    /// Interaction logic for RejectedReasone.xaml
    /// </summary>
    
    public partial class RejectedReasone : Window
    {
        private RejectedController _rejectedController;
        private long _drugId;
        private RejectedDrug _rejected;
        public RejectedReasone()
        {
            InitializeComponent();
            this.DataContext = new RejectedDrug();
            _rejected = new RejectedDrug();
            _rejectedController = new RejectedController();
        }

        public void Show(long drugId)
        {
            _rejected = _rejectedController.GetByDrugId(drugId);
            DataContext = _rejected;
            _drugId = drugId;
            this.Show();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
