# PixHub_PaymentConsumer

A console application built in C# as a Consumer (Worker) of the RabbitMQ for PixHub_API.

## Description

The application is responsible to warn **Payment Service Providers** (PSPs) about pix transactions. As a Worker, it was built as part of the PixHub_API to be able to scale horizontally, avoiding response time bottlenecks for origin PSPs without depending on the agility of external services.
it is also part of the processing part of the processing of the logic of the PIX mechanism within the **Central Bank** (BC).

<br />

## Quick start

Note: you need dotnet installed to run this consumer!

Clone the repository:

`git clone https://github.com/422UR4H/PixHub_PaymentConsumer`


Enter the folder and run app:

```bash
cd PixHub_PaymentConsumer/
dotnet watch
```

## Usage

Just run the program above and this application is already working. You will need services from financial institutions to run the application. It can be a simple mock to test requests.
Furthermore, you will need the main application running as well. The entire environment is dockerized together with RabbitMQ, so just run it and send requests to: POST `/payments`.


## Technologies used

For this project, I used:

- C#;
- .NET;
- RabbitMQ;
