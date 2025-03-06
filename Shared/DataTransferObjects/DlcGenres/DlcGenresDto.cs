namespace Shared.DataTransferObjects.DlcGenres;

public record DlcGenresDto
{
    public int DlcId { get; init; }
    public int GenreId { get; init; }
};