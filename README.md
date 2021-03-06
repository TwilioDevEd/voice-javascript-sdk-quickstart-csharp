<a href="https://www.twilio.com">
  <img src="https://static0.twilio.com/marketing/bundles/marketing/img/logos/wordmark-red.svg" alt="Twilio" width="250" />
</a>

# Twilio Voice JavaScript SDK Quickstart for C#

> This template is part of Twilio CodeExchange. If you encounter any issues with this code, please open an issue at [github.com/twilio-labs/code-exchange/issues](https://github.com/twilio-labs/code-exchange/issues).

## About

This application should give you a ready-made starting point for writing your own voice apps with Twilio Voice JavaScript SDK 2.0 (Formerly known as Twilio Client). 

This application is built in ASP.NET Core 3.1.

Implementations in other languages:

| Node.js        | Java        | Python                                                                        | PHP         | Ruby        |
| :---------- | :---------- | :---------------------------------------------------------------------------- | :---------- | :---------- |
| [Done](https://github.com/TwilioDevEd/voice-javascript-sdk-quickstart-node) | [Done](https://github.com/TwilioDevEd/voice-javascript-sdk-quickstart-java)| [Done](https://github.com/TwilioDevEd/voice-javascript-sdk-quickstart-python) | [Done](https://github.com/TwilioDevEd/voice-javascript-sdk-quickstart-php) | [Done](https://github.com/TwilioDevEd/voice-javascript-sdk-quickstart-ruby) |

## Set Up

### Requirements

- [Visual Studio 2019/Visual Studio for Mac 2019](https://visualstudio.microsoft.com/)
- [.NET Core 3.1 SDK with ASP.NET Core Runtime 3.1](https://dotnet.microsoft.com/download)
- [ngrok](https://ngrok.com/download) - this is used to expose your local development server to the internet. For more information, read [this Twilio blog post](https://www.twilio.com/blog/2015/09/6-awesome-reasons-to-use-ngrok-when-testing-webhooks.html).
- A WebRTC enabled browser (Google Chrome or Mozilla Firefox are recommended). Edge and Internet Explorer will not work for testing.

### Create a TwiML Application, Purchase a Phone Number, Create an API Key

1. [Create a TwiML Application in the Twilio Console](https://www.twilio.com/console/voice/twiml/apps). Once you create the TwiML Application, click on it in your list of TwiML Apps to find the TwiML App SID. You will save this SID later to your `user-secrets`. **Note:** You will also need to configure the Voice "REQUEST URL" in your TwiML App later.
   - For detailed instructions with screenshots, see the [Create a TwiML App.md file](ConsoleHowTos/CreateNewTwiMLApp/CreateNewTwiMLApp.md)
1. [Purchase a Voice phone number](https://www.twilio.com/console/phone-numbers/incoming). You will need this phone number in [E.164 format](https://en.wikipedia.org/wiki/E.164) for your `user-secrets`.
   - For detailed instructions with screenshots, see the [Buy a Phone Number.md file](ConsoleHowTos/BuyVoicePhoneNumber/BuyVoicePhoneNumber.md)
1. [Create an API Key in the Twilio Console](https://www.twilio.com/console/project/api-keys). Keep the API Key SID and the API Secret in a safe place, since you will need them for your `user-secrets`. Your API KEY is needed to create an [Access Token](https://www.twilio.com/docs/iam/access-tokens).

   - For detailed instructions with screenshots, see the [Create an API Key.md file](ConsoleHowTos/CreateAPIKey/CreateAPIKey.md)

### Gather Config Values for your User Secrets

Before we begin local development, we need to collect all the config values we need to run the application. These values will be used in Step 2 below by saving them to your .NET User Secrets.

| Config Value                           | Description                                                                                                                                                              |
| :------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `AccountSid`                           | Your primary Twilio account identifier - find this [in the Twilio Console here](https://www.twilio.com/console).                                                                |
| `AuthToken`                            | Your Twilio Account Auth Token - find this [in the Twilio Console below your Account SID](https://www.twilio.com/console)                                                       |
| `TwimlAppSid`                          | The SID of the TwiML App you created in step 1 above. Find the SID [in the Twilio Console here](https://www.twilio.com/console/voice/twiml/apps).                               |
| `CallerId`                             | Your Twilio phone number in [E.164 format](https://en.wikipedia.org/wiki/E.164) - you can [find your number here](https://www.twilio.com/console/phone-numbers/incoming) |
| `ApiSid` / `ApiSecret`                 | The `ApiSid` is the API Key SID you created in step 3 above, and the `ApiSecret` is the secret associated with that key.                                                 |

### Local development

1. First clone this repository and cd into it:
   ```bash
   git clone https://github.com/TwilioDevEd/voice-javascript-sdk-quickstart-csharp.git
   cd voice-javascript-sdk-quickstart-csharp
   ```
1. Enable [DotNet User Secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows). 
    First, navigate to the `Quickstart` directory:
    ```bash
    cd Quickstart
    ```

    Run the following command to use User Secrets in your application:

    ```bash
    dotnet user-secrets init
    ```

    If you open the `Quickstart.csproj` file, you should now see something like the following:

    ```xml
    <PropertyGroup>
        <TargetFramework>netcoreapp3.1</TargetFramework>
        <UserSecretsId>some-guid-here</UserSecretsId>
    </PropertyGroup>
    ```
    Next, we'll save our configuration values using User Secrets. 

    Run each of the following commands in your terminal, making sure you've replaced the BOILERPLATE_TEXT with your actual configuration values we gathered earlier.

    ```bash
    dotnet user-secrets set TwilioAccountDetails:AccountSid YOUR_ACCOUNT_SID
    ```
    ```bash
    dotnet user-secrets set TwilioAccountDetails:AuthToken YOUR_AUTH_TOKEN
    ```
    ```bash
    dotnet user-secrets set TwilioAccountDetails:TwimlAppSid YOUR_TWIML_APP_SID
    ```
    ```bash
    dotnet user-secrets set TwilioAccountDetails:CallerId YOUR_CALLERID
    ```
    ```bash
    dotnet user-secrets set TwilioAccountDetails:ApiSid YOUR_API_SID
    ```    
    ```bash
    dotnet user-secrets set TwilioAccountDetails:ApiSecret YOUR_API_SECRET
    ```

    To check that all of your secrets have been saved properly, run the following command:

    ```bash
    dotnet user-secrets list
    ``` 

    To learn more about User Secrets, check out the Twilio Blog post called ["User Secrets in a .NET Core Web App"](https://www.twilio.com/blog/2018/05/user-secrets-in-a-net-core-web-app.html). 
1. Run the Project. A new browser tab should open [http://localhost:5000](http://localhost:5000).
1. Expose your application to the wider internet using `ngrok`. This step is **crucial** for the app to work as expected.
   ```bash
   ngrok http 5000
   ```
1. `ngrok` will assign a unique URL to your tunnel.
   It might be something like `https://asdf456.ngrok.io`. You will need this to configure your TwiML app in the next step.
1. Configure your TwiML app

   - In the Twilio Console, navigate to [Programmable Voice > TwiML > TwiML Apps](https://www.twilio.com/console/voice/twiml/apps)
   - Select the TwiML App you created earlier
   - On your TwiML App's information page, find the 'Voice Configuration' section.
   - Change the Request URL to your ngrok url with `/voice` appended to the end. (E.g: `https://asdf456.ngrok.io/voice`) **Note:** You **must** use the https URL, otherwise some browsers will block
     microphone access.
   - Click the 'Save' button.

   ![screenshot of TwiML App Voice Configuration](./screenshots/UpdateRequestURL.png)

You should now be ready to make and receive calls from your browser.

## Your Web Application

When you navigate to `localhost:5000`, you should see the web application containing a 'Start up the Device' button. Click this button to initialize a `Twilio.Device`.

![screenshot of web app home page](./screenshots/InitializeDevice.png)

When the `Twilio.Device` is initialized, you will be assigned a random "client name", which will appear in the 'Device Info' column on the left side of the page. This client name is used as the `identity` field when generating an Access Token for the `Twilio.Device`, and is also used to route SDK-to-SDK calls to the correct `Twilio.Device`.

### To make an outbound call to a phone number:

- Under 'Make a Call', enter a phone number in [E.164 format](https://en.wikipedia.org/wiki/E.164) and press the 'Call' button

### To make a browser-to browser call:

Open two browser windows to `localhost:5000` and click 'Start up the Device' button in both windows. You should see a different client name in each window.

Enter one client's name in the other client's 'Make a Call' input and press the 'Call' button.

![screenshot of browser to browser call](./screenshots/BrowserToBrowserCall.png)

### Receiving Incoming Calls from a Non-Browser Device

You will first need to configure your Twilio Voice Phone Number to use the TwiML App we created earlier. This tells Twilio how to handle an incoming call directed to your Twilio Voice Number.

1.  Log in to your [Twilio Console](https://www.twilio.com/console)
1.  Navigate to your [Active Numbers list](https://www.twilio.com/console/phone-numbers/incoming)
1.  Click on the number you purchased earlier
1.  Scroll down to find the 'Voice & Fax' section and look for 'CONFIGURE WITH'
1.  Select 'TwiML' App
1.  Under 'TWIML APP', choose the TwiML App you created earlier.
1.  Click the 'Save' button at the bottom of the browser window.

![screenshot of phone number configuration](./screenshots/ConfigurePhoneNumberWithTwiMLApp.png)

You can now call your Twilio Voice Phone Number from your cell or landline phone.

**Note:** Since this is a quickstart with limited functionality, incoming calls will only be routed to your most recently-created `Twilio.Device`.

### Unknown Audio Devices

If you see "Unknown Audio Output Device 1" in the "Ringtone" or "Speaker" devices lists, click the button below the boxes (Seeing "Unknown" Devices?) to have your browser identify your input and output devices.

## Resources

- The CodeExchange repository can be found [here](https://github.com/twilio-labs/code-exchange/).

## Contributing

This template is open source and welcomes contributions. All contributions are subject to our [Code of Conduct](https://github.com/twilio-labs/.github/blob/master/CODE_OF_CONDUCT.md).

## License

[MIT](http://www.opensource.org/licenses/mit-license.html)

## Disclaimer

No warranty expressed or implied. Software is as is.

[twilio]: https://www.twilio.com
