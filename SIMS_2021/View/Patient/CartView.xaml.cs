using SIMS_2021.Controller;
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
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : UserControl
    {
        private BillsController _billsController;
        private User _loggedInUser;
        private HomepagePatient _parent;
        public CartView()
        {
            _billsController = new BillsController();
            InitializeComponent();
        }
        public void BindGrid()
        {
            this.CartDataGrid.ItemsSource = (List<CartDTO>)DataContext;
        }
        public void SetParent(HomepagePatient parent)
        {
            _parent = parent;
        }
        public void SaveUser(User loggedInUser)
        {
            _loggedInUser = loggedInUser;
        }

        private void Kupi_Click(object sender, RoutedEventArgs e)
        {
            if (_billsController.Buy((List<CartDTO>)DataContext, _loggedInUser.Id))
            {
                DataContext = new List<CartDTO>();
                BindGrid();
                _parent.RefreshCart();
                MessageBox.Show("Uspesno ste kupili lekove.");
            }
            else
            {
                MessageBox.Show("Nemoguce je kupiti lekove.");
            }

        }
    }
}
