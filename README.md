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