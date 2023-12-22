using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShefPovar
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection database;

        public DatabaseService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Product>().Wait();
        }

        public async Task<List<Product>> GetIngredientsAsync()
        {
            return await database.Table<Product>().ToListAsync();
        }



        public async Task SaveIngredientAsync(Product ingredient)
        {
            if (ingredient.Id == 0)
            {
                await database.InsertAsync(ingredient);
            }
            else
            {
                await database.UpdateAsync(ingredient);
            }
        }

        public async Task DeleteIngredientAsync(Product product)
        {
            await database.DeleteAsync(product);
        }

        public async Task UpdateIngredientAsync(Product product)
        {
            await database.UpdateAsync(product);
        }

        public async Task<double> GetTotalPriceAsync()
        {
            var products = await database.Table<Product>().ToListAsync();
            double totalPrice = products.Sum(p => p.Price);
            return totalPrice;
        }
        public async Task<List<Product>> GetIngredientsSegmentAsync(int skip, int take)
        {
            return await database.Table<Product>().Skip(skip).Take(take).ToListAsync();
        }


    }
}
