using SIMS_2021.Controller;
using SIMS_2021.Model;
using SIMS_2021.View.Doctor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Controls.Primitives;
using System.Data;
using SIMS_2021.View.Patient;
using SIMS_2021.DTOs;
using SIMS_2021.View.Pharmacist;

namespace SIMS_2021.View
{
    /// <summary>
    /// Interaction logic for ListDrugs.xaml
    /// </summary>
    public partial class ListDrugs : UserControl
    {

        private CollectionViewSource _itemSourceList;
        public string SearchParameterSelected { get; set; }
        public float LowestPrice { get; set; }
        public float HighestPrice { get; set; }
        public string SearchString { get; set; }
        public string TextIngredients { get; set; }

        public DrugsController _drugController;

        private User _loggedInUser;

        private List<string> _drugFieldsStrings;
        private Cart _cart;
        private bool _addedButtons;

        public ListDrugs()
        {
            SearchParameterSelected = "";
            
            _drugFieldsStrings = typeof(Drug).GetProperties().Select(x => x.Name)
                .Where(x => x != "Id" && x != "Deleted" && x != "Accepted" && x != "Ingredients" && x != "Price").ToList();
           
            _drugController = new DrugsController();
            InitializeComponent();
            FillDrugsComboBox();
        }
        public void SaveCurrentCart(Cart cart)
        {
            _cart = cart;
        }
        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {

            Drug clickedDrug = (Drug)(((CheckBox)sender).DataContext);
            clickedDrug = _drugController.MenageAccepted(clickedDrug.Id, (bool)((CheckBox)sender).IsChecked);
        }
        private void ButtonReject(object sender, RoutedEventArgs e) 
        {
            RejectDialog reject = new RejectDialog();
            Drug clickedDrug = (Drug)((Button)sender).DataContext;
            reject.Show(clickedDrug.Id);
        }
        private void ButtonRejectReasone(object sender, RoutedEventArgs e)
        {
            RejectedReasone reject = new RejectedReasone();
            Drug clickedDrug = (Drug)((Button)sender).DataContext;
            reject.Show(clickedDrug.Id);
        }
        public void SaveCurrentUser(User user)
        {
            _loggedInUser = user;

            if (_loggedInUser.UserType.Equals(UserType.Pharmacist))
            {
                _drugFieldsStrings.Add("Accepted");
                DrugFields.Items.Clear();
                FillDrugsComboBox();
            }
        }
        private void FillDrugsComboBox()
        {
            foreach (var field in _drugFieldsStrings)
            {
                this.DrugFields.Items.Add(field);
            }
        }
        public void BindGrid(List<Drug> list)
        {
            this.DataContext = list;
            ObservableCollection<Drug> _drugs = new ObservableCollection<Drug>();
            // fill observale collection for filtering
            if (this.DataContext != null)
            {
                foreach (var drug in (List<Drug>)this.DataContext)
                {
                    _drugs.Add(drug);
                }
                // Collection which will take your ObservableCollection
                _itemSourceList = new CollectionViewSource() { Source = _drugs };

                _itemSourceList.Filter += new FilterEventHandler(Filter);


                LowestPrice = 0;
                HighestPrice = float.MaxValue;

                _itemSourceList.Filter += new FilterEventHandler(IngredientsFilter);
                _itemSourceList.Filter += new FilterEventHandler(PriceFilter);

                // ICollectionView the View/UI part 
                ICollectionView Itemlist = _itemSourceList.View;

                this.DrugDataGrid.ItemsSource = Itemlist;

                if (_loggedInUser.UserType.Equals(UserType.Pharmacist))
                {
                    DrugDataGrid.Columns[6].Visibility = Visibility.Collapsed;
                    DrugDataGrid.Columns[7].Visibility = Visibility.Collapsed;

                    DataGridTextColumn textColumn = new DataGridTextColumn();
                    textColumn.Header = "Odobren";
                    textColumn.Binding = new Binding("Accepted");
                    DrugDataGrid.Columns.Add(textColumn);
                }
                else if (_loggedInUser.UserType.Equals(UserType.Patient))
                {
                    DrugDataGrid.Columns[6].Visibility = Visibility.Collapsed;
                    DrugDataGrid.Columns[7].Visibility = Visibility.Collapsed;
                    DrugDataGrid.Columns[8].Visibility = Visibility.Collapsed;

                    if (!_addedButtons)
                    {

                        DataGridTemplateColumn buttonColumn = new DataGridTemplateColumn();
                        DataTemplate buttonTemplate = new DataTemplate();
                        FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(Button));
                        buttonTemplate.VisualTree = buttonFactory;
                        //add handler or you can add binding to command if you want to handle click
                        buttonFactory.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(OpenBuyDrugDialog));
                        buttonFactory.SetValue(ContentProperty, "Dodaj u korpu");
                        buttonColumn.CellTemplate = buttonTemplate;
                        DrugDataGrid.Columns.Add(buttonColumn);

                        _addedButtons = true;
                    }

                } 
            }

        }
        private void OpenBuyDrugDialog(object sender, RoutedEventArgs e)
        {
            BuyDrugDialog buyDrugDialog = new BuyDrugDialog();
            Drug clickedDrug = (Drug)((Button)sender).DataContext;
            buyDrugDialog.Show(clickedDrug, _loggedInUser, _cart);
        }
        private void Ingredients_Click(object sender, RoutedEventArgs e)
        {
            Drug clickedDrug = (Drug)((Button)sender).DataContext;
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (KeyValuePair<string, string> ingredient in clickedDrug.Ingredients)
            {
                ingredients.Add(new Ingredient { Name = ingredient.Key, Description = ingredient.Value });
            }
            ShowIngredients showIngredients = new ShowIngredients();
            showIngredients.BindGrid(ingredients);
            showIngredients.Show();
        }

        private void Filter(object sender, FilterEventArgs e)
        {
            var obj = e.Item as Drug;
            bool canSearch = SearchParameterSelected != null && SearchParameterSelected != "";
            if (obj != null && canSearch)
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
            }
        }

        private void PriceFilter(object sender, FilterEventArgs e)
        {
            var obj = e.Item as Drug;
            bool canSearch = SearchParameterSelected == "Price";

            if (obj != null && canSearch)
            {
                {
                    if (obj.Price > LowestPrice && obj.Price < HighestPrice)
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
            }
        }

        private void TextSearch(object sender, TextChangedEventArgs e)
        {
            
            _itemSourceList.View.Refresh();
        }

        private void DrugFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                SearchParameterSelected = e.AddedItems[0].ToString();
        }

        private void IngredientsFilter(object sender, FilterEventArgs e)
        {
            if (!SearchParameterSelected.Equals(""))
            {
                return;
            }
            if (TextIngredients == null || TextIngredients == "")
            {
                return;
            }

            var obj = e.Item as Drug;

            List<Drug> allDrugs = (List<Drug>)DataContext;
            List<Drug> foundDrugs = new List<Drug>();

            if (TextIngredients.Contains('|'))
            {
                foundDrugs = SearchOr(allDrugs);
            }
            else if (TextIngredients.Contains('&'))
            {
                foundDrugs = SearchAnd(allDrugs);
            }
            else if (TextIngredients == "")
            {
                e.Accepted = false;
            }
            else
            {
                foundDrugs.AddRange(allDrugs.Where(x => x.Ingredients
                .Keys.Contains(TextIngredients.Trim(), StringComparer.InvariantCultureIgnoreCase)).ToList());
            }
            if (foundDrugs.Any(x => x.Code.Equals(obj.Code))) {
                e.Accepted = true;
            } else
            {
                e.Accepted = false;
            }

        }

        private void Button_Click_SearchIngredients(object sender, RoutedEventArgs e)
        {
            TextIngredients =  Search_Ingredients.Text;
            SearchParameterSelected = "";
            _itemSourceList.View.Refresh();

        }
        private void Button_Click_SearchPrice(object sender, RoutedEventArgs e)
        {
            PriceSearch priceSearch = new PriceSearch();
            priceSearch.Show(this);
        }

        public void PriceSearch(string highest, string lowest) 
        {
            HighestPrice = float.Parse(highest);
            LowestPrice = float.Parse(lowest);
            SearchParameterSelected = "Price";
            _itemSourceList.View.Refresh();
        }
        public List<Drug> SearchOr(List<Drug> allDrugs) 
        {
            string[] stringOr = TextIngredients.Split('|');

            List<Drug> foundDrugsOr = new List<Drug>();

            foreach (string s in stringOr)
            {
                foundDrugsOr.AddRange(allDrugs.Where(x => x.Ingredients
                 .Keys.Contains(s.Trim(), StringComparer.InvariantCultureIgnoreCase)).ToList());
            }

            return foundDrugsOr;
        }
        public List<Drug> SearchAnd(List<Drug> allDrugs)
        {
            List<Drug> foundDrugsAnd = new List<Drug>();
            foreach (var drug in allDrugs)
            {
                foundDrugsAnd.Add(drug);
            }
            string[] stringAnd = TextIngredients.Split('&');
            List<Drug> removeList = new List<Drug>();
            foreach (string s in stringAnd)
            {
                foreach (var drug in foundDrugsAnd)
                {
                    if (!drug.Ingredients.Keys.Contains(s.Trim(),
                        StringComparer.InvariantCultureIgnoreCase))
                    {
                        removeList.Add(drug);
                    }
                }
            }

            foreach (Drug drug in removeList)
            {
                foundDrugsAnd.Remove(drug);
            }
            return foundDrugsAnd;
        }

      
    }

}
