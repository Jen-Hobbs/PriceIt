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

        /// <summary>
        /// initiate database connection
        /// </summary>
        /// <param name="dbPath">path of database</param>
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ItemDB>().Wait();
        }

        /// <summary>
        /// get items based on category from database
        /// </summary>
        /// <param name="category">category to search from in database</param>
        /// <returns>list of items based on category</returns>
        public Task<List<ItemDB>> GetItemsAsync(string category)
        {
            if(category == null)
            {
                return _database.Table<ItemDB>().ToListAsync();
            }
            return _database.QueryAsync<ItemDB>("Select * from ItemDB Where Category = ?", category);
        }
        /// <summary>
        /// save item in database
        /// </summary>
        /// <param name="item">Item to save in database</param>
        /// <returns>item added</returns>
        public Task<int> SavePersonAsync(ItemDB item)
        {
            return _database.InsertAsync(item);
        }
        /// <summary>
        /// delete item from database
        /// </summary>
        /// <param name="id">item that is to be deleted</param>
        /// <returns></returns>
        public async Task DeleteItem(ItemDB id)
        {
            await _database.DeleteAsync(id);
        }
        /// <summary>
        /// delete all items in database
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAll()
        {
            await _database.DeleteAllAsync<ItemDB>();
        }
        /// <summary>
        /// get all distinct items in database
        /// </summary>
        /// <returns>list of all distinct categoryInfo </returns>
        public async Task<List<CategoryInfo>> GetCategoriesAsync()
        {
            return await _database.QueryAsync<CategoryInfo>("SELECT DISTINCT Category from ItemDB");
        }
        /// <summary>
        /// accesses database to get a specific item based of ID
        /// </summary>
        /// <param name="id">ID used to get specific item</param>
        /// <returns>a single item based of the ID</returns>
        public async Task<ItemDB> GetItemFromID(int id)
        {
            List<ItemDB> temp = await _database.QueryAsync<ItemDB>("Select * From ItemDB Where ID = ?", id);
            
            return temp[0];
        }
        /// <summary>
        /// Updates a specific item from the database
        /// </summary>
        /// <param name="item">Item to update</param>
        /// <returns></returns>
        public async Task UpdatePrice(ItemDB item)
        {
            await _database.UpdateAsync(item);
        }
    }
}