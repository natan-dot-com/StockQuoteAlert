# Stock Quote Alert

## Introduction

A simple stock quote alert that notifies the user via E-mail when the price of a stock is under/above certain thresholds. It uses an HTTP server to fetch the stock data in real time from [Brapi](https://brapi.dev/), 
also using an SMTP server to communicate with the user via E-mail.

## Execution

Since this project was made entirely on Visual Studio (this means it contains several Visual Studio files), the best (and recommended way) to run this application is to open it on 
Visual Studio and to run it from there. This application requires three mandatory arguments to run:

```
StockQuoteAlert.exe {TICKER_SYMBOL} {LOWERBOUND_PRICE} {UPPERBOUND_PRICE}
```

More specifically:
- `TICKER_SYMBOL`: Specifies which stock is going to be monitored
- `LOWERBOUND_PRICE`: If the stock price falls under this value, a notification e-mail will be sent.
- `UPPERBOUND_PRICE`: If the stock price goes above this value, a notification e-mail will be sent.

## Configuration

For this application to work, it requires a JSON configuration file, `appconfig.json`, with the following structure to specify the configuration:

```json
{
  "api": {
    "key": "{brapi_api_secret}"
  },
  "email": {
    "host": "{smtp_server_host}",
    "username": "{smtp_server_username}",
    "password": "{smtp_server_password}",
    "senderList": [
      "user1@email.com",
      "user2@email.com",
      "..."
    ]
  }
}
```

Respectively:
- `API.key`: The secret key to access the Brapi's API.
- `Email.host`: Host of the SMTP server.
- `Email.username`: Username to access the SMTP server.
- `Email.password`: Password to access the SMTP server.
- `Email.senderList`: A list of e-mails in which the notification e-mail will be sent to.

## Project Structure

The project is structured in several classes and uses the **Observer** design pattern to achieve its results effectively:

- `Arguments.cs`: Specify the structure that will store the argument values after parsing.
- `Config.cs`: Specify the structure of the JSON file (mentioned above) after parsing.
- `EmailHandler.cs`: Wrapper for the SMTP server, containing methods to send the previously written messages and manage the sender list.
- `StockAPI.cs`: Wrapper for the Brapi's API, containing methods to retrieve the information of a given stock.
- `StockObserver.cs`: Observer class which monitors the stock price of a given stock, also sending the e-mails when needed.
- `StockState.cs`: State structure that represents the current state of a given stock, storing informations such as the current price, the currency, etc. Fetches the stock information from the API every 10 seconds
and it's responsible to notify the attached observers when the informations have been updated.
- `Utility.cs`: Contains utility functions to parse the arguments and the configuration JSON file.

## Test environment

During development, [Mailtrap](https://mailtrap.io/?gad_source=1)'s free tier was used to establish a connection with the SMTP server and to verify if the e-mails were being sended accordingly.

## Next steps

Since this project were done as a challenge in a recruitment process, some stuff couldn't be made the best way possible. If the project were going to be continued later on, there would be some points to work on:
- Improve the general error handling capacity of the software.
- Create a more generic parser for the arguments received from the command line, also with better error handling.
- Create some kind of procedure to detect when an e-mail were already being sent, in order to avoid e-mail spamming in certain situations.
- Improve the application's logging, probably using some sort of logging-related package.
