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

namespace SIMS_2021.View.Pharmacist
{
    /// <summary>
    /// Interaction logic for HomepagePharmacist.xaml
    /// </summary>
    public partial class HomepagePharmacist : UserControl
    {
        TransitionControl _transitionCtrl;
        public DrugsController _drugsController;
        public IngredientsController _ingredientsController;
        List<Drug> _drugs;
        List<IngredientDTO> _ingredients;

        User _pharmacist;
        public HomepagePharmacist(TransitionControl transitionControl, User user)
        {
            InitializeComponent();
            _transitionCtrl = transitionControl;
            _drugsController = new DrugsController();
            _ingredientsController = new IngredientsController();
            _pharmacist = user;
            ShowProfile();
        }

        private void ShowProfile()
        {
            this.NewDrug.Visibility = Visibility.Hidden;
            this.Ingredients.Visibility = Visibility.Hidden;
            this.Drugs.Visibility = Visibility.Hidden;
            this.Profile.DataContext = _pharmacist;
            this.Profile.Visibility = Visibility.Visible;
        }

        private void Button_Click0(object sender, RoutedEventArgs e)
        {
            var transControl = new TransitionControl(_transitionCtrl.ParentWindow);
            var screenOne = new LogIn(transControl);
            _transitionCtrl.ParentWindow.ChangeContent(screenOne);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void IngredientsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Drugs.Visibility = Visibility.Hidden;
            this.Profile.Visibility = Visibility.Hidden;
            this.NewDrug.Visibility = Visibility.Hidden;
            _ingredients = _ingredientsController.GetAll();
            this.Ingredients.DataContext = _ingredients;
            this.Ingredients.BindGrid(_ingredients);
            this.Ingredients.Visibility = Visibility.Visible;
        }

        private void DrugsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Ingredients.Visibility = Visibility.Hidden;
            this.Profile.Visibility = Visibility.Hidden;
            this.NewDrug.Visibility = Visibility.Hidden;
            _drugs = _drugsController.GetAll();
            this.Drugs.DataContext = _drugs;
            this.Drugs.SaveCurrentUser(_pharmacist);
            this.Drugs.BindGrid(_drugs);
            this.Drugs.Visibility = Visibility.Visible;
        }

        private void ShowNewDrug(object sender, RoutedEventArgs e)
        {
            this.Drugs.Visibility = Visibility.Hidden;
            this.Ingredients.Visibility = Visibility.Hidden;
            this.Profile.Visibility = Visibility.Hidden;
            this.NewDrug.Visibility = Visibility.Visible;
        }

    
    }
}
