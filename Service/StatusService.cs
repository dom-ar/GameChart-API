using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Status;

namespace Service
{
    internal sealed class StatusService : IStatusService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public StatusService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StatusDto>> GetAllStatusAsync(bool trackChanges)
        {
            var status = await _repository.Status.GetAllStatusAsync(trackChanges);
            var statusDto = _mapper.Map<IEnumerable<StatusDto>>(status);
            return statusDto;
        }

        public async Task<StatusDto> GetStatusAsync(int statusId, bool trackChanges)
        {
            var status = await _repository.Status.GetStatusAsync(statusId, trackChanges);
            if (status is null)
                throw new StatusNotFoundException(statusId);

            var statusDto = _mapper.Map<StatusDto>(status);

            return statusDto;
        }

        public async Task<IEnumerable<StatusDto>> GetStatusesByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var status = await _repository.Status.GetStatusesByIdsAsync(idsList, trackChanges);
            if (idsList.Count() != status.Count())
                throw new CollectionByIdsBadRequestException();

            var statusToReturn = _mapper.Map<IEnumerable<StatusDto>>(status);
            return statusToReturn;
        }

        public async Task<StatusDto> CreateStatusAsync(StatusForCreationDto status)
        {
            var statusEntity = _mapper.Map<Status>(status);

            _repository.Status.CreateStatus(statusEntity);
            await _repository.SaveAsync();

            var statusToReturn = _mapper.Map<StatusDto>(statusEntity);
            return statusToReturn;
        }

        public async Task<(IEnumerable<StatusDto> statuses, string ids)> CreateStatusesCollectionAsync(
            IEnumerable<StatusForCreationDto> statusesCollection)
        {
            var statuses = _mapper.Map<IEnumerable<Status>>(statusesCollection);

            foreach (var status in statuses)
            {
                _repository.Status.CreateStatus(status);
            }
            await _repository.SaveAsync();

            var statusesToReturn = _mapper.Map<IEnumerable<StatusDto>>(statuses).ToList();
            var ids = string.Join(", ", statusesToReturn.Select(s => s.Id));

            return (statuses: statusesToReturn, ids: ids);
        }

        public async Task DeleteStatusAsync(int statusId, bool trackChanges)
        {
            var statusEntity = await _repository.Status.GetStatusAsync(statusId, trackChanges);
            if (statusEntity is null)
                throw new StatusNotFoundException(statusId);

            _repository.Status.DeleteStatus(statusEntity);
            await _repository.SaveAsync();
        }
    }
}
