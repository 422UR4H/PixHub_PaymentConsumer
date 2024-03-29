namespace PaymentsConsumer.Dtos;

public class TransferPaymentDTO(
  long paymentId,
  Guid transactionId,
  OriginDTO origin,
  DestinyDTO destiny,
  WebhookDTO webhook,
  int amount,
  string description)
{
  public long PaymentId { get; } = paymentId;
  public Guid TransactionId { get; } = transactionId;
  public OriginDTO Origin { get; } = origin;
  public DestinyDTO Destiny { get; } = destiny;
  public WebhookDTO Webhook { get; } = webhook;
  public int Amount { get; } = amount;
  public string? Description { get; } = description;

  public NewPaymentDTO ToNewPaymentDTO()
  {
    return new NewPaymentDTO(TransactionId, Origin, Destiny, Amount, Description);
  }

  public TransferStatusDTO ToTransferStatusDTO(string status)
  {
    return new TransferStatusDTO(TransactionId, status);
  }
}