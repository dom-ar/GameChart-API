namespace Entities.Exceptions.NotFound
{
    public sealed class FranchiseNotFoundException : NotFoundException
    {
        public FranchiseNotFoundException(int franchiseId)
            : base($"Franchise with id: {franchiseId} doesn't exist in the database.") { }

        public FranchiseNotFoundException(IEnumerable<int> franchiseIds) 
            : base($"Franchise(s) with id(s): {string.Join(", ", franchiseIds)} doesn't exist in the database.") { }

    }
}