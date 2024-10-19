using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using CMCSPrototype;
using System.Windows.Controls;

namespace CMCSPrototype.Tests
{
    [TestClass]
    public class ClaimsTests
    {
        private SqlConnection? sqlConnection;  // Allow nullability

        [TestInitialize]
        public void TestInitialize()
        {
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=CMCSDatabase;Integrated Security=true;";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (sqlConnection != null)
            {
                sqlConnection.Close();
            }
        }

        [TestMethod]
        public void TestLoadClaims_ReturnsData()
        {
            VerifyClaimsPage verifyClaimsPage = new VerifyClaimsPage();
            var claimsData = verifyClaimsPage.LoadClaims();  // Now this will return DataTable

            Assert.IsNotNull(claimsData, "Claims data should not be null.");
            Assert.IsTrue(claimsData.Rows.Count > 0, "Claims data should not be empty.");
        }
    }
}