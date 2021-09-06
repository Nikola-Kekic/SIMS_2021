using SIMS_2021.Controller;
using SIMS_2021.DTOs;
using SIMS_2021.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SIMS_2021.View
{
    /// <summary>
    /// Interaction logic for ListIngredients.xaml
    /// </summary>
    public partial class ListIngredients : UserControl
    {
        private CollectionViewSource _itemSourceList;
        public string TextIngredients { get; set; }
        public string SearchParameterSelected { get; set; }

        private List<string> _ingredientFieldsStrings;

        private IngredientsController _ingredientsController;

        public ListIngredients()
        {
            _ingredientsController = new IngredientsController();

            SearchParameterSelected = "";
            _ingredientFieldsStrings = typeof(IngredientDTO).GetProperties().Select(x => x.Name)
                .Where(x => x != "Id").ToList();
            _ingredientFieldsStrings.Add("Drug");

            InitializeComponent();
            FillIngredientsComboBox();
        }
        private void FillIngredientsComboBox()
        {
            foreach (var field in _ingredientFieldsStrings)
            {
                this.IngredientFields.Items.Add(field);
            }
        }

        public void BindGrid(List<IngredientDTO> list)
        {
            this.IngredientsDataGrid.ItemsSource = list;
            ObservableCollection<IngredientDTO> _ingredients = new ObservableCollection<IngredientDTO>();

            if (this.DataContext != null)
            {
                foreach (var ingredient in (List<IngredientDTO>)this.DataContext)
                {
                    _ingredients.Add(ingredient);
                }
                _itemSourceList = new CollectionViewSource() { Source = _ingredients };

                _itemSourceList.Filter += new FilterEventHandler(Filter);

                //var ingredientsFilter = new Predicate<object>(IngredientsFilter);
                //_itemSourceList.View.Filter += ingredientsFilter;

                ICollectionView Itemlist = _itemSourceList.View;

                this.IngredientsDataGrid.ItemsSource = Itemlist;
            }
        }

        private void TextSearch(object sender, TextChangedEventArgs e)
        {
            _itemSourceList.View.Refresh();
        }

        private void IngredientFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                SearchParameterSelected = e.AddedItems[0].ToString();
        }

        private void Filter(object sender, FilterEventArgs e)
        {
            var obj = e.Item as IngredientDTO;
            bool canSearch = SearchParameterSelected != null && SearchParameterSelected != "";
            if (obj != null && canSearch)
            {
                if (!SearchParameterSelected.Equals("Drug"))
                {
                    var objProperty = obj.GetType().GetProperty(SearchParameterSelected)
                        .GetValue(obj, null);
                    if (objProperty != null)
                    {
                        if (objProperty.ToString().ToLower().Contains(Search.Text.ToLower()))
                            e.Accepted = true;
                        else
                            e.Accepted = false;
                    }
                } else
                {
                    if (Search.Text.Trim().Equals(""))
                    {
                        return;
                    }
                    List<IngredientDTO> ingredients = _ingredientsController.GetAllInDrug(Search.Text.ToLower());
                    if (ingredients.Any(x => x.Name.Equals(obj.Name)))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
            }
        }
    }
}
