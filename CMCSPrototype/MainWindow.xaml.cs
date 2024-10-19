using System;
using System.Windows;
using System.Windows.Controls;

namespace CMCSPrototype
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Event handler for Home menu item
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the HomePage within the MainFrame
            MainFrame.Navigate(new HomePage());
        }

        // Event handler for Submit Claims menu item
        private void SubmitClaims_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the SubmitClaimsPage within the MainFrame
            MainFrame.Navigate(new SubmitClaimsPage());
        }

        // Event handler for Verify Claims menu item
        private void VerifyClaims_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the VerifyClaimsPage within the MainFrame
            MainFrame.Navigate(new VerifyClaimsPage());
        }

        // Event handler for Check Claims menu item
        private void CheckClaims_Click(object sender, RoutedEventArgs e)
        {
            // Replace this with the actual lecturer ID you want to use
            int lecturerID = 1; // Replace with dynamic value if needed
            // Navigate to the CheckClaimsPage and pass the LecturerID within the MainFrame
            MainFrame.Navigate(new CheckClaimsPage(lecturerID));
        }
    }
}