using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShefPovar;

namespace ShefPovar
{
    public class BludoDatabaseService
    {
        readonly SQLiteAsyncConnection database;
        public BludoDatabaseService(string dbPath2)
        {
            database = new SQLiteAsyncConnection(dbPath2);
            database.CreateTableAsync<Bludo>().Wait();
        }

        public Task<int> AddBludoAsync(Bludo bludo)
        {
            return database.InsertAsync(bludo);
        }

        // Метод для получения всех записей из базы данных
        public Task<List<Bludo>> GetBludosAsync()
        {
            return database.Table<Bludo>().ToListAsync();
        }
        public async Task SaveBludoAsync(Bludo newBludo)
        {
            if (newBludo.Id == 0)
            {
                await database.InsertAsync(newBludo);
            }
            else
            {
                await database.UpdateAsync(newBludo);
            }
        }
        public async Task DeleteBludoAsync(Bludo bludo)
        {
            await database.DeleteAsync(bludo);
        }

    }
}
