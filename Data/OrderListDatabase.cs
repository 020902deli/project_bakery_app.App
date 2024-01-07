using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_bakery_app;
using project_bakery_app.Models;

namespace project_bakery_app.Data
{

    public class OrderListDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public OrderListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<OrderList>().Wait();
        }
        public Task<List<OrderList>> GetOrderListsAsync()
        {
            return _database.Table<OrderList>().ToListAsync();
        }
        public Task<OrderList> GetOrderListAsync(int id)
        {
            return _database.Table<OrderList>()
            .Where(i => i.ID == id)
           .FirstOrDefaultAsync();
        }
        public Task<int> SaveOrderListAsync(OrderList slist)
        {
            if (slist.ID != 0)
            {
                return _database.UpdateAsync(slist);
            }
            else
            {
                return _database.InsertAsync(slist);
            }
        }
        public Task<int> DeleteOrderListAsync(OrderList slist)
        {
            return _database.DeleteAsync(slist);
        }
    }
}
