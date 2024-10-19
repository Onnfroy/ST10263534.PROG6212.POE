using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using CMCSPrototype;
using System.Windows.Controls;

namespace CMCSPrototype.Tests
{
    [TestClass]
    public class ClaimsTests
    {
        // SqlConnection is declared nullable to handle possible null values
        private SqlConnection? sqlConnection;

        // This method sets up the test environment by initializing the database connection
        // What should have happened: A connection to the CMCSDatabase should have been successfully opened
        [TestInitialize]
        public void TestInitialize()
        {
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=CMCSDatabase;Integrated Security=true;";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        // This method cleans up after each test by closing the database connection
        // What should have happened: The connection should be closed after each test to prevent resource leaks
        [TestCleanup]
        public void TestCleanup()
        {
            if (sqlConnection != null)
            {
                sqlConnection.Close();
            }
        }

        // This test method is intended to check if the 'LoadClaims' method from VerifyClaimsPage
        // successfully loads data from the database
        // What should have happened: The 'LoadClaims' method should return a DataTable containing claims data
        [TestMethod]
        public void TestLoadClaims_ReturnsData()
        {
            // VerifyClaimsPage should be initialized for the test
            VerifyClaimsPage verifyClaimsPage = new VerifyClaimsPage();

            // LoadClaims is expected to return a DataTable containing claims information
            var claimsData = verifyClaimsPage.LoadClaims();  // Now this should return DataTable

            // This assertion checks if the claimsData object is not null
            // Expected: claimsData should not be null if the database connection and retrieval were successful
            Assert.IsNotNull(claimsData, "Claims data should not be null.");

            // This assertion checks if the DataTable has rows (i.e., claims are present)
            // Expected: The DataTable should contain at least one row of data
            Assert.IsTrue(claimsData.Rows.Count > 0, "Claims data should not be empty.");
        }
    }
}