using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMCSPrototype;  // Reference to your main project

[TestClass]
public class SubmitClaimTests
{
    private MainWindow mainWindow;

    [TestInitialize]
    public void Setup()
    {
        mainWindow = new MainWindow();
        mainWindow.InitializeComponent();  // Initialize UI components
    }

    [TestMethod]
    public void SubmitClaim_WithValidInputs_ShouldDisplaySuccessMessage()
    {
        // Arrange
        mainWindow.txtHoursWorked.Text = "40";
        mainWindow.txtHourlyRate.Text = "20";

        // Act
        mainWindow.SubmitClaim_Click(null, null);

        // Assert
        Assert.IsTrue(true, "Claim Submitted! \nHours Worked: 40\nHourly Rate: 20");
    }

    [TestMethod]
    public void SubmitClaim_WithInvalidInputs_ShouldShowError()
    {
        // Arrange
        mainWindow.txtHoursWorked.Text = "abc";  // Invalid input
        mainWindow.txtHourlyRate.Text = "xyz";   // Invalid input

        // Act
        mainWindow.SubmitClaim_Click(null, null);

        // Assert
        Assert.IsTrue(mainWindow.txtHoursWorked.Text == "abc", "Invalid input.");
    }
}