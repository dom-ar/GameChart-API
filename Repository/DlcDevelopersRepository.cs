using Contracts;
using Entities.Models.Joint_Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DlcDevelopersRepository : RepositoryBase<DlcDevelopers>, IDlcDevelopersRepository
    {
        public DlcDevelopersRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<DlcDevelopers>> GetDlcDevelopersAsync(int dlcId, bool trackChanges) =>
            await FindByCondition(dd => dd.DlcId.Equals(dlcId), trackChanges)
                .OrderBy(dd => dd.Developer!.Name)
                .ToListAsync();

        public void AddDeveloperToDlc(int dlcId, DlcDevelopers dlcDevelopers)
        {
            dlcDevelopers.DlcId = dlcId;
            Create(dlcDevelopers);
        }

        public void RemoveDeveloperFromDlc(DlcDevelopers dlcDevelopers) => Delete(dlcDevelopers);

    }
}