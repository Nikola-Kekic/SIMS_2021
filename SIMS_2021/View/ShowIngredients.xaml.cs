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

namespace SIMS_2021.View
{
    /// <summary>
    /// Interaction logic for ShowIngredients.xaml
    /// </summary>
    public partial class ShowIngredients : Window
    {
        public ShowIngredients()
        {
            InitializeComponent();
        }

        public void BindGrid(List<Ingredient> list)
        {
            DataContext = list;
            this.IngredientsDataGrid.ItemsSource = (List<Ingredient>)DataContext;
        }
    }
}
