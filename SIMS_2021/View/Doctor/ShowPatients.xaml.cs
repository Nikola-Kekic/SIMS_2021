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
        public ShowPatients()
        {
            InitializeComponent();
            BindGrid((List<User>)this.DataContext);
        }
        public void BindGrid(List<User> list)
        {
            this.PatientsDataGrid.ItemsSource = list;
        }

    }
}
