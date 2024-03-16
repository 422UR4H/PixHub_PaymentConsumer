using System.ComponentModel.DataAnnotations;

namespace PaymentsConsumer.Dtos;

public class DestinyDTO(KeyDTO key)
{
  [Required(ErrorMessage = "Field destiny is mandatory")]
  public KeyDTO Key { get; } = key;
}