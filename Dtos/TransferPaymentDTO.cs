namespace PaymentsConsumer.Dtos;

public class TransferPaymentDTO(
  int paymentId,
  Guid transactionId,
  OriginDTO origin,
  DestinyDTO destiny,
  string originWebhook,
  string destinyWebhook,
  int amount,
  string description)
{
  public int PaymentId { get; } = paymentId;
  public Guid TransactionId { get; } = transactionId;
  public OriginDTO Origin { get; } = origin;
  public DestinyDTO Destiny { get; } = destiny;
  public string OriginWebhook { get; } = originWebhook;
  public string DestinyWebhook { get; } = destinyWebhook;
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