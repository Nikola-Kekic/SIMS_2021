using SIMS_2021.View;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_2021
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        readonly Duration _animationDuration = new Duration(TimeSpan.FromSeconds(0.3));
        public MainWindow()
        {
            InitializeComponent();
            ChangeContent(new LogIn(new TransitionControl(this)));
        }
        

        DoubleAnimation CreateDoubleAnimation(double from, double to, EventHandler completedEventHandler) 
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation(from, to, _animationDuration);
            if (completedEventHandler != null) 
                doubleAnimation.Completed += completedEventHandler;
            return doubleAnimation;
            
        }

        void SlideAnimation(UIElement newContent, UIElement oldContent, EventHandler completedEventHandler) 
        {
            double leftStart = Canvas.GetLeft(oldContent);
            Canvas.SetLeft(newContent, leftStart - Width);
            TransitionContainer.Children.Add(newContent);
            if (double.IsNaN(leftStart)) 
            {
                leftStart = 0;
            }
            DoubleAnimation outAnimation = CreateDoubleAnimation(leftStart, leftStart + Width, null);
            DoubleAnimation inAnimation = CreateDoubleAnimation(leftStart - Width, leftStart, completedEventHandler);
            oldContent.BeginAnimation(Canvas.LeftProperty, outAnimation);
            newContent.BeginAnimation(Canvas.LeftProperty, inAnimation);

        }

        public void ChangeContent(UIElement newContent)
        {
            if (TransitionContainer.Children.Count == 0) 
            {
                TransitionContainer.Children.Add(newContent);
                return;
            }
            if (TransitionContainer.Children.Count == 1)
            {
                TransitionContainer.IsHitTestVisible = false;
                UIElement oldContent = TransitionContainer.Children[0];
                EventHandler onAnimationCompletedHandler = delegate (object sender, EventArgs e)
                {
                    TransitionContainer.IsHitTestVisible = true;
                    TransitionContainer.Children.Remove(oldContent);

                    if (oldContent is IDisposable)
                        (oldContent as IDisposable).Dispose();

                    oldContent = null;

                };
                SlideAnimation(newContent, oldContent, onAnimationCompletedHandler);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Izlistaj.Window2 d = new Izlistaj.Window2();
            //d.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Izlistaj.Window2 d = new Izlistaj.Window2();
            //d.Show();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TransitionContainer.Width = e.NewSize.Width;
            TransitionContainer.Height = e.NewSize.Height;

            double xChange = 1, yChange = 1;

            if (e.PreviousSize.Width != 0)
            xChange = (e.NewSize.Width/e.PreviousSize.Width);

            if (e.PreviousSize.Height != 0)
            yChange = (e.NewSize.Height / e.PreviousSize.Height);

            foreach (FrameworkElement fe in TransitionContainer.Children )
            {   
                /*because I didn't want to resize the grid I'm having inside the canvas in this particular instance. (doing that from xaml) */            
                //if (fe is Grid == false)
                //{
                    fe.Height = fe.ActualHeight * yChange;
                    fe.Width = fe.ActualWidth * xChange;

                    Canvas.SetTop(fe, Canvas.GetTop(fe) * yChange);
                    Canvas.SetLeft(fe, Canvas.GetLeft(fe) * xChange);

                //}
            }
        }
    }
}
