namespace PaymentsConsumer.Dtos;

public class TransferStatusDTO(long id, string status)
{
  public long Id { get; } = id;
  public string Status { get; } = status;
}