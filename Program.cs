using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Npgsql;
using PaymentsConsumer.Dtos;
using System.Net.Http.Json;
using PixHub.Dtos;


const string PUBLISHER_ENDPOINT = "http://localhost:8080/payments/finish/";

/** Database **/
var connString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";
await using var conn = new NpgsqlConnection(connString);
await conn.OpenAsync();

/** RabbitMQ **/
string queueName = "payments";
ConnectionFactory factory = new()
{
  HostName = "localhost",
  UserName = "admin",
  Password = "admin"
};
IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

channel.QueueDeclare(
  queue: queueName,
  durable: true,
  exclusive: false,
  autoDelete: false,
  arguments: null
);

Console.WriteLine("[*] Waiting for messages...");

EventingBasicConsumer consumer = new(channel);
consumer.Received += async (model, ea) =>
{
  HttpClient httpClient = new();

  var body = ea.Body.ToArray();
  var message = Encoding.UTF8.GetString(body);
  // Console.WriteLine("[x] Received {0}", message);

  TransferPaymentDTO? dto = JsonSerializer.Deserialize<TransferPaymentDTO>(message);

  if (dto is null)
  {
    Console.WriteLine("Content body is not valid! Transaction faild.");
    channel.BasicReject(ea.DeliveryTag, false);
    return;
  }
  Console.WriteLine($"Processing payment from {dto.Origin.User.Cpf} to {dto.Destiny.Key.Value}");

  string status;
  HttpResponseMessage destinyProviderResponse;
  try
  {
    CancellationTokenSource cancellation = new(TimeSpan.FromMinutes(2));
    destinyProviderResponse = await httpClient
      .PostAsJsonAsync(dto.DestinyWebhook, dto.ToCreatePaymentDTO(), cancellation.Token);

    if (destinyProviderResponse.IsSuccessStatusCode)
    {
      channel.BasicAck(ea.DeliveryTag, false);
      status = "SUCCESS";
    }
    else
    {
      channel.BasicReject(ea.DeliveryTag, false);
      status = "FAILED";
    }
  }
  catch (OperationCanceledException)
  {
    Console.WriteLine("Error: Timeout in the request for the destiny provider!");

    channel.BasicReject(ea.DeliveryTag, false);
    status = "FAILED";
  }
  Console.WriteLine(status);

  await httpClient.PostAsJsonAsync($"{PUBLISHER_ENDPOINT}{dto.PaymentId}", new FinishPaymentsDTO(status));
  await httpClient.PatchAsJsonAsync(dto.OriginWebhook, dto.ToTransferStatusDTO(status));
};

channel.BasicConsume(
  queue: queueName,
  autoAck: false,
  consumer: consumer
);

Console.WriteLine("Press [enter] to exit");
Console.ReadLine();
