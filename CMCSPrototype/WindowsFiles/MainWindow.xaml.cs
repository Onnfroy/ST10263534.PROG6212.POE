using System;
using System.Data.SqlClient; // Use SqlClient for SQL Server
using System.Windows;

namespace CMCSPrototype
{
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabase(); // Initialize the database when the application starts
        }

        // Initialize SQL Server (LocalDB) Database
        private void InitializeDatabase()
        {
            // Connection string for LocalDB (SQL Server)
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=CMCSDatabase;Integrated Security=true;";
            sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();

                // Create the Claims table if it doesn't already exist
                string createTableQuery = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Claims' AND xtype='U')
                                            CREATE TABLE Claims (
                                                ClaimID INT IDENTITY(1,1) PRIMARY KEY,
                                                HoursWorked FLOAT NOT NULL,
                                                HourlyRate FLOAT NOT NULL,
                                                Status NVARCHAR(50) DEFAULT 'Pending'
                                            );";
                SqlCommand createTableCmd = new SqlCommand(createTableQuery, sqlConnection);
                createTableCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing database: " + ex.Message);
            }
        }

        // Event handler for submitting a claim
        private void SubmitClaim_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (ValidateInput())
            {
                string hoursWorked = txtHoursWorked.Text;
                string hourlyRate = txtHourlyRate.Text;

                try
                {
                    // Insert the claim into the database
                    string insertQuery = "INSERT INTO Claims (HoursWorked, HourlyRate, Status) VALUES (@HoursWorked, @HourlyRate, 'Pending');";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, sqlConnection);
                    insertCmd.Parameters.AddWithValue("@HoursWorked", Convert.ToDouble(hoursWorked));
                    insertCmd.Parameters.AddWithValue("@HourlyRate", Convert.ToDouble(hourlyRate));
                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("Claim Submitted! \nHours Worked: " + hoursWorked + "\nHourly Rate: " + hourlyRate);

                    // Clear the input fields after submission
                    txtHoursWorked.Clear();
                    txtHourlyRate.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error submitting claim: " + ex.Message);
                }
            }
        }

        // Input validation to ensure numeric values for hours worked and hourly rate
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoursWorked.Text) || string.IsNullOrWhiteSpace(txtHourlyRate.Text))
            {
                MessageBox.Show("Please enter both Hours Worked and Hourly Rate.");
                return false;
            }

            if (!double.TryParse(txtHoursWorked.Text, out _) || !double.TryParse(txtHourlyRate.Text, out _))
            {
                MessageBox.Show("Please enter valid numeric values for Hours Worked and Hourly Rate.");
                return false;
            }

            return true;
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
            string selectQuery = "SELECT Status FROM Claims WHERE ClaimID = @ClaimID";
            try
            {
                SqlCommand selectCmd = new SqlCommand(selectQuery, sqlConnection);
                selectCmd.Parameters.AddWithValue("@ClaimID", Convert.ToInt32(claimID));
                var status = selectCmd.ExecuteScalar();
                if (status != null)
                {
                    lblClaimStatus.Text = "Status for Claim ID " + claimID + ": " + status.ToString();
                }
                else
                {
                    lblClaimStatus.Text = "Claim ID not found.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking claim status: " + ex.Message);
            }
        }

        // Event handler for approving a claim
        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            string claimID = txtApproveClaimID.Text;
            string updateQuery = "UPDATE Claims SET Status = 'Approved' WHERE ClaimID = @ClaimID";

            try
            {
                SqlCommand updateCmd = new SqlCommand(updateQuery, sqlConnection);
                updateCmd.Parameters.AddWithValue("@ClaimID", Convert.ToInt32(claimID));
                int rowsAffected = updateCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Claim ID " + claimID + " has been approved.");
                }
                else
                {
                    MessageBox.Show("Claim ID not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error approving claim: " + ex.Message);
            }
        }

        // Event handler for rejecting a claim
        private void RejectClaim_Click(object sender, RoutedEventArgs e)
        {
            string claimID = txtApproveClaimID.Text;
            string remarks = txtRemarks.Text;
            string updateQuery = "UPDATE Claims SET Status = 'Rejected', Remarks = @Remarks WHERE ClaimID = @ClaimID";

            try
            {
                SqlCommand updateCmd = new SqlCommand(updateQuery, sqlConnection);
                updateCmd.Parameters.AddWithValue("@ClaimID", Convert.ToInt32(claimID));
                updateCmd.Parameters.AddWithValue("@Remarks", remarks);
                int rowsAffected = updateCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Claim ID " + claimID + " has been rejected. Remarks: " + remarks);
                }
                else
                {
                    MessageBox.Show("Claim ID not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error rejecting claim: " + ex.Message);
            }
        }
    }
}