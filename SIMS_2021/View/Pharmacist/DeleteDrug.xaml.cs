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
    /// Interaction logic for DeleteDrug.xaml
    /// </summary>
    public partial class DeleteDrug : Window
    {
        public Drug DrugToDelete { get; set; }
        private DrugsController _drugsController;
        public DeleteDrug()
        {
            InitializeComponent();
            _drugsController = new DrugsController();
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (DrugToDelete.Code.Equals(this.CodeToDelete.Text))
            {
                _drugsController.DeleteDrug(DrugToDelete);
                DrugToDelete.Deleted = true;
                MessageBox.Show("Lek je obrisan.");
                this.Close();
            } else
            {
                MessageBox.Show("Pogresna sifra leka.");

            }
        }

        public void Show(Drug toDelete)
        {
            DrugToDelete = toDelete;
            this.Show();
        }
    }
}
