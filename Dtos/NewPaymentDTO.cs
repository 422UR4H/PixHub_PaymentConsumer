using System.ComponentModel.DataAnnotations;

namespace PaymentsConsumer.Dtos;

public class NewPaymentDTO(Guid transactionId, OriginDTO origin, DestinyDTO destiny, int amount, string? description)
{
  public Guid TransactionId { get; } = transactionId;

  public OriginDTO Origin { get; } = origin;

  public DestinyDTO Destiny { get; } = destiny;

  public int Amount { get; } = amount;

  public string? Description { get; } = description;
}