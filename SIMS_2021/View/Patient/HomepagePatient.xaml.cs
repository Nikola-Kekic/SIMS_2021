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
    /// Interaction logic for HomepagePatient.xaml
    /// </summary>
    public partial class HomepagePatient : UserControl
    {
        private TransitionControl _transCtrl;
        private DrugsController _drugsController;
        private IngredientsController _ingredientsController;
        private PatientsController _patientsController;
        private BillsController _billsController;
        private List<Drug> _drugs;
        private List<IngredientDTO> _ingredients;
        private Cart _cart;
        private User _patient;
        public HomepagePatient(TransitionControl transitionControl, User patient)
        {
            InitializeComponent();
            _transCtrl = transitionControl;
            _drugsController = new DrugsController();
            _ingredientsController = new IngredientsController();
            _patientsController = new PatientsController();
            _billsController = new BillsController();
            _cart = new Cart();
            _patient = patient;
            ShowProfile();
        }

        private void ShowProfile()
        {
            this.Ingredients.Visibility = Visibility.Hidden;
            this.Drugs.Visibility = Visibility.Hidden;
            this.CartView.Visibility = Visibility.Hidden;
            this.Bills.Visibility = Visibility.Hidden;
            this.Profile.DataContext = _patient;
            this.Profile.Visibility = Visibility.Visible;
        }

        private void Button_Click0(object sender, RoutedEventArgs e)
        {
            var transControl = new TransitionControl(_transCtrl.ParentWindow);
            var screenOne = new LogIn(transControl);
            _transCtrl.ParentWindow.ChangeContent(screenOne);
        }
        private void IngredientsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Drugs.Visibility = Visibility.Hidden;
            this.Profile.Visibility = Visibility.Hidden;
            this.CartView.Visibility = Visibility.Hidden;
            this.Bills.Visibility = Visibility.Hidden;
            _ingredients = _ingredientsController.GetAll();
            this.Ingredients.DataContext = _ingredients;
            this.Ingredients.BindGrid(_ingredients);
            this.Ingredients.Visibility = Visibility.Visible;
        }

        private void DrugsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Ingredients.Visibility = Visibility.Hidden;
            this.Profile.Visibility = Visibility.Hidden;
            this.CartView.Visibility = Visibility.Hidden;
            this.Bills.Visibility = Visibility.Hidden;

            _drugs = _drugsController.GetAllNotDeleted();
            this.Drugs.DataContext = _drugs;
            this.Drugs.SaveCurrentUser(_patient);
            this.Drugs.SaveCurrentCart(_cart);
            this.Drugs.BindGrid(_drugs);
            this.Drugs.Visibility = Visibility.Visible;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridBarra_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void BillsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Ingredients.Visibility = Visibility.Hidden;
            this.Profile.Visibility = Visibility.Hidden;
            this.CartView.Visibility = Visibility.Hidden;
            this.Drugs.Visibility = Visibility.Hidden;

            var bills = _billsController.GetAllForUser(_patient.Id);
            int numOfDrugs = _patientsController.GetBoughtDrugsSum(_patient.Id);
            this.Bills.DataContext = new LabelDTO() { Property = numOfDrugs };
            this.Bills.BindGrid(bills);
            this.Bills.Visibility = Visibility.Visible;

        }
        public void RefreshCart()
        {
            _cart = new Cart();
            _cart.DrugsAndQuantity = new Dictionary<long, int>();
        }
        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            this.Ingredients.Visibility = Visibility.Hidden;
            this.Profile.Visibility = Visibility.Hidden;
            this.Drugs.Visibility = Visibility.Hidden;
            this.Bills.Visibility = Visibility.Hidden;

            if (_cart.DrugsAndQuantity == null)
                _cart.DrugsAndQuantity = new Dictionary<long, int>();

            List<CartDTO> cartDTOs = _patientsController.GetDrugsAndQuantity(_cart.DrugsAndQuantity);
            this.CartView.SetParent(this);
            this.CartView.DataContext = cartDTOs;
            this.CartView.BindGrid();
            this.CartView.SaveUser(_patient);
            this.CartView.Visibility = Visibility.Visible;
        }
    }
}
