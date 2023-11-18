# Stock Quote Alert

### Introduction

A simple stock quote alert that notifies the user via E-mail when the price of a stock is under/above certain thresholds. It uses an HTTP server to fetch the stock data in real time from [Brapi](https://brapi.dev/), 
also using an SMTP server to communicate with the user via E-mail.

### Configuration

For this application to work, it requires a JSON file with the following structure to specify the configuration:

```json
{
  "API": {
    "key": "{brapi_api_secret}"
  },
  "Email": {
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

### Project Structure

The project is structured in several classes and uses the **Observer** design pattern to achieve its results effectively:

- `Arguments.cs`: Specify the structure that will store the argument values after parsing.
- `Config.cs`: Specify the structure of the JSON file (mentioned above) after parsing.
- `EmailHandler.cs`: Wrapper for the SMTP server, containing methods to send the previously written messages and manage the sender list.
- `StockAPI.cs`: Wrapper for the Brapi's API, containing methods to retrieve the information of a given stock.
- `StockObserver.cs`: Observer class which monitors the stock price of a given stock, also sending the e-mails when needed.
- `StockState.cs`: State structure that represents the current state of a given stock, storing informations such as the current price, the currency, etc. Fetches the stock information from the API every 10 seconds
and it's responsible to notify the attached observers when the informations have been updated.
- `Utility.cs`: Contains utility functions to parse the arguments and the configuration JSON file.

### Test environment

During development, [Mailtrap](https://mailtrap.io/?gad_source=1)'s free tier was used to establish a connection with the SMTP server and to verify if the e-mails were being sended accordingly.
