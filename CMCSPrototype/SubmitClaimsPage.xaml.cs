using Microsoft.Win32; // For OpenFileDialog
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace CMCSPrototype
{
    public partial class SubmitClaimsPage : Page
    {
        private SqlConnection sqlConnection;
        private string uploadedFilePath;  // Store the path of the uploaded document

        public SubmitClaimsPage()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        // Initialize the database connection
        private void InitializeDatabaseConnection()
        {
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=CMCSDatabase;Integrated Security=true;";
            sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to database: " + ex.Message);
            }
        }

        // Event handler for choosing a file to upload
        private void UploadDocument_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";  // Set file filter, can change to specific types if needed
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFilePath = openFileDialog.FileName;
                lblUploadStatus.Text = "Document uploaded: " + uploadedFilePath;
            }
        }

        // Event handler for submitting the claim
        private void SubmitClaim_Click(object sender, RoutedEventArgs e)
        {
            string lecturerID = txtLecturerID.Text;
            string hoursWorked = txtHoursWorked.Text;
            string hourlyRate = txtHourlyRate.Text;

            if (string.IsNullOrWhiteSpace(lecturerID) || string.IsNullOrWhiteSpace(hoursWorked) ||
                string.IsNullOrWhiteSpace(hourlyRate) || string.IsNullOrWhiteSpace(uploadedFilePath))
            {
                MessageBox.Show("Please fill out all fields and upload a supporting document.");
                return;
            }

            try
            {
                string query = "INSERT INTO Claims (LecturerID, HoursWorked, HourlyRate, DocumentPath, Status) VALUES (@LecturerID, @HoursWorked, @HourlyRate, @DocumentPath, 'Pending')";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@LecturerID", lecturerID);
                sqlCommand.Parameters.AddWithValue("@HoursWorked", Convert.ToDouble(hoursWorked));
                sqlCommand.Parameters.AddWithValue("@HourlyRate", Convert.ToDouble(hourlyRate));
                sqlCommand.Parameters.AddWithValue("@DocumentPath", uploadedFilePath);

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Claim Submitted Successfully!");

                txtLecturerID.Clear();
                txtHoursWorked.Clear();
                txtHourlyRate.Clear();
                lblUploadStatus.Text = "No document uploaded yet.";
                uploadedFilePath = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting claim: " + ex.Message);
            }
        }

        // Navigation button event handler for "Submit Claims"
        private void SubmitClaims_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SubmitClaimsPage());
        }

        // Navigation button event handler for "Verify Claims"
        private void VerifyClaims_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new VerifyClaimsPage());
        }
    }
}