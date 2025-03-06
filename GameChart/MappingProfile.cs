using AutoMapper;
using Entities.Models;
using Entities.Models.Joint_Models;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Developer;
using Shared.DataTransferObjects.Dlc;
using Shared.DataTransferObjects.DlcDevelopers;
using Shared.DataTransferObjects.DlcGenres;
using Shared.DataTransferObjects.DlcRelease;
using Shared.DataTransferObjects.Engine;
using Shared.DataTransferObjects.Franchise;
using Shared.DataTransferObjects.Game;
using Shared.DataTransferObjects.GameDevelopers;
using Shared.DataTransferObjects.GameGenres;
using Shared.DataTransferObjects.GameRelease;
using Shared.DataTransferObjects.Genre;
using Shared.DataTransferObjects.Platform;
using Shared.DataTransferObjects.Publisher;
using Shared.DataTransferObjects.Status;

namespace GameChart
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Developer, DeveloperDto>();
            CreateMap<Developer, BasicDeveloperDto>();
            CreateMap<DeveloperForCreationDto, Developer>();

            CreateMap<Publisher, PublisherDto>();
            CreateMap<PublisherForCreationDto, Publisher>();

            CreateMap<Engine, EngineDto>();
            CreateMap<Engine, BasicEngineDto>();
            CreateMap<EngineForCreationDto, Engine>();

            CreateMap<Franchise, FranchiseDto>();
            CreateMap<Franchise, BasicFranchiseDto>();
            CreateMap<FranchiseForCreationDto, Franchise>();

            CreateMap<Genre, GenreDto>();
            CreateMap<Genre, BasicGenreDto>();
            CreateMap<GenreForCreationDto, Genre>();

            // Game Mapping

            CreateMap<Game, GameDto>()
                .ForMember(dest => dest.Developers, opt =>
                    opt.MapFrom(src => src.GameDevelopers.Select(gd => gd.Developer)))
                .ForMember(dest => dest.Genres, opt =>
                    opt.MapFrom(src => src.GameGenres.Select(gg => gg.Genre)));

            CreateMap<Game, BasicGameDto>()
                .ForMember(dest => dest.Developers, opt =>
                    opt.MapFrom(src => src.GameDevelopers.Select(gd => gd.Developer)))
                .ForMember(dest => dest.Genres, opt =>
                    opt.MapFrom(src => src.GameGenres.Select(gg => gg.Genre)));

            CreateMap<GameForCreationDto, Game>()
                .ForMember(dest => dest.GameDevelopers, opt => opt.Ignore())
                .ForMember(dest => dest.GameGenres, opt => opt.Ignore());

            CreateMap<GameForUpdateDto, Game>()
                .ForMember(dest => dest.GameDevelopers, opt => opt.Ignore())
                .ForMember(dest => dest.GameGenres, opt => opt.Ignore());

            // Game Release Mapping

            CreateMap<GameRelease, GameReleaseDto>();
            CreateMap<GameReleaseCreationDto, GameRelease>();

            // Dlc Mapping

            CreateMap<Dlc, DlcDto>()
                .ForMember(dest => dest.Developers, opt =>
                    opt.MapFrom(src => src.DlcDevelopers.Select(dd => dd.Developer)))
                .ForMember(dest => dest.Genres, opt =>
                    opt.MapFrom(src => src.DlcGenres.Select(dg => dg.Genre)));

            CreateMap<DlcForCreationDto, Dlc>();
            CreateMap<DlcForUpdateDto, Dlc>();

            // Dlc Release Mapping

            CreateMap<DlcRelease, DlcReleaseDto>();
            CreateMap<DlcReleaseCreationDto, DlcRelease>();

            CreateMap<Platform, PlatformDto>();
            CreateMap<Platform, BasicPlatformDto>();
            CreateMap<PlatformForCreationDto, Platform>();

            CreateMap<Status, StatusDto>();
            CreateMap<StatusForCreationDto, Status>();

            CreateMap<GameDevelopers, GameDevelopersDto>();
            CreateMap<GameDevelopersForCreationDto, GameDevelopers>();

            CreateMap<GameGenres, GameGenresDto>();
            CreateMap<GameGenresForCreationDto, GameGenres>();

            CreateMap<DlcGenres, DlcGenresDto>();
            CreateMap<DlcGenresForCreationDto, DlcGenres>();

            CreateMap<DlcDevelopers, DlcDevelopersDto>();
            CreateMap<DlcDevelopersForCreationDto, DlcDevelopers>();
        }
    }
}
