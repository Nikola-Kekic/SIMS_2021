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

namespace SIMS_2021.View.Doctor
{
    /// <summary>
    /// Interaction logic for PrikazPacijenata.xaml
    /// </summary>
    public partial class ShowPatients : UserControl
    {
        private User _newUser {get; set;}
        private PatientsController _patientsController;
        public ShowPatients()
        {
            InitializeComponent();
            _patientsController = new PatientsController();
            BindGrid((List<User>)this.DataContext);
        }
        public void BindGrid(List<User> list)
        {
            this.PatientsDataGrid.ItemsSource = list;
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            AddPatient addPatient = new AddPatient();
            addPatient.Show(this);
        }

        public void SavePatient(User patient)
        {
            bool added = _patientsController.AddPatient(patient);
            if (added)
            {
                MessageBox.Show("Pacijent je registrovan.");
                this.DataContext = _patientsController.GetAll();
                BindGrid((List<User>)this.DataContext);
            }
            else
            {
                MessageBox.Show("Pacijent nije uspesno registrovan.");
            }
        }
    }
}
