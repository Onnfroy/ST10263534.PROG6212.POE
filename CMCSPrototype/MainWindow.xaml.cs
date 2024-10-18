using System.Windows;

namespace CMCSPrototype
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Set the default page (home) when the application starts
            MainFrame.Content = new HomePage();
        }

        // Navigate to HomePage when 'Home' is clicked
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new HomePage();
        }

        // Navigate to SubmitClaimsPage when 'Submit Claims' is clicked
        private void SubmitClaims_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SubmitClaimsPage();
        }

        // Navigate to VerifyClaimsPage when 'Verify Claims' is clicked
        private void VerifyClaims_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new VerifyClaimsPage();
        }
    }
}