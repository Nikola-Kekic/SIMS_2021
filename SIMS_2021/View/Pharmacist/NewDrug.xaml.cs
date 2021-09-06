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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_2021.View.Pharmacist
{
    /// <summary>
    /// Interaction logic for NewDrug.xaml
    /// </summary>
    public partial class NewDrug : UserControl
    {
        public DrugsController _drugsController;

        public NewDrug()
        {
            _drugsController = new DrugsController();
            InitializeComponent();
            this.DataContext = new Drug();
        }

        private void AddIngredients_Click(object sender, RoutedEventArgs e)
        {
            Ingredients ingredients = new Ingredients();
            ingredients.Show((Drug)this.DataContext);
        }


        private void AddDrug_Click(object sender, RoutedEventArgs e)
        {
            bool added;
            if (this.DataContext != null)
            {
                added = _drugsController.AddDrug((Drug)this.DataContext);
                if (added)
                {
                    MessageBox.Show("Lek je dodat!");
                    DataContext = new Drug();
                    return;
                }
                MessageBox.Show("Lek nije dodat! Lek vec postoji!");
            }
        }
    }
}
