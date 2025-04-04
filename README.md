# AiraloExercise

## IDE Used
- **Primary**: Visual Studio Community 2022
- **Alternative IDEs**: Any IDE supporting C# (e.g., VS Code, JetBrains Rider, etc.), though they may require additional setup steps and package installations.

## Tools Used
- **C# / .NET**: Main programming language and framework
- **RestSharp**: For making API calls and handling HTTP requests
- **NUnit**: Test framework for running unit and integration tests
- **Selenium WebDriver**: For performing automated UI tests

---

## Initial Set-Up

To get started with the project, follow these steps:

1. **Clone the Repository**:
    Clone the GitHub repository to your local machine using the following link:
    ```bash
    https://github.com/bpopov1914/AiraloExercise.git
    ```

2. **Set Environment Variables**:
    Set `CLIENT_ID` and `CLIENT_SECRET` as **Environment Variables** for secure storage of authentication credentials.
    - **Windows Setup**:
      1. Search for "Edit the system environment variables" in the Windows menu and open it.
      2. Click on the **Environment Variables** button.
      3. Under **System Variables**, click **New**.
      4. Enter the **name** (e.g., `CLIENT_ID`) and **value** (your actual ID).
      5. Repeat for the `CLIENT_SECRET` variable.
      6. **Restart Visual Studio** to ensure the environment variables are loaded correctly.

    - **Note**: This process may differ slightly on Linux or macOS. Ensure you follow appropriate steps for your OS.

3. **Update Chrome Browser**:
    Ensure that **Google Chrome** on your local machine is updated to the latest version.
    - The current version of **Selenium.WebDriver.ChromeDriver** used in the project is `135.0.7049.4200`. It will **not** work with older Chrome versions.

---

## Project Structure

The project is divided into two main folders:

1. **ApiTests**: Contains the API-related tests.
   - **Test Class**: `PlaceOrderApiTest.cs` contains the actual API tests.
   - **OrderResponseModels**: Models for parsing the response of the Place Order API.
   - **SimListResponseModels**: Models for parsing the response of the Get Sim List API.
   - **Utils**:
     - `RestCalls.cs`: Logic for making API calls.
     - `ResponseDataExtractors.cs`: Contains a method for extracting the token from the Authentication endpoint response.

2. **UiTests**: Contains the UI-related tests using Selenium.
   - **Test Class**: `JapanPackageUiTest.cs` contains the UI tests.
   - **Utils**:
     - `BrowserActions.cs`: Logic for browser actions.
     - `ScreenshotHelper.cs`: Helper class for taking screenshots during tests.

---

## Test Execution

You can run the tests using either **Visual Studio Test Explorer** or via the **command line**.

### Running Tests in Visual Studio

1. Open the **Test Explorer**:
    - Go to **Test > Test Explorer** from the menu.

2. **Running Tests**:
    - Click the **Run** button (green arrow) next to any test to run it.
    - Right-click on a test to **Run** or **Debug** it.
    - To run all tests, click **Run All Tests in View**.

### Running Tests via Command Line

1. **Navigate to Project Folder**:
    Open your terminal or command prompt and navigate to the project folder:
    ```bash
    cd C:\Path\To\AiraloExercise
    ```

2. **Run All Tests**:
    Run the following command to execute all tests:
    ```bash
    dotnet test
    ```

3. **Run Specific Tests**:
    You can use the `--filter` option to run specific test methods or classes:
    - Run a specific test class:
      ```bash
      dotnet test --filter "AiraloExercise.ApiTests.PlaceOrderApiTest"
      ```
    - Run a specific test method:
      ```bash
      dotnet test --filter "AiraloExercise.ApiTests.PlaceOrderApiTest.PlaceOrder"
      ```

---

## Tests Logic

### PlaceOrderApiTest

- **[OneTimeSetUp]**: 
  - Authenticates with the `POST /v2/token` endpoint.
  - Extracts the token from the response and stores it for future API calls.
  - Verifies that the token was successfully extracted.

- **PlaceOrder Test**:
  - Calls the `POST /v2/orders` endpoint.
  - Asserts that the response is successful and that order details match the expected values.

- **GetListOfSimCards Test**:
  - Calls the `GET /v2/sims` endpoint.
  - Asserts that the response is successful and validates the SIM card details against expected values.
  - Tests are executed in the following order to ensure data dependency between them.:
    1. **PlaceOrder**
    2. **GetListOfSimCards**
  - If **GetListOfSimCards** is executed on it's own, or before **PlaceOrder**, it will fail, as it relies on data from **PlaceOrder**

### JapanPackageUiTest

- **[SetUp]**:
  - Initializes the Chrome driver and ensures it's running properly.

- **JapanPackage Test**:
  - Initializes an expected package details dictionary.
  - Performs the following actions:
    - Navigates to the Airalo website.
    - Searches for Japan country in the search box and selecting Japan from the suggested options.
    - Changes the currency from the Euro default one to US Dollars.
    - Selecting the first package from the Japan packages
	- Getting all the details of the package
	- Assertions are performed to ensure that the extracted details match the expected values.

- **[TearDown]**:
  - Ensures the driver session is closed properly after each test.

---

## Screenshot Helper

- **Logic**:
  - Screenshots are automatically named using the current date and time to ensure uniqueness.
  - Screenshots are stored in the **bin** directory (e.g., `C:\Users\{UserName}\source\repos\AiraloExercise\AiraloExercise\bin\Debug\net8.0`).
  - Primarily used for debugging purposes during UI tests.

---