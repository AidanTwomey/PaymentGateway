# Payment Gateway

![CI build](https://https://github.com/AidanTwomey/PaymentGateway/blob/main/.github/workflows/main.yml/badge.svg)

An API for demonstrating payments in dotnet core.

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