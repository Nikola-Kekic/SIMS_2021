using SIMS_2021.Controller;
using SIMS_2021.DTOs;
using SIMS_2021.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for BuyDrugDialog.xaml
    /// </summary>
    public partial class BuyDrugDialog : Window
    {
        public string Quantity { get; set; }

        private Drug _clickedDrug;
        private User _loggedInUser;
        private Cart _cart;
        private static readonly Regex _regex = new Regex("[^0-5]+");
        private PatientsController _patientsController;
        public BuyDrugDialog()
        {
            DataContext = this;
            _patientsController = new PatientsController();
            InitializeComponent();
        }

        private void BuyDrug_Click(object sender, RoutedEventArgs e)
        {
            if (!IsTextAllowed(Quantity))
            {
                MessageBox.Show("Moguc je samo celobrojan unos do 5.");
                return;
            }

           
            int sum = _patientsController.GetBoughtDrugsSum(_loggedInUser.Id);
            if(sum + Int32.Parse(Quantity) > 50)
            {
                MessageBox.Show("Nemoguce je dodati lek u korpu. Predjen je nedelji limit (50).");
                this.Close();
                return;
            }

            if (_cart.DrugsAndQuantity.ContainsKey(_clickedDrug.Id))
            {
                int quantity = _cart.DrugsAndQuantity[_clickedDrug.Id];
                if ( quantity + Int32.Parse(Quantity) > 5)
                {
                    MessageBox.Show("Nemoguce je dodati lek u korpu. Predjen je limit za jedan lek (5).");
                    this.Close();
                    return;
                }  else
                {
                    _cart.DrugsAndQuantity[_clickedDrug.Id] += Int32.Parse(Quantity);
                }

            } else
            {
                _cart.DrugsAndQuantity.Add(_clickedDrug.Id, Int32.Parse(Quantity));
            }
            MessageBox.Show("Lek je dodat u korpu!");
            DataContext = new Drug();
            this.Close();
            return;

        }

        public void Show(Drug clickedDrug, User loggedInUser, Cart cart)
        {
            _clickedDrug = clickedDrug;
            _loggedInUser = loggedInUser;
            _cart = cart;
            this.Show();
        }
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
