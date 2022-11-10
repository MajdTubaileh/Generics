using WiredBrainCoffee.StorageApp.Entities;

namespace WiredBrainCoffee.StorageApp.Repositories
{
    public static class RepositoryExtensions
    {
        public static void AddBatch<T>(this IWriteRepository<T> Repository, T[] items) where T : IEntity
        {
            foreach (var item in items)
            {
                Repository.Add(item);
            }
            Repository.Save();
        }
    }
}
