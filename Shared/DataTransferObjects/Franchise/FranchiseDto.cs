namespace Shared.DataTransferObjects.Franchise;

public record FranchiseDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
};

public record BasicFranchiseDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
}
