using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Franchise;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service
{
    internal sealed class FranchiseService : IFranchiseService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public FranchiseService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<FranchiseDto> franchises, MetaData metaData)> GetAllFranchisesAsync(FranchiseParameters franchiseParameters, bool trackChanges)
        {
            var franchisesWithMetaData = await _repository.Franchise.GetAllFranchisesAsync(franchiseParameters, trackChanges);
            var franchisesDto = _mapper.Map<IEnumerable<FranchiseDto>>(franchisesWithMetaData);

            return (franchises: franchisesDto, metaData: franchisesWithMetaData.MetaData);
        }

        public async Task<FranchiseDto?> GetFranchiseAsync(int franchiseId, bool trackChanges)
        {
            var franchise = await _repository.Franchise.GetFranchiseAsync(franchiseId, trackChanges);

            if (franchise is null)
                throw new FranchiseNotFoundException(franchiseId);

            var franchiseDto = _mapper.Map<FranchiseDto>(franchise);
            return franchiseDto;
        }

        public async Task<IEnumerable<FranchiseDto>> GetFranchisesByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var franchises = await _repository.Franchise.GetFranchisesByIdsAsync(idsList, trackChanges);
            if (idsList.Count() != franchises.Count())
                throw new CollectionByIdsBadRequestException();

            var franchisesToReturn = _mapper.Map<IEnumerable<FranchiseDto>>(franchises);
            return franchisesToReturn;
        }

        public async Task<FranchiseDto> CreateFranchiseAsync(FranchiseForCreationDto franchise)
        {
            var franchiseEntity = _mapper.Map<Franchise>(franchise);

            _repository.Franchise.CreateFranchise(franchiseEntity);
            await _repository.SaveAsync();

            var franchiseToReturn = _mapper.Map<FranchiseDto>(franchiseEntity);
            return franchiseToReturn;
        }

        public async Task<(IEnumerable<FranchiseDto> franchises, string ids)> CreateFranchisesCollectionAsync(IEnumerable<FranchiseForCreationDto> franchisesCollection)
        {
            var franchises = _mapper.Map<IEnumerable<Franchise>>(franchisesCollection);

            foreach (var franchise in franchises)
            {
                _repository.Franchise.CreateFranchise(franchise);
            }
            await _repository.SaveAsync();

            var franchisesToReturn = _mapper.Map<IEnumerable<FranchiseDto>>(franchises).ToList();
            var ids = string.Join(", ", franchisesToReturn.Select(f => f.Id));

            return (franchises: franchisesToReturn, ids: ids);
        }

        public async Task DeleteFranchiseAsync(int franchiseId, bool trackChanges)
        {
            var franchise = await _repository.Franchise.GetFranchiseAsync(franchiseId, trackChanges);
            if (franchise is null)
                throw new FranchiseNotFoundException(franchiseId);

            _repository.Franchise.DeleteFranchise(franchise);
            await _repository.SaveAsync();
        }
    }
}
