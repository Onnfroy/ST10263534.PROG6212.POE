using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;  // For opening the document
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;  // For Navigation

namespace CMCSPrototype
{
    public partial class VerifyClaimsPage : Page
    {
        private SqlConnection sqlConnection;

        public VerifyClaimsPage()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            LoadClaims();  // Load pending claims when the page is initialized
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

        // Load pending claims from the database
        public DataTable LoadClaims()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string query = "SELECT ClaimID, LecturerID, HoursWorked, HourlyRate, Status, DocumentPath FROM Claims WHERE Status = 'Pending'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading claims: " + ex.Message);
            }

            return dataTable;  // Return the DataTable
        }   
   

        // Approve claim event handler
        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = claimsDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                string claimID = selectedRow["ClaimID"].ToString();
                UpdateClaimStatus(claimID, "Approved");
            }
        }

        // Reject claim event handler
        private void RejectClaim_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = claimsDataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                string claimID = selectedRow["ClaimID"].ToString();
                UpdateClaimStatus(claimID, "Rejected");
            }
        }

        // Update claim status in the database
        private void UpdateClaimStatus(string claimID, string status)
        {
            try
            {
                string updateQuery = "UPDATE Claims SET Status = @Status WHERE ClaimID = @ClaimID";
                SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                updateCommand.Parameters.AddWithValue("@Status", status);
                updateCommand.Parameters.AddWithValue("@ClaimID", claimID);
                updateCommand.ExecuteNonQuery();

                // Refresh the DataGrid after updating the status
                LoadClaims();
                MessageBox.Show("Claim ID " + claimID + " has been " + status.ToLower() + ".");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating claim status: " + ex.Message);
            }
        }

        // Event handler to open the document
        private void Document_Click(object sender, MouseButtonEventArgs e)
        {
            TextBlock clickedTextBlock = sender as TextBlock;
            if (clickedTextBlock != null)
            {
                string documentPath = clickedTextBlock.Text;
                try
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = documentPath,
                        UseShellExecute = true // Opens the file with the default associated program
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not open document: " + ex.Message);
                }
            }
        }

        // Navigation event handlers
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage());
        }

        private void SubmitClaims_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SubmitClaimsPage());
        }

        private void VerifyClaims_Click(object sender, RoutedEventArgs e)
        {
            // Already on VerifyClaimsPage, no need to navigate
        }
    }
}