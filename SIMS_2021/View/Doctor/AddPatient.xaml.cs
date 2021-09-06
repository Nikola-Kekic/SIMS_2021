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
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        private ShowPatients _parent;
        public AddPatient()
        {
            InitializeComponent();
        }

        public void Show(ShowPatients parent)
        {
            this._parent = parent;
            this.DataContext = new User();
            this.Show();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            User newPatient = (User)DataContext;
            bool errors = string.IsNullOrEmpty(newPatient.Name)
                || string.IsNullOrEmpty(newPatient.LastName)
                || string.IsNullOrEmpty(newPatient.Email)
                || string.IsNullOrEmpty(newPatient.JMBG)
                || string.IsNullOrEmpty(newPatient.MobileNumber)
                || string.IsNullOrEmpty(newPatient.Password);
            
            if (!errors)
            {
                _parent.SavePatient(newPatient);
                this.Close();
            } else
            {
                MessageBox.Show("Forma nije popunjena.");
            }
        }
    }
}
