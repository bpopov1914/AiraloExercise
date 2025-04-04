# AiraloExercise

IDE used:
 - Visual Studio Community 2022 
 - Alternatively other IDEs that support C# can be used (Such as: VS Code, JetBeans Rider, etc.). However, those might require some additional set up steps and packages installations.

Tools used:
 - C#/.Net
 - RestSharp (For API tests)
 - NUnit (Test framework)
 - Selenium Web Driver (For UI tests)

Initial Set Up:
 - Clone the GitHub repository to your local machine using this link: https://github.com/bpopov1914/AiraloExercise.git
 - Set "CLIENT_ID" and "CLIENT_SECRET" variables as Environment Variables, so that they are stored in a more secure way. (See "Environment Variables" section below for exact steps)
 - Make sure  the Chrome browser on your local machine is updated to the latest version. The current version of "Selenium.WebDriver.ChromeDriver" installed is Version="135.0.7049.4200" and it will not work with older Chrome browser versions.

Environment Variables:
 - In the Windows menu, search for "Edit the system environment variables" and open it
 - Click on the "Environment Variables ..." button in the bottom right corner of the modal
 - On the new modal, under "System Variables" click on the "New" button
 - Add name and value of the variable and click "OK". Note, the values should be the actual id's that need to be used for authentication through the API.
 - Repeat the above two steps for the two variables: "CLIENT_ID" and "CLIENT_SECRET"
 - Important: You need to restart Visual Studio in order for the IDE to get the newly created Environment Variables.
 - Note: Those are the steps to set Environment Variables on Windows. For Linux, MacOs and/or other OS, those steps might differ.

Project Structure:
 - The files are split in two main folders called "ApiTests" and "UiTests".
 - "ApiTests":
	- Test class "PlaceOrderApiTest.cs", that contains the actual API tests. (See "Tests logic" section below for more details about the tests)
	- Folder "OrderResponseModels" containing the models used for parsing the response of the Place Order endpoint.
	- Folder "SimListResponseModels" containing the models used for parsing the response of the Get Sim List endpoint.
	- Folder "Utils" containing two classes: "RestCalls.cs", where the logic for making the api calls is written, and "ResponseDataExtractors.cs" containing a method for extracting the token from the Authentication endpoint response.
 - "UiTests":
	- Test class "JapanPackageUiTest.cs", that contains the actual UI tests. (See "Tests logic" section below for more details about the tests)
	- Folder "Utils" containing two classes: "BrowserActions.cs" containing the logic for each browser action used by the tests, and "ScreenshotHelper.cs" containing the logic for making screenshots by the tests.

Test Execution:
 - Running the tests using Visual Studio Test Explorer:
	- The VS Test Explorer can be opened from the header menu -> "Test" -> "Test Explorer"
	- Inside the Test Explorer all tests located by the build are listed
	- Each one can be executed separately by clicking on the "Run" green arrow at the top, or mouse right-click adn click on "Run" green arrow
	- Each test can be executed in debug mode with mouse right-click adn click on "Debug" option
	- All tests can be executed simultainiously by clicking on the "Run All Tests in View" green arrow at the top
 - Run Tests with the "dotnet test" Command:
	- Using "Cmd" or "Terminal" navigate to the Project Folder (e.g. cd C:\Users\{UserName}\source\repos\AiraloExercise\AiraloExercise\)
	- Once you are in the test project folder, you can run the tests using the following command: "dotnet test"
	- To run a specific test method or class, you can use the "--filter" option.
		- E.g. to run the tests from a specific class: "dotnet test --filter "AiraloExercise.ApiTests.PlaceOrderApiTest""
		- E.g. to run only a specific test method inside a class: "dotnet test --filter "AiraloExercise.ApiTests.PlaceOrderApiTest.PlaceOrder""
	
Tests logic:
 - "PlaceOrderApiTest":
	- The tests start with [OneTimeSetUp] NUnit annotation, meaning the actions inside will be performed once before executing all tests.
	- OneTimeSetUp:
		- authenticating with the POST /v2/token endpoint
		- extracting the token from the response
		- saving it in a variable, so it can be used for the other API calls
		- doing an Assertion to verify if the token has been extracted successfully
	- "PlaceOrder" test:
		- Calling the POST /v2/orders endpoint and Asserting if the call was successful
		- Getting the order and sim details from the response
		- Asserting that all the details from the response match the expected
	- "GetListOfSimCards" test:
		- Calling the GET /v2/sims endpoint and Asserting if the call was successful
		- Getting the list of sims from the response and based on the description, adding the ones that need to be verified to a new List of sim cards called "simsToVerify"
		- Asserting that all the details from the response match the expected
	- Note: The tests are ordered to be executed as follows: The "PlaceOrder" test first and then the "GetListOfSimCards" test. This is done, because the "GetListOfSimCards" test relys on the data from the "PlaceOrder" test. They are ordered using the [Order(n)] NUnit annotation, where "n" is a number (e.g. [Order(1)])

 - "JapanPackageUiTest":
	- The tests start with [SetUp] NUnit annotation, meaning the actions inside will be performed once before executing each test.
	- Set Up:
		- Initializing Chrome driver with options
		- Try/Catch block to assure the driver is up and running before continuing
	- "JapanPackage" test:
		- Initializing "Dictionary<string, string> expectedPackageDetails" that hold all the expected details of the package
		- Block of code ensuring the browser is opened before attempting any actions on it: "if (driver.WindowHandles.Any())"
		- Then the following actions are performed:
			- Navigating to the Airalo website
			- Search for "Japan" country in the search box and selecting "Japan" from the suggested options
			- Changing the currency from the Euro default one to US Dollars, which is needed for the test
			- Selecting the first package from the Japan packages
			- Getting all the details of the package
			- Asserting each package detail is as expected
	- Tear Down:
		- [TearDown] NUnit annotation, meaning after each test is done, the actions inside of it will be performed
		- Quiting the driver in the Tear Down, to ensure the driver session is closed properly
		
Screenshot Helper:
 - Logic current set up:
	- Screenshot names are automatically generated using a Current Date and Time timestamp, ensuring uniqueness
	- Screenshots are stored in the current working dirrectory (e.g. C:\Users\{UserName}\source\repos\AiraloExercise\AiraloExercise\bin\Debug\net8.0)
	- Screenshots are used for debuging puproses mainly