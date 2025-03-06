using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models.Joint_Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class ReleaseService : IReleaseService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ReleaseService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        //public IEnumerable<ReleaseDto> GetAllUpcomingAnnouncedReleases(bool trackChanges)
        //{
        //    var gameReleases = _repository.GameRelease.GetAllGameReleases(trackChanges);
        //    var dlcReleases = _repository.DlcRelease.GetAllDlcReleases(trackChanges);
        //}

    }
}