using System.Windows;

namespace CMCSPrototype
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Event handler for submitting a claim
        private void SubmitClaim_Click(object sender, RoutedEventArgs e)
        {
            // Capture the input for hours worked and hourly rate
            string hoursWorked = txtHoursWorked.Text;
            string hourlyRate = txtHourlyRate.Text;

            // For the prototype, just show a message that the claim was submitted
            MessageBox.Show("Claim Submitted! \nHours Worked: " + hoursWorked + "\nHourly Rate: " + hourlyRate);
        }

        // Event handler for uploading a document
        private void UploadDocument_Click(object sender, RoutedEventArgs e)
        {
            // For the prototype, just show a message indicating that the document was uploaded
            lblDocumentStatus.Text = "Document uploaded successfully!";
            MessageBox.Show("Document Uploaded Successfully!");
        }

        // Event handler for checking the claim status
        private void CheckStatus_Click(object sender, RoutedEventArgs e)
        {
            // Capture the Claim ID entered by the user
            string claimID = txtClaimID.Text;

            // For the prototype, show a generic message indicating the claim status
            lblClaimStatus.Text = "Status for Claim ID " + claimID + ": Approved";
            MessageBox.Show("Checked Status for Claim ID " + claimID);
        }
    }
}
