using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zähler
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Zählwerk zw = new Zählwerk(120);

        public MainWindow()
        {
            InitializeComponent();
            zw.Überlauf += OnÜberlauf;  // Event-Handler registrieren -> eigene Funktion im Observable Object

            buttonUp.Background = new System.Windows.Media.LinearGradientBrush(Colors.LightCyan, Colors.LightSkyBlue, 70.0);
            buttonDown.Background = new System.Windows.Media.LinearGradientBrush(Colors.LightCyan, Colors.LightSkyBlue, 70.0);
            lblZählerstand.Content = zw.Zählerstand.ToString();
        }

        private void OnÜberlauf()
        {
            lblZählerstand.Background = System.Windows.Media.Brushes.Pink;
            Console.WriteLine("Überlauf");
        }

        private void buttonUp_Click(object sender, RoutedEventArgs e)
        {
            lblZählerstand.Content = zw.Zähle(5).ToString();
        }

        private void buttonDown_Click(object sender, RoutedEventArgs e)
        {
            lblZählerstand.Content = zw.Zähle(-5).ToString();
        }

        private void buttonUp_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonUp.Background = new System.Windows.Media.LinearGradientBrush(Colors.LightCyan, Colors.LightSkyBlue, 10.0);
        }

        private void buttonUp_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonUp.Background = new System.Windows.Media.LinearGradientBrush(Colors.LightCyan, Colors.LightSkyBlue, 70.0);
        }

        private void buttonDown_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonDown.Background = new System.Windows.Media.LinearGradientBrush(Colors.LightCyan, Colors.LightSkyBlue, 70.0);
        }

        private void buttonDown_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonDown.Background = new System.Windows.Media.LinearGradientBrush(Colors.LightCyan, Colors.LightSkyBlue, 10.0);
        }
    }
}
