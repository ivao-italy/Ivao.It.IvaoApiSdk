namespace Ivao.It.IvaoApiSdk.Dto.Tracker;

public class TransponderDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public ushort Order { get; set; }

    public override string ToString() => Id;

}