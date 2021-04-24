# Payment Gateway

![CI build](https://github.com/AidanTwomey/PaymentGateway/actions/workflows/main.yml/badge.svg)

An API for demonstrating payments in dotnet core. See the Swagger endpoint at `/swagger` for full specification. To make a payment using the API, see the [postman collection](https://github.com/AidanTwomey/PaymentGateway/tree/main/tests/postman) in the tests folder for examples of calling the API.

## Deployed Version

A version of the API has been deployed to a publicly available [API](http://aidant-payment-gateway.northeurope.azurecontainer.io/v1/payments) hosted in an Azure Container instance. This can be called 

## Example 

To make a payment, you should make a POST request to the `/payment` endpoint specifying the payment card, amount and currency in the body as follows for the media type `application/json`:
```
{
  "card": {
      "number": "4916712854785842",
      "expiryMonth": 11,
      "expiryYear": 2022,
      "cvv": "635"
  },
  "amount": 17.89,
  "currency": "USD"
}
```

### Responses

| Payment Status      | Http Status Code | Response body                          |
| ------------------- | ---------------- | -------------------------------------- |
| Successful          | 200              | payment id, masked credit card number  |
| Rejected            | 400              |                                        |
| Unavailable         | 503              |                                        |

Note that at the end of the month the Azure account may be unavailable because the developer has run out of credit. Soz.

## Calling The Bank
This implementation assumes a bank that is callable via a REST API with a resource `/payment` which returns a 200 response should the payment be completed and a 400 if the payment is rejected. The stub implementation for this demo is a simple AWS Lambda behind an API Gateway. The gateway performs some simple validation, including a paymant limit of 100 Syldavian Spufniks. In order to swap this stub for the actual bank, the base url in appsettings.json should be updated.

Note that the bank requires an API key to be provided in the `x-api-key` header. This can be configured in the `appsettings.json` file.