using System.Windows;
using System.Windows.Controls;

namespace CMCSPrototype
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to HomePage (this is already HomePage)
        }

        private void SubmitClaims_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to SubmitClaimsPage
            NavigationService.Navigate(new SubmitClaimsPage());
        }

        private void VerifyClaims_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to VerifyClaimsPage
            NavigationService.Navigate(new VerifyClaimsPage());
        }
    }
}