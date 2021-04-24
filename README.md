# Payment Gateway

![CI build](https://github.com/AidanTwomey/PaymentGateway/actions/workflows/main.yml/badge.svg)

An API for demonstrating payments in dotnet core. See the Swagger endpoint at `/swagger` for full specification.

## Deployed Version

[API](http://aidant-payment-gateway.northeurope.azurecontainer.io/v1/payments)

## Example 

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

## Calling The Bank
This implementation assumes a bank that is callable via a REST API with a resource `/payment` which returns a 200 response should the payment be completed and a 400 if the payment is rejected. The stub implementation for this demo is a simple AWS Lambda behind an API Gateway. The gateway performs some simple validation, including a paymant limit of 100 MOngolian Spufniks. In order to swap this stub for the actual bank, the base url in appsettings.json should be updated.