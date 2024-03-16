using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaymentsConsumer.Dtos;

[method: JsonConstructor]
public class KeyDTO(string value, string type)
{
    [StringLength(32)]
    [Required(ErrorMessage = "Field value is mandatory")]
    public string Value { get; } = value;

    // TODO: fix to enum validation here
    [RegularExpression("^(CPF|Email|Phone|Random)$")]
    [Required(ErrorMessage = "Field type is mandatory")]
    public string Type { get; } = type;

    // public KeyDTO(PixKey pixKey) : this(pixKey.Value, pixKey.Type) { }

  // public PixKey ToEntity(int accountId)
  // {
  //   return new PixKey(Value, Type, accountId);
  // }
}