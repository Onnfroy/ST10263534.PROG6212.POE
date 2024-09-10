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
            string hoursWorked = txtHoursWorked.Text;
            string hourlyRate = txtHourlyRate.Text;
            MessageBox.Show("Claim Submitted! \nHours Worked: " + hoursWorked + "\nHourly Rate: " + hourlyRate);
        }

        // Event handler for uploading a document
        private void UploadDocument_Click(object sender, RoutedEventArgs e)
        {
            lblDocumentStatus.Text = "Document uploaded successfully!";
            MessageBox.Show("Document Uploaded Successfully!");
        }

        // Event handler for checking the claim status
        private void CheckStatus_Click(object sender, RoutedEventArgs e)
        {
            string claimID = txtClaimID.Text;
            lblClaimStatus.Text = "Status for Claim ID " + claimID + ": Approved";
            MessageBox.Show("Checked Status for Claim ID " + claimID);
        }

        // Event handler for approving a claim
        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            string claimID = txtApproveClaimID.Text;
            MessageBox.Show("Claim ID " + claimID + " has been approved.");
        }

        // Event handler for rejecting a claim
        private void RejectClaim_Click(object sender, RoutedEventArgs e)
        {
            string claimID = txtApproveClaimID.Text;
            string remarks = txtRemarks.Text;
            MessageBox.Show("Claim ID " + claimID + " has been rejected. Remarks: " + remarks);
        }
    }
}