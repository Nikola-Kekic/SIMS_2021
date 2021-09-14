using SIMS_2021.Controller;
using SIMS_2021.Model;
using SIMS_2021.View.Doctor;
using SIMS_2021.View.Patient;
using SIMS_2021.View.Pharmacist;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class LogIn : UserControl
    {
        TransitionControl _transitionControl;
        private LoginController _loginController;
        private int i = 0;
        public Exception Exception { get; set; }

        public LogIn(TransitionControl transitionControl)
        {
            InitializeComponent();
            _transitionControl = transitionControl;
            _loginController = new LoginController();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = _loginController.FindUser(JMBG.Text, Password.Password);


            if (user == null && i < 2)
            {
                ++i;
                MessageBox.Show("Neuspesna prijava! Imate 3 pokusaja da se prijavite na sistem!\n " +
                                "Broj pokusaja: " + i.ToString() + "\n");
                return;
            }
            else if (user != null && i <= 2)
            {
                if (user.UserType.Equals(UserType.Doctor))
                {
                    i = 0;
                    _transitionControl.ParentWindow.ChangeContent(new HomepageDoctor(new TransitionControl(_transitionControl.ParentWindow), user));
                }
                else if (user.UserType.Equals(UserType.Patient))
                {
                    i = 0;
                    _transitionControl.ParentWindow.ChangeContent(new HomepagePatient(new TransitionControl(_transitionControl.ParentWindow), user));
                }
                else if (user.UserType.Equals(UserType.Pharmacist))
                {
                    i = 0;
                    _transitionControl.ParentWindow.ChangeContent(new HomepagePharmacist(new TransitionControl(_transitionControl.ParentWindow), user));
                }
                else
                {
                    MessageBox.Show("Greska prilikom unosa!");
                }
            }
            else
            {
                MessageBox.Show("Aplikacija se gasi!\nImali ste vise od 3 pokusaja.");
                Application.Current.Shutdown();
            }
        }
    }
}
