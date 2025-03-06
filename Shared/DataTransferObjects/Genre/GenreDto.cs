namespace Shared.DataTransferObjects.Genre;

public record GenreDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
};

public record BasicGenreDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
}
