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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_2021.View.Pharmacist
{
   
    /// <summary>
    /// Interaction logic for Ingredients.xaml
    /// </summary>
    public partial class Ingredients : Window
    {
        private Drug _drug;
      
        public Ingredients()
        {
            DataContext = new Ingredient();
            InitializeComponent();
        }

        internal void Show(Drug drug)
        {
            _drug = drug;
            this.Show();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (_drug.Ingredients == null)
                _drug.Ingredients = new Dictionary<string, string>();
            Ingredient newIngredient = (Ingredient)DataContext;
            if (newIngredient != null)
            {
                bool canBeAdded = newIngredient.Name != "" && newIngredient.Description != "";

                if (canBeAdded)
                {
                    var ingredient = new Ingredient() { Description = newIngredient.Description, Name = newIngredient.Name };
                    
                    _drug.Ingredients.Add(ingredient.Name, ingredient.Description);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Forma nije pravilno popunjena.");
                }
            }
        }
     
    }
    
}
