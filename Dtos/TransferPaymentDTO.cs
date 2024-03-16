namespace PaymentsConsumer.Dtos;

public class TransferPaymentDTO(
  int paymentId,
  OriginDTO origin,
  DestinyDTO destiny,
  string originWebhook,
  string destinyWebhook,
  int amount,
  string description)
{
  public int PaymentId { get; } = paymentId;
  public OriginDTO Origin { get; } = origin;
  public DestinyDTO Destiny { get; } = destiny;
  public string OriginWebhook { get; } = originWebhook;
  public string DestinyWebhook { get; } = destinyWebhook;
  public int Amount { get; } = amount;
  public string? Description { get; } = description;

  public CreatePaymentDTO ToCreatePaymentDTO()
  {
    return new CreatePaymentDTO(Origin, Destiny, Amount, Description);
  }

  public TransferStatusDTO ToTransferStatusDTO(string status)
  {
    return new TransferStatusDTO(PaymentId, status);
  }
}