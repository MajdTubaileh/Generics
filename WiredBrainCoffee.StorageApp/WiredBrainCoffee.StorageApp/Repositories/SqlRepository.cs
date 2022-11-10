using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WiredBrainCoffee.StorageApp.Entities;

namespace WiredBrainCoffee.StorageApp.Repositories
{

    public delegate void ItemAdded<in T>(T item); //method pointer to point at a method void and return a parameter
       


    public partial class SqlRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbContext _dbContext;
        private readonly ItemAdded<T>? itemAddedCallback;
        private  DbSet<T> _dbSet;

        public SqlRepository(DbContext dbContext, ItemAdded<T>? ItemAddedCallback = null)
        {
            _dbContext = dbContext;
            itemAddedCallback = ItemAddedCallback;
            _dbSet = dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            List<T> result = new List<T>();
            Console.WriteLine(_dbSet.Count());
         for(int i =1;i<= _dbSet.Count(); i++)
            {
                result.Add(GetById(i));
            }
            result.OrderBy(item => item.Id).ToList();
            return result;

        }

        public T GetById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _dbSet.Find(id);
#pragma warning restore CS8603 // Possible null reference return.
        }
        public void Add(T item)
        {

            _dbSet.Add(item);
            itemAddedCallback?.Invoke(item);
        }
        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        
    }

}
