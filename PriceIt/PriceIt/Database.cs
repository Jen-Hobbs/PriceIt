using PriceIt.Model;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PriceIt
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ItemDB>().Wait();
        }

        public Task<List<ItemDB>> GetPeopleAsync()
        {

            return _database.Table<ItemDB>().ToListAsync();
        }

        public Task<int> SavePersonAsync(ItemDB person)
        {
            Debug.Write("save Item");
            Debug.Write(person);
            return _database.InsertAsync(person);
        }
        public async Task DeleteItem(ItemDB id)
        {
            await _database.DeleteAsync(id);
        }
        public async Task DeleteAll()
        {
            await _database.DeleteAllAsync<ItemDB>();
        }
        public async Task<List<CategoryInfo>> GetCategoriesAsync()
        {
            return await _database.QueryAsync<CategoryInfo>("SELECT DISTINCT Category from ItemDB");
        }
    }
}