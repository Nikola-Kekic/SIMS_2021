﻿using System;
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
    /// Interaction logic for TransitionControl.xaml
    /// </summary>
    public partial class TransitionControl : UserControl
    {
        public MainWindow ParentWindow { get; set; }
        public TransitionControl currentScreen { get; set; }
        public TransitionControl(MainWindow _parnet)
        {
            this.ParentWindow = _parnet;
            InitializeComponent();
        }
    }
}
