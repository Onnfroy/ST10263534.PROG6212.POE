using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace CMCSPrototype
{
    public partial class CheckClaimsPage : Page
    {
        private SqlConnection sqlConnection;
        private int lecturerID;

        // Constructor: Pass LecturerID
        public CheckClaimsPage(int lecturerID)
        {
            InitializeComponent();
            this.lecturerID = lecturerID; // Store lecturer ID
            InitializeDatabaseConnection(); // Set up the connection
            LoadClaims(); // Load claims for this specific lecturer
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

        // Load the claims for the current lecturer
        private void LoadClaims()
        {
            try
            {
                // Query: Select all claims for the current lecturer
                string query = "SELECT LecturerID, HoursWorked, HourlyRate, (HoursWorked * HourlyRate) AS TotalAmount, Status, Comments FROM Claims WHERE LecturerID = @LecturerID";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@LecturerID", lecturerID); // Use passed LecturerID

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Bind the data to the DataGrid
                claimsDataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading claims: " + ex.Message);
            }
        }

        // Navigation events for menu buttons
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage());
        }

        private void SubmitClaims_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CheckClaimsPage(lecturerID)); // Pass the current LecturerID
        }

        private void VerifyClaims_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new VerifyClaimsPage());
        }

        private void CheckClaims_Click(object sender, RoutedEventArgs e)
        {
            // Already on the Check Claims page, reload if needed
            LoadClaims(); // Reload claims
        }
    }
}