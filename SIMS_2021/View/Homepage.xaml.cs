using MaterialDesignThemes.Wpf;
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

namespace SIMS_2021.View
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : UserControl
    {
        TransitionControl _transCtrl;
        public Homepage(TransitionControl transitionControl)
        {
            InitializeComponent();
            _transCtrl = transitionControl;
        }

        private void Button_Click0(object sender, RoutedEventArgs e)
        {
            var transControl = new TransitionControl(_transCtrl.ParentWindow);
            var screenOne = new LogIn(transControl);
            _transCtrl.ParentWindow.ChangeContent(screenOne);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridBarra_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
