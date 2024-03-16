using System.ComponentModel.DataAnnotations;

namespace PaymentsConsumer.Dtos;

public class CreatePaymentDTO(OriginDTO origin, DestinyDTO destiny, int amount, string? description)
{
  [Required(ErrorMessage = "Field origin is mandatory")]
  public OriginDTO Origin { get; } = origin;

  [Required(ErrorMessage = "Field destiny is mandatory")]
  public DestinyDTO Destiny { get; } = destiny;

  [Range(1, int.MaxValue)]
  [Required(ErrorMessage = "Field amount is mandatory")]
  public int Amount { get; } = amount;

  public string? Description { get; } = description;
}