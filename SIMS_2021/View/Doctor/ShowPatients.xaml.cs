using SIMS_2021.Controller;
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

namespace SIMS_2021.View.Doctor
{
    /// <summary>
    /// Interaction logic for ShowPatients.xaml
    /// </summary>
    public partial class ShowPatients : UserControl
    {
        private User _newUser {get; set;}
        private PatientsController _patientsController;
        private CollectionViewSource _itemSourceList;
        private List<string> _userFieldsStrings;

        public string SearchParameterSelected { get; set; }
        public ShowPatients()
        {
            SearchParameterSelected = "";
            _userFieldsStrings = typeof(User).GetProperties().Select(x => x.Name)
                .Where(x => x != "Id" && x != "Password").ToList();

            InitializeComponent();
            _patientsController = new PatientsController();
            BindGrid((List<User>)this.DataContext);
            FillUserComboBox();
        }
        private void FillUserComboBox()
        {
            foreach (var field in _userFieldsStrings)
            {
                this.UserFields.Items.Add(field);
            }
        }

        public void BindGrid(List<User> list)
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            // fill observale collection for filtering
            if (list != null)
            {
                foreach (var user in list)
                {
                    users.Add(user);
                }
                // Collection which will take your ObservableCollection
                _itemSourceList = new CollectionViewSource() { Source = users };

                _itemSourceList.Filter += new FilterEventHandler(Filter);

                // ICollectionView the View/UI part 
                ICollectionView Itemlist = _itemSourceList.View;

                this.PatientsDataGrid.ItemsSource = Itemlist;
            }

        }
        private void Filter(object sender, FilterEventArgs e)
        {
            var obj = e.Item as User;
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

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            AddPatient addPatient = new AddPatient();
            addPatient.Show(this);
        }
        private void TextSearch(object sender, TextChangedEventArgs e)
        {

            _itemSourceList.View.Refresh();
        }
        private void UserFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                SearchParameterSelected = e.AddedItems[0].ToString();
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
